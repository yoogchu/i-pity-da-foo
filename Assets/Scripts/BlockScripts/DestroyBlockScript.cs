using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockScript: MonoBehaviour {

	public float threshold = 25f;

	// two stages for destruction --
	// blocks that disappear when the ragdoll hits them,
	// and then blocks that fall, and break other blocks

	void OnCollisionEnter (Collision collision)
	{
		if (gameObject != null && collision.gameObject.GetComponent<Rigidbody> () != null) {
			if (KineticEnergy (collision.gameObject.GetComponent<Rigidbody> ()) > threshold) {
				Destroy (gameObject);
			}
		}
	}
	public static float KineticEnergy(Rigidbody rb) {
		// mass in kg, velocity in meters per second, result is joules
		return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
	}
		
}
