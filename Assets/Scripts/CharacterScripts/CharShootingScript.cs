using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShootingScript : MonoBehaviour {
    //public Rigidbody projectile;
    public GameObject ragdoll;
    public Transform releasePoint;
    public AudioSource shootingAudio;
    public AudioClip fireClip;
    public AudioClip chargingClip;
    public CharControlScript ccScript;

    // Set Values
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = .75f;
    public float ammo = 5;

    private float mCurrentLaunchForce;
    private float mChargeSpeed;
    private bool mFired;

    private void OnEnable()
    {
        mCurrentLaunchForce = minLaunchForce;
    }

	// Use this for initialization
	private void Start () {
        mChargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
	}
	
	private void Update () {
        // Reset Launch Arc

        // At max charge but not yet fired
        if (ammo > 0 && ccScript.isAiming)
        {
            if (mCurrentLaunchForce >= maxLaunchForce && !mFired)
            {
                mCurrentLaunchForce = maxLaunchForce;
                Fire();
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                // Press fire button for first time
                mFired = false;
                mCurrentLaunchForce = minLaunchForce;

                // Play charging sounds
                //shootingAudio.clip = chargingClip;
                //shootingAudio.Play();
            }
            else if (Input.GetButton("Fire1") && !mFired)
            {
                // Holding the button, but not yet fired
                mCurrentLaunchForce += mChargeSpeed * Time.deltaTime;

                // Set launch arc?

            }
            else if (Input.GetButtonUp("Fire1") && !mFired)
            {
                // Release and haven't yet fired
                Fire();
            }
        } else
        {
            mFired = false;
            mCurrentLaunchForce = minLaunchForce;
        }
	}

    private void Fire()
    {
        mFired = true;

        // Instantiate Projectile with velocity
        //Rigidbody projectileInstance = Instantiate(projectile, releasePoint.position, releasePoint.rotation) as Rigidbody;
        //projectileInstance.velocity = mCurrentLaunchForce * releasePoint.forward;

        // Instantiate Ragdoll instead of Projectile
        GameObject ragInstance = Instantiate(ragdoll, releasePoint.position + 2 * releasePoint.forward, releasePoint.rotation) as GameObject;
        GameObject ragMain = ragInstance.transform.Find("mixamorig:Hips").gameObject;
        Rigidbody ragMainRbody = ragMain.GetComponent<Rigidbody>();
        ragMainRbody.velocity = mCurrentLaunchForce * releasePoint.forward;

        // Play firing sound
        //shootingAudio.clip = fireClip;
        //shootingAudio.Play();

        // Reset Launch force
        mCurrentLaunchForce = minLaunchForce;

        ammo--;
    }
}
