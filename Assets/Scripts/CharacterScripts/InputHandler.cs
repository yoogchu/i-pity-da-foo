using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
    bool shootInput;
    bool crouchInput;

    public CameraHandler camHandler;
    public Vector3 aimPos;

    public float moveAmount
    {
        get;
        private set;
    }
    public Vector3 moveDirection
    {
        get;
        private set;
    }
    public Vector3 rotDirection
    {
        get;
        private set;
    }
    public float horizontal
    {
        get;
        private set;
    }
    public float vertical
    {
        get;
        private set;
    }
    public bool aimInput
    {
        get;
        private set;
    }
    public bool runInput
    {
        get;
        private set;
    }

    void FixedUpdate()
    {
        GetInput();

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        // Determine Movement Direction based on camera
        Vector3 moveDir = camHandler.mTransform.forward * vertical;
        moveDir += camHandler.mTransform.right * horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;

        // Rotation Direction is based on camera
        rotDirection = camHandler.mTransform.forward;

        // Handle Aim Position
        Ray ray = new Ray(camHandler.mTransform.position, camHandler.mTransform.forward);
        aimPos = ray.GetPoint(30);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 0))
        {
            aimPos = hit.point;
        }
    }

    // Get the input values
    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    void Update()
    {
        // TODO: Determine proper aim controls (AIM and FIRE?)
        aimInput = Input.GetButton("Fire2");
        runInput = Input.GetButton("Run");
    }
}
