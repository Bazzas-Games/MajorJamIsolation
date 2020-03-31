using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : MonoBehaviour
{
    public bool isPowered = false;
    BoxCollider2D[] connectors;
    public Collider2D otherPoweredConnector;
    public Collider2D receivingPowerConnector;

    // Start is called before the first frame update
    void Start()
    {
        connectors = GetComponentsInChildren<BoxCollider2D>();
        foreach(BoxCollider2D b in connectors)
        {
            b.gameObject.layer = LayerMask.NameToLayer("PowerConnectors");
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    

    // Update is called once per frame
    void Update()
    {
        CheckPower();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.otherCollider.gameObject.layer == LayerMask.NameToLayer("PowerConnectors")){
            if (isPowered)
            {
                ConnectPower(collision);
            }
        }
    }


    void CheckPower()
    {
        bool prevPowered = isPowered;
        isPowered = GetComponentInParent<PowerSource>() != null;
        if (prevPowered && !isPowered) DisconnectPower();
    }

    void ConnectPower(Collision2D collision)
    {
        Debug.Log("called from "+ name + ", unpowered cable " + collision.rigidbody.name + collision.collider.name+ ", connecting to power source " + collision.otherRigidbody.name + collision.otherCollider.name);
        PowerBlock otherPowerBlock = collision.rigidbody.GetComponent<PowerBlock>();
        otherPowerBlock.receivingPowerConnector = collision.otherCollider;
        otherPowerBlock.otherPoweredConnector = collision.collider;

        collision.otherRigidbody.velocity = Vector2.zero;
        collision.rigidbody.velocity = Vector2.zero;
        collision.otherRigidbody.angularVelocity = 0;
        collision.rigidbody.angularVelocity = 0;
        collision.otherRigidbody.isKinematic = true;
        collision.rigidbody.isKinematic = true;
        Vector3 rot = collision.rigidbody.transform.rotation.eulerAngles;
        rot.z = Mathf.Round(rot.z / 90) * 90;
        collision.rigidbody.transform.eulerAngles = rot;

        collision.rigidbody.transform.parent = collision.otherCollider.transform;
        collision.rigidbody.transform.localPosition = Vector2.zero;

        collision.collider.enabled = false;
        collision.otherCollider.enabled = false;
    }


    void DisconnectPower()
    {
        Debug.Log(name + " has disconnected from the power source.");
        foreach(PowerBlock p in GetComponentsInChildren<PowerBlock>())
        {
            p.gameObject.transform.parent = null;
            foreach(BoxCollider2D b in p.connectors)
            {
                if (b.GetComponentInChildren<PowerBlock>() == null)
                    b.enabled = true;
            }
        }

    }
}
