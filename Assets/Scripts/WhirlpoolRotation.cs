using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolRotation : MonoBehaviour {

	public float rotationSpeed;
	
	// Update is called once per frame
	void Update () {
		float rotate = Time.deltaTime * rotationSpeed;

		gameObject.transform.Rotate(new Vector3(0,0,1) * rotate, Space.Self);
	}
}
