using UnityEngine;
using System.Collections;

public class ArmRotation1 : MonoBehaviour {
	
	// Update is called once per frame
	public int rotationOffset=0;
	void Update () {
		//subtracting the position of the player from mouse position
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position; 
		difference.Normalize (); //normalizing the vector. Meaning that all the sum of the vector will be equal to 1.
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg; // Find the angle in degrees
		
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
	}
}
