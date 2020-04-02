using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<HullBreach> breaches = new List<HullBreach>();
    public bool hasOxygen;

    
    void Update()
    {
        bool hasHoles = false;

        hasHoles = false;
        foreach(HullBreach b in breaches)
        {
            if(b.isBroken) hasHoles = true;
		}
        hasOxygen = !hasHoles;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Breach"))
        {
            breaches.Add(collider.GetComponent<HullBreach>());
            Debug.Log("HullBreaches count: " + breaches.Count);
            
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        
	}

}
