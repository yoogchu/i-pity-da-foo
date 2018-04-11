using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootingScript : MonoBehaviour {

    public GameObject projectile;
    public Transform charTransform;
    public Rigidbody charRigidBody;
    public CharControlScript charControlScript;
    public VelocityReporterScript velReport;

    public float velocity;
    public float delay;
    public float rotateSpeed;

    private Transform enemyTransform;
    private float g;
    private float radianAngle;
    private float degAngle;
    private bool enemyClose;
    private float timeSince;

    // Use this for initialization
    void Start () {
        enemyTransform = GetComponent<Transform>();

        g = Mathf.Abs(Physics2D.gravity.y);

        timeSince = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        // Find the (x,y) of the char in relation to enemy
        radianAngle = AngleOfPlayer();
        degAngle = radianAngle * Mathf.Rad2Deg;

        Vector3 heading = charTransform.position + velReport.velocity * 2f - enemyTransform.position;
        heading.y = 0;
        Quaternion newDir = Quaternion.LookRotation(heading) * Quaternion.Euler(-degAngle, 0, 0);
        enemyTransform.rotation = Quaternion.Lerp(enemyTransform.rotation, newDir, rotateSpeed * Time.fixedDeltaTime);

        // If close enough to hit and delay is good, fire shot
        if (enemyClose && Time.time - timeSince > delay)
        {
            FireShot();
        }
	}

    float AngleOfPlayer ()
    {
        Vector3 difference = charTransform.position + velReport.velocity * 2f - enemyTransform.position;
        float x = new Vector2(difference.x, difference.z).magnitude;
        float y = difference.y;
        float sqrt = Mathf.Pow(velocity, 4.0f) - g * (g * x * x + 2 * y * velocity * velocity);
        enemyClose = sqrt >= 0;
        if (!enemyClose)
            return 0;

        return Mathf.Atan((velocity * velocity + Mathf.Sqrt(sqrt)) / (g * x));
    }

    void FireShot()
    {
        GameObject projInstance = Instantiate(projectile, enemyTransform.position, enemyTransform.rotation) as GameObject;
        projInstance.GetComponent<ExplosionScript>().playerRb = charRigidBody;
        projInstance.GetComponent<ExplosionScript>().playerScript = charControlScript;
        Rigidbody projRBody = projInstance.GetComponent<Rigidbody>();
        projRBody.velocity = velocity * enemyTransform.forward;

        timeSince = Time.time;
    }
}
