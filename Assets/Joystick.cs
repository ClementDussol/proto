using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _delta;

    public Vector2 Direction => _delta.normalized;
    public float Magnitude => _delta.magnitude;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _delta = Input.mousePosition - _startPosition;
        }
    } 
}
