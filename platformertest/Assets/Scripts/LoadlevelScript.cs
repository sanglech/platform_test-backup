using UnityEngine;
using System.Collections;

public class LoadlevelScript : MonoBehaviour {

	void OnTriggerEnter(Collider collider){

		Application.LoadLevel("level2");
	}

}
