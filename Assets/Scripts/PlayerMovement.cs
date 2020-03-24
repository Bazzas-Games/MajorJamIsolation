using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject heldObject;
    public float grabDistance = 1f;
    public LayerMask grabbableObjects;

    private Rigidbody2D rb;
    private Vector2 aimDir = new Vector2();
    [SerializeField]
    private Transform holdPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FaceMouse();

        if (Input.GetMouseButtonDown(0))
        {
            if(heldObject == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir, grabDistance, grabbableObjects);
                Debug.DrawRay(transform.position, aimDir.normalized);
                if(hit.collider != null && hit.collider.GetComponent<Throwable>() != null)
                {
                    Grab(hit.collider.gameObject);
                    Debug.Log("Hit! " + hit.collider.gameObject.name);
                }
            }
        }
    }


    
    


    void FaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        aimDir = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
        );
        transform.up = aimDir;
    }

    void Grab(GameObject obj)
    {
        heldObject = obj;
        Rigidbody2D heldObjectRB = heldObject.GetComponent<Rigidbody2D>();
        heldObjectRB.isKinematic = true;
        heldObject.transform.parent = holdPoint;
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
    }
}
