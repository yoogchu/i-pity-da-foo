using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockScript: MonoBehaviour {

	private Rigidbody a;
	public float healthCoefficient = 1.5f;
	float health;

	public AudioSource blockAudio;
	public AudioClip hitClip;
	public AudioClip destroyClip;

	void Start() {
		a = gameObject.GetComponent<Rigidbody> ();
		health = a.mass * healthCoefficient;
	}

	// two stages for destruction --
	// blocks that disappear when the ragdoll hits them,
	// and then blocks that fall, and break other blocks

	void OnCollisionEnter (Collision collision)
	{
		if (gameObject != null && collision.gameObject.GetComponent<Rigidbody> () != null && collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1) {
			health -= KineticEnergy (collision.gameObject.GetComponent<Rigidbody> ());
			if (health < 0f) {
				blockAudio.clip = destroyClip;
				blockAudio.Play ();
				Destroy (gameObject);
			} else {
				blockAudio.clip = hitClip;
				blockAudio.Play ();
			}	
		}
	}

	public static float KineticEnergy(Rigidbody rb) {
		// mass in kg, velocity in meters per second, result is joules
		return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
	}
		
}
