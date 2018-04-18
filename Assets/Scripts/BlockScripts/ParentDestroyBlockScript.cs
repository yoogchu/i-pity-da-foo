using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDestroyBlockScript: MonoBehaviour
{
	public float healthCoefficient = 1.5f;
	public AudioSource blockAudio;
	public AudioClip hitClip;
	public AudioClip destroyClip;

	public Canvas UI;

	void Start() {

		foreach (Transform child in transform) {
			DestroyBlockScript a = child.gameObject.AddComponent<DestroyBlockScript> ();
			a.healthCoefficient = healthCoefficient;
			a.blockAudio = blockAudio;
			a.hitClip = hitClip;
			a.destroyClip = destroyClip;
			a.ui_manager = UI.GetComponent<UIManager> ();

		}
	}
	
}
