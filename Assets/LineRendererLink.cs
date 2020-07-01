using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererLink : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    
    public Transform startAnchor;
    public Transform endAnchor;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(0, startAnchor.position);
        _lineRenderer.SetPosition(1, endAnchor.position);
    }
}
