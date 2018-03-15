using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    public Transform camTransform;
    public Transform target;
    public Transform pivot;
    public Transform mTransform;
    public bool leftPivot;
    private float delta;

    private float mouseX;
    private float mouseY;
    private float smoothX;
    private float smoothY;
    private float smoothXVel;
    private float smoothYVel;
    private float lookAngle;
    private float tiltAngle;

    // Camera Values
    public float turnSmooth = .1f;
    public float moveSpeed = 9f;
    public float aimSpeed = 25f;
    public float yRotSpeed = 8f;
    public float xRotSpeed = 8f;
    public float minAngle = -35f;
    public float maxAngle = 35f;
    public float normalX;
    public float normalY;
    public float normalZ = -3f;
    public float aimZ = -.5f;
    public float aimX = 0f;
    public float crouchY;
    public float adaptSpeed = 9f;

    public InputHandler cinput;
    public CharControlScript ccScript;

    void Awake()
    {
        mTransform = this.transform;
        target = ccScript.mTransform;
    }

    public void FixedUpdate()
    {
        delta = Time.fixedDeltaTime;

        HandlePositions();
        HandleRotations();

        float speed = moveSpeed;
        if (ccScript.isAiming)
            speed = aimSpeed;

        Vector3 targetPos = Vector3.Lerp(mTransform.position, target.position, speed);
        mTransform.position = target.position;
    }

    // Update the position of the camera and pivot depending on character states
    private void HandlePositions()
    {
        float targetX = normalX;
        float targetY = normalY;
        float targetZ = normalZ;
        if (ccScript.isCrouching)
            targetY = crouchY;

        if (ccScript.isAiming)
        {
            targetX = aimX;
            targetZ = aimZ;
        }

        if (leftPivot)
            targetX = -targetX;

        Vector3 newPivotPos = pivot.localPosition;
        newPivotPos.x = targetX;
        newPivotPos.y = targetY;

        Vector3 newCamPos = camTransform.localPosition;
        newCamPos.z = targetZ;

        float t = delta * adaptSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPos, t);
        camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, newCamPos, t);
    }

    // Update the rotation of the camera pivot based on Mouse Movement
    private void HandleRotations()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVel, turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVel, turnSmooth);
        } else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * yRotSpeed;
        Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
        mTransform.rotation = targetRot;

        tiltAngle -= smoothY * xRotSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    }
}
