using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : MonoBehaviour
{
    public bool isPowered = false;
    BoxCollider2D[] connectors;

    // Start is called before the first frame update
    void Start()
    {
        connectors = GetComponentsInChildren<BoxCollider2D>();
        foreach(BoxCollider2D b in connectors)
        {
            b.gameObject.layer = LayerMask.NameToLayer("PowerConnectors");
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.otherCollider.gameObject.layer == LayerMask.NameToLayer("PowerConnectors")){
            Debug.Log("Collided with connector");
        }
    }
}
