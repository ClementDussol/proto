using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    
    private Joystick _joystick;
    private Rigidbody _rigidbody;
    private bool _isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _isMoving = Input.GetMouseButton(0);
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _rigidbody.velocity = new Vector3(_joystick.Direction.x * speed, _rigidbody.velocity.y, _joystick.Direction.y * speed);
        }
        else
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.up * _rigidbody.velocity.y, .1f);
        }
        
        
        _rigidbody.rotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);
    }
}
