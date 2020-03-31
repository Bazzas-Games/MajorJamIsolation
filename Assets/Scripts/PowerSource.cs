using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    private PowerBlock pb;
    private BoxCollider2D output;
    // Start is called before the first frame update
    void Start()
    {
        output = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentsInChildren<PowerBlock>().Length > 1) output.enabled = true;
    }
}
