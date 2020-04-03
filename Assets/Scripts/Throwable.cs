using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool canPatchBreach = false;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Grabbable");
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = 8;
        rb.angularDrag = 0f;

        rb.AddTorque(Random.Range(-50f, 50f));
    }
}
