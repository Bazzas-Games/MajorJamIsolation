using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public bool isAnchored = false;
    public Rigidbody2D rb;
    public bool canPatchBreach = false;

    void Start()
    {
        if (!isAnchored) rb = GetComponent<Rigidbody2D>();
        gameObject.layer = 8;
    }
}
