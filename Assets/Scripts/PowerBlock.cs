using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : MonoBehaviour
{
    public bool isPowered = false;
    public bool coolingDown = false;
    public bool isPowerSource = false;
    public bool isPowerTarget = false;
    public Collider2D prev;
    public Collider2D targetCollider;
    public List<Collider2D> next = new List<Collider2D>();

    BoxCollider2D[] connectors;
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        connectors = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D b in connectors)
        {
            b.gameObject.layer = LayerMask.NameToLayer("PowerConnectors");
        }
        if (isPowerSource || isPowerTarget) gameObject.layer = LayerMask.NameToLayer("PushOffWall");
        else gameObject.layer = LayerMask.NameToLayer("Grabbable");
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        anim.SetBool("IsPowered", isPowered);
        if (isPowerSource) isPowered = true;
        CheckPower();
        if (isPowerTarget)
        {
            Collider2D checkCollider = Physics2D.OverlapBox(targetCollider.bounds.center, targetCollider.bounds.size, gameObject.transform.rotation.eulerAngles.z, LayerMask.GetMask("PowerConnectors"));
            if(checkCollider != null)
            {
                if (checkCollider.GetComponentInParent<PowerBlock>().isPowered)
                {
                    ConnectPower(checkCollider);
                }
            }
        }
    }
    

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(name + " colliding with " + collision.otherRigidbody.name);
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("PowerConnectors")&& 
            !isPowered && !coolingDown){

            if (collision.collider.GetComponentInParent<PowerBlock>().isPowered)
            {
                ConnectPower(collision);
            }
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if(coolingDown && collision.collider.gameObject.layer == LayerMask.NameToLayer("PowerConnectors"))
        {
            coolingDown = false;
        }
    }


    public void CheckPower()
    {
        if (!isPowerSource)
        {
            if (isPowered && prev == null) DisconnectPower();
        }
    }


    void ConnectPower(Collision2D collision)
    {
        Rigidbody2D unpoweredRb = collision.otherRigidbody;
        Rigidbody2D poweredRb = collision.rigidbody;
        Collider2D unpoweredCol = collision.otherCollider;
        Collider2D poweredCol = collision.collider;
        PowerBlock unpoweredBlock = unpoweredRb.GetComponent<PowerBlock>();
        PowerBlock poweredBlock = poweredRb.GetComponent<PowerBlock>();


        // physically lock 
        unpoweredRb.velocity = Vector2.zero;
        unpoweredRb.angularVelocity = 0;
        unpoweredRb.isKinematic = true;
        poweredRb.velocity = Vector2.zero;
        poweredRb.angularVelocity = 0;
        poweredRb.isKinematic = true;

        Vector3 rot = unpoweredRb.transform.rotation.eulerAngles;
        rot.z = Mathf.Round(rot.z / 90) * 90;
        unpoweredRb.transform.eulerAngles = rot;
        unpoweredRb.transform.parent = poweredCol.transform;
        unpoweredRb.transform.localPosition = Vector2.zero;
        // end lock

        // disable only the two connected colliders
        unpoweredCol.enabled = false;
        poweredCol.enabled = false;

        // store references to each collider object for disconnect later
        unpoweredBlock.prev = poweredCol;
        poweredBlock.next.Add(unpoweredCol);

        // power up block
        unpoweredBlock.isPowered = true;
    }

    void ConnectPower(Collider2D other)
    {
        Rigidbody2D unpoweredRb = rb;
        Rigidbody2D poweredRb = other.attachedRigidbody;
        Collider2D unpoweredCol = targetCollider;
        Collider2D poweredCol = other;
        PowerBlock unpoweredBlock = unpoweredRb.GetComponent<PowerBlock>();
        PowerBlock poweredBlock = poweredRb.GetComponent<PowerBlock>();


        // physically lock 
        unpoweredRb.velocity = Vector2.zero;
        unpoweredRb.angularVelocity = 0;
        unpoweredRb.isKinematic = true;
        poweredRb.velocity = Vector2.zero;
        poweredRb.angularVelocity = 0;
        poweredRb.isKinematic = true;

        Vector3 rot = unpoweredRb.transform.rotation.eulerAngles;
        rot.z = Mathf.Round(rot.z / 90) * 90;
        unpoweredRb.transform.eulerAngles = rot;
        unpoweredRb.transform.parent = poweredCol.transform;
        unpoweredRb.transform.localPosition = Vector2.zero;
        // end lock

        // disable only the two connected colliders
        unpoweredCol.enabled = false;
        poweredCol.enabled = false;

        // store references to each collider object for disconnect later
        unpoweredBlock.prev = poweredCol;
        poweredBlock.next.Add(unpoweredCol);

        // power up block
        unpoweredBlock.isPowered = true;
    }


    public void DisconnectPower()
    {
        coolingDown = true;
        isPowered = false;

        // turn on all of this block's colliders
        foreach (BoxCollider2D b in connectors)
        {
            b.enabled = true;
        }
        
        // turn on prev collider
        if(prev != null) prev.enabled = true;
        prev = null;

        // call DisconnectPower on any subsequent blocks
        for (int i = next.Count - 1; i >=0; i--)
        {
            Collider2D c = next[i];
            next.Remove(c);
            c.GetComponentInParent<PowerBlock>().DisconnectPower();
        }
        

        // physically unlock
        transform.parent = null;
        if(!isPowerTarget) rb.isKinematic = false;        
    }
}
