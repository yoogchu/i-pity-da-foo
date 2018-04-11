using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    public GameObject explosionEffect;
    public float blastRadius = 3f;
    public Rigidbody playerRb;
    public CharControlScript playerScript;

    public float force = 200f;

    void OnCollisionEnter (Collision col)
    {
        // Add particle effect
        //Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb && rb == playerRb)
            {
                playerScript.Explode();
                rb.AddExplosionForce(force, transform.position, blastRadius);
            }
        }

        // Destroy the ball
        Destroy(gameObject);
    }
}
