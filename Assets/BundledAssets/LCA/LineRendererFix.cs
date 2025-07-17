using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererFix : MonoBehaviour
{

    public Vector2 scale = Vector2.one;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<LineRenderer>().material.mainTextureScale= scale;
    }
}
