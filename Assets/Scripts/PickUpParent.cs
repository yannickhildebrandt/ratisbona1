﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpParent : MonoBehaviour {
	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;
	public Transform sphere;
	// Use this for initialization
	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		
	}

	// Update is called once per frame
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You are holding 'Touch' on the Trigger'");
		}
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have activated TouchDown");
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have activated TouchUp");
		}
		if (device.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You are holding 'Press' on the Trigger'");
		}
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have activated PressDown");
		}
		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have activated PressUp");
		}
		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			Debug.Log ("You have activated PressUp on the Touchpad");
			sphere.transform.position = new Vector3 (0, 0, 0);
		}
	}
	void OnTriggerStay ( Collider col) {
		Debug.Log ("You have collided with" + col.name + " and activated onTriggerStay");
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You have collided with" + col.name + "While holding down touch");
			col.attachedRigidbody.isKinematic = true;
			col.gameObject.transform.SetParent (this.gameObject.transform);
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log("You have released Touch while colliding with" + col.name);
			col.gameObject.transform.SetParent (null);
			col.attachedRigidbody.isKinematic = false;
			tossObject (col.attachedRigidbody);
		}
	}
	void tossObject (Rigidbody rigidbody)
	{
		Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
		if (origin != null) {
			rigidbody.velocity = origin.TransformVector (device.velocity);
			rigidbody.angularVelocity = origin.TransformVector (device.angularVelocity);

		} else {
			rigidbody.velocity = device.velocity;
			rigidbody.angularVelocity = device.angularVelocity;
		}
	}
}
