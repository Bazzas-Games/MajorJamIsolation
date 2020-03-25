using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Throwable heldObject;
    public Transform holdPoint;
    public LayerMask grabbableObjects;
    public float grabDistance = 1f;
    public float maxForce = 10f;
    public float maxVel = 5f;
    public float pullbackDistance = 100000f;
    public float thrustForce = 1f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 mousePos2D;
    private Vector2 mouseClickPos;
    private Vector2 aimDir = new Vector2();
    private Vector2 dragDirection = Vector2.zero;
    private float dragDistRaw = 0; // distance from mouse to click point, can be negative
    private float dragDist = 0; // clamped distance from mouse to click point, 0 - dragInputScale
    private float dragMult = 0; // 0 - 1, how close is mouse to dragInputScale?


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Microthrusters
        Vector2 thrusters = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.AddForce(thrusters.normalized * thrustForce);


        mousePos2D = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))  // grab object if left clicked
        {
            if(heldObject == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir, grabDistance, grabbableObjects);
                Debug.DrawRay(transform.position, aimDir.normalized);
                if(hit.collider != null && hit.collider.GetComponent<Throwable>() != null)
                {
                    Grab(hit.collider.gameObject.GetComponent<Throwable>());
                    Debug.Log("Hit! " + hit.collider.gameObject.name);
                }
            }
        }
        if(Input.GetMouseButtonDown(1)) // start charging
        {
            mouseClickPos = mousePos2D;
        }
        if (Input.GetMouseButton(1)) // charge throw
        {
            if (heldObject != null)
            {
                dragDirection = Camera.main.WorldToScreenPoint(transform.position);
                dragDirection = dragDirection - mouseClickPos;
                dragDistRaw = Vector2.Dot(mouseClickPos - dragDirection, mouseClickPos - mousePos2D);
                dragDist = Mathf.Clamp(dragDistRaw, 0, pullbackDistance);
                dragMult = dragDist / pullbackDistance;

                Debug.DrawLine(mouseClickPos, mouseClickPos - dragDirection, Color.blue);
                animator.SetBool("isDragging", true);
                animator.SetFloat("Blend", dragMult);
            }
            else FaceMouse();
        }
        else {          // unless charging a throw, face the mouse.
            FaceMouse();
            animator.SetBool("isDragging", false);
        }
    }


    void FaceMouse()
    {
        aimDir = Camera.main.ScreenToWorldPoint(mousePos2D) - transform.position;
        transform.up = aimDir;
    }

    void Grab(Throwable obj)
    {
        heldObject = obj;
        Collider2D c = heldObject.GetComponent<Collider2D>();
        c.enabled = false;
        Rigidbody2D heldObjectRB = heldObject.gameObject.GetComponent<Rigidbody2D>();
        heldObjectRB.isKinematic = true;
       
        heldObject.transform.parent = holdPoint;
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
    }


    void Throw()
    {

        if (!heldObject.isAnchored)
        {

        }
    }
}
