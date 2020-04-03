using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Door[] openDoors;

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            // on button "press"
            foreach (Door d in openDoors)
            {
               d.Open(); 
			}

		    gameObject.SetActive(false);
        
        }
    }
}