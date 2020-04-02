using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : MonoBehaviour
{
    public bool isPowered = false;
    public bool coolingDown = false;
    BoxCollider2D[] connectors;
    public Collider2D prev;
    public List<Collider2D> next = new List<Collider2D>();
    public bool isPowerSource = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        connectors = GetComponentsInChildren<BoxCollider2D>();
        foreach(BoxCollider2D b in connectors)
        {
            b.gameObject.layer = LayerMask.NameToLayer("PowerConnectors");
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    
    void Update()
    {
        if (isPowerSource) isPowered = true;
        CheckPower();
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("PowerConnectors")&& 
            !isPowered && !coolingDown &&
            collision.rigidbody.GetComponent<PowerBlock>().isPowered){
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
        rb.isKinematic = false;        
    }
}
