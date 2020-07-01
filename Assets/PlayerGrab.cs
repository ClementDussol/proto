using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
	public float throwForce;
	public Transform grabAnchor;
	
	private Rigidbody _grabbedBody;
	private bool _isGrabbing;
	
	private Rigidbody _rigidbody;
	private Joint _joint;
	private Joystick _joystick;
	private LineRendererLink[] _arms;
	
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_joystick = GetComponent<Joystick>();
		_arms = GetComponentsInChildren<LineRendererLink>();
		
		foreach (var arm in _arms)
		{
			arm.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		if (_grabbedBody)
		{
			if (Input.GetMouseButtonUp(0))
				Throw(_grabbedBody);
		}
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Grabbable") && !_isGrabbing)
			Grab(collision.rigidbody, collision.GetContact(0).point);
	}

	private void Grab(Rigidbody body, Vector3 point)
	{
		_isGrabbing = true;
		
		_grabbedBody = body;
		_joint = body.gameObject.AddComponent<SpringJoint>();
		_joint.connectedBody = _rigidbody;
		_joint.anchor = body.transform.InverseTransformPoint(point);
		_joint.connectedAnchor = body.transform.InverseTransformPoint(grabAnchor.position);
		_joint.enableCollision = true;
		(_joint as SpringJoint).spring = 25;

		foreach (var arm in _arms)
		{
			arm.gameObject.SetActive(true);
			arm.endAnchor = body.transform;
		}
		
		StopAllCoroutines();
	}

	private void Throw(Rigidbody body)
	{
		DestroyImmediate(_joint);
		
		body.velocity = Vector3.zero;
		body.AddForce(new Vector3(_joystick.Direction.x, 1, _joystick.Direction.y) * throwForce, ForceMode.Impulse);

		_grabbedBody = null;

		StartCoroutine(ReleaseArmsRoutine());
	}

	private IEnumerator ReleaseArmsRoutine()
	{
		yield return new WaitForSecondsRealtime(.33f);
		
		foreach (var arm in _arms)
		{
			arm.gameObject.SetActive(false);
		}

		_isGrabbing = false;
	}
}
