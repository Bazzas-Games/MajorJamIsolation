using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Breach[] breaches;
    public bool hasOxygen;

    
    void Update()
    {
        bool hasHoles = false;

        hasHoles = false;
        foreach(Breach b in breaches)
        {
            if(b.isBroken) hasHoles = true;
		}
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        
	}

}
