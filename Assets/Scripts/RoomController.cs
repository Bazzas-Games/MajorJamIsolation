using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool hasOxygen;
    public PowerBlock[] doorPlugs;
    public Door[] exitDoors;

    private Rigidbody2D rb;
    private List<HullBreach> breaches = new List<HullBreach>();


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.layer = LayerMask.NameToLayer("BreachChecker");
    }
    
    void Update()
    {
        bool hasHoles = false;
        foreach(HullBreach b in breaches)
        {
            if(b.isBroken) hasHoles = true;
		}
        hasOxygen = !hasHoles;


        bool hasPower = true;
        foreach(PowerBlock p in doorPlugs)
        {
            if (!p.isPowered) hasPower = false;
        }


        if(hasOxygen && hasPower)
        {
            foreach(Door d in exitDoors)
            {
                d.Open();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Breach"))
        {
            breaches.Add(collider.GetComponent<HullBreach>());
            Debug.Log("Discovered breach: " + collider.name + " in room "+ gameObject.name + ". Room total: " + breaches.Count);

        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collider.gameObject.GetComponent<AudioController>().currentRoom = this;
        }
    }


}
