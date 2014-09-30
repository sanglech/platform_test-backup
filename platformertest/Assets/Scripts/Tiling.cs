﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX=2;              // Offeset so we dont get wierd errors?

	public bool hasARightBuddy=false;  // used for checkign if we need to instantiate stuff
	public bool hasALeftBuddy=false;    

	public bool reverseScale=false;    //used if the object is not tilable

	private float spriteWidth=0f;     //the width of our element
	private Camera cam;
	private Transform myTransform;


	void Awake(){
		cam = Camera.main;
		myTransform = transform;
		}

	// Use this for initialization

	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	
	}
	
	// Update is called once per frame
	void Update () {
		//Does it need a left and right buddy? if not do nothing
		if (!hasALeftBuddy || !hasARightBuddy) 
		{
			//Calculate the cameras extend (half the width) of what the camera can see in world coordinates
			float camHorizontalExtend=cam.orthographicSize* Screen.width/Screen.height;

			//calculate the x position where the camera can see the edge of the sprite (element)
			float edgeVisiblePositionRight=(myTransform.position.x + spriteWidth/2)-camHorizontalExtend;
			float edgeVisiblePositionLeft=(myTransform.position.x +spriteWidth/2)+camHorizontalExtend;

			// Check if camera can see outside of the foreground, and then if we can call make new buddy
			if(cam.transform.position.x>=edgeVisiblePositionRight-offsetX && !hasARightBuddy)
			{
				MakeNewBuddy(1);
				hasARightBuddy=true;
			}
			else if(cam.transform.position.x<=edgeVisiblePositionLeft+offsetX && !hasALeftBuddy)
			{
				MakeNewBuddy(-1);
				hasALeftBuddy=true;
			}
		}
	
	}

	// A function that creates a buddy on the side required.
	void MakeNewBuddy(int rightOrLeft){
		//Calculating the new position for a new buddy.
		Vector3 newPosition = new Vector3 (myTransform.position.x+spriteWidth*rightOrLeft,myTransform.position.y,myTransform.position.z);
		// Creating our new buddy and storing him somewhere.
		Transform newBuddy = Instantiate (myTransform,newPosition,myTransform.rotation) as Transform;

		// If not tilable reverse the x size of our object to get rid of non-symmetry
		if (reverseScale == true) {
			newBuddy.localScale=new Vector3(newBuddy.localScale.x*-1,newBuddy.localScale.y,newBuddy.localScale.z);	
		}

		newBuddy.parent = myTransform.parent;
		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling> ().hasALeftBuddy = true;
		} 
		else {
			newBuddy.GetComponent<Tiling>().hasARightBuddy=true;
		}
	}
}
