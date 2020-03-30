using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public HullBreach[] breaches;
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

    void OnTriggerStay2D(Collider2D collision)
    {
        
	}

}
