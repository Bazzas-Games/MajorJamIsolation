using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    private List<ContactPoint2D> contacts = new List<ContactPoint2D>();
    private int test = 0;
    public bool oxygen;

    
    void FixedUpdate()
    {
        Debug.Log(test);

        test = 0;
    }

   

    void OnTriggerStay2D(Collider2D collision)
    {

        test = 1;
	}

}
