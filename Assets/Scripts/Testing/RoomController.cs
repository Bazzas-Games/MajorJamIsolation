using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    private List<ContactPoint2D> contacts;

    public bool oxygen;

    // Update is called once per frame
    void Update()
    {
        
        if(contacts.Count <= 0) oxygen = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetContacts(contacts);
        Debug.DrawLine(transform.position, new Vector2(10,10), Color.green);
        Debug.Log("Is Colliding");

	}

}
