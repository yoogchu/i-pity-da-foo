using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(InputHandler))]
public class CharControlScript : MonoBehaviour {
    private Animator anim;
    private Rigidbody rbody;
    private InputHandler cinput;
    private CapsuleCollider ccollider;

    private Transform leftFoot;
    private Transform rightFoot;
    public bool isGrounded;

    // Character Values
    public float walkSpeed = 4f;
    public float sprintSpeed = 5f;
    public float crouchSpeed = 2f;
    public float aimSpeed = 2f;
    public float rotateSpeed = 8f;

    // Character States
    public bool onGround;
    public bool isAiming;
    public bool isCrouching;
    public bool isRunning;

    private float delta;
    public Transform mTransform;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();
        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<InputHandler>();
        if (cinput == null)
            Debug.Log("CharacterInputController could not be found");
    }

    void Start()
    {
        mTransform = this.transform;

        //example of how to get access to certain limbs
        leftFoot = this.transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
        rightFoot = this.transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg/mixamorig:RightFoot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");

        anim.applyRootMotion = false;

        isGrounded = false;

        //never sleep so that OnCollisionStay() always reports for ground check
        rbody.sleepThreshold = 0f;
    }


    // Handle Movement on Normal Case
    void MovementNormal()
    {
        // Increase drag depending on input amount
        if (cinput.moveAmount > .05f)
            rbody.drag = 0;
        else
            rbody.drag = 4;

        // Set transform speed depending on state of character
        float speed = walkSpeed;
        if (isRunning)
            speed = sprintSpeed;
        if (isCrouching)
            speed = crouchSpeed;

        Vector3 dir = mTransform.forward * (speed * cinput.moveAmount);
        rbody.velocity = dir;
    }

    // Handle Rotation on Normal Case
    void RotationNormal()
    {
        Vector3 targetDir = cinput.rotDirection;
        if (!isAiming)
            targetDir = cinput.moveDirection;
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = mTransform.forward;

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(mTransform.rotation, lookDir, rotateSpeed * delta);
        mTransform.rotation = targetRotation;
    }

    // Update Animations on Normal Case
    void HandleAnimationsNormal()
    {
        float anim_v = cinput.moveAmount;
        anim.SetFloat("Vertical", anim_v, 0.15f, delta);
    }

    // Update Animations on Aiming Case
    void HandleAnimationsAiming()
    {
        float v = cinput.vertical;
        float h = cinput.horizontal;

        v = Mathf.Clamp(v, -.4f, .4f);

        anim.SetFloat("Horizontal", h, 0.2f, delta);
        anim.SetFloat("Vertical", v, 0.2f, delta);
    }

    // Update movement based on aiming direction
    void MovementAiming()
    {
        float speed = aimSpeed;
        Vector3 dir = cinput.moveDirection * speed;
        rbody.velocity = dir;
    }

    public void FixedUpdate()
    {
        delta = Time.fixedDeltaTime;

        if (isAiming)
        {
            MovementAiming();
            HandleAnimationsAiming();
        } else
        {
            MovementNormal();
            HandleAnimationsNormal();
        }

        RotationNormal();
    }

    public void Update()
    {
        // Update state based on input handler
        isAiming = cinput.aimInput;

        // If state is updated, update animator
        anim.SetBool("sprint", isRunning);
        anim.SetBool("crouching", isCrouching);
        anim.SetBool("aiming", isAiming);
    }
}
