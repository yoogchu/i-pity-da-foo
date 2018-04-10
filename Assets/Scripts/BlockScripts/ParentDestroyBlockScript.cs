using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDestroyBlockScript: MonoBehaviour
{
	void Start() {
		foreach (Transform child in transform) {
			child.gameObject.AddComponent<DestroyBlockScript> ();
		}
	}
	
}
