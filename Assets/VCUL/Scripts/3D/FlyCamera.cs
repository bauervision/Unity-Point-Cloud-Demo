﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyCamera : MonoBehaviour
{
    [Header("Camera")]
    [Tooltip("Multiplier for camera sensitivity.")]
    [Range(0f, 300)]
    public float sensitivity = 90f;
    [Tooltip("Multiplier for camera movement upwards.")]
    [Range(0f, 10f)]
    public float climbSpeed = 4f;
    [Tooltip("Multiplier for normal camera movement.")]
    [Range(0f, 20f)]
    public float normalMoveSpeed = 10f;
    [Tooltip("Multiplier for slower camera movement.")]
    [Range(0f, 5f)]
    public float slowMoveSpeed = 0.25f;
    [Tooltip("Multiplier for faster camera movement.")]
    [Range(0f, 40f)]
    public float fastMoveSpeed = 3f;
    [Tooltip("Rotation limits for the X-axis in degrees. X represents the lowest and Y the highest value.")]
    public Vector2 rotationLimitsX;
    [Tooltip("Rotation limits for the X-axis in degrees. X represents the lowest and Y the highest value.")]
    public Vector2 rotationLimitsY;
    [Tooltip("Whether the rotation on the X-axis should be limited.")]
    public bool limitXRotation = false;
    [Tooltip("Whether the rotation on the Y-axis should be limited.")]
    public bool limitYRotation = false;

    public Text toggleText;
    public GameObject PCD_Mesh;

    private Vector2 cameraRotation;
    private bool cameraLocked = false;
    private float newPointSize = 0.05f;


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            cameraLocked = !cameraLocked;

        }

        toggleText.text = cameraLocked ? "UI Enabled" : "Hit Space Bar\nto Enable UI Interaction";
        handleShaderUpdate();
    }

    public void SetPointSize(float value)
    {
        newPointSize = value;
    }

    private void handleShaderUpdate()
    {
        var shader = PCD_Mesh.GetComponent<Renderer>().sharedMaterial;
        shader.SetFloat("_PointSize", newPointSize);

    }
    // LateUpdate is called every frame, if the Behaviour is enabled
    private void LateUpdate()
    {
        if (!cameraLocked)
        {
            cameraRotation.x += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            cameraRotation.y += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            if (limitXRotation)
            {
                cameraRotation.x = Mathf.Clamp(cameraRotation.x, rotationLimitsX.x, rotationLimitsX.y);
            }
            if (limitYRotation)
            {
                cameraRotation.y = Mathf.Clamp(cameraRotation.y, rotationLimitsY.x, rotationLimitsY.y);
            }

            transform.localRotation = Quaternion.AngleAxis(cameraRotation.x, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(cameraRotation.y, Vector3.left);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += transform.right * (normalMoveSpeed * fastMoveSpeed) * Input.GetAxis("Horizontal") * Time.deltaTime;
                transform.position += transform.forward * (normalMoveSpeed * fastMoveSpeed) * Input.GetAxis("Vertical") * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.position += transform.right * (normalMoveSpeed * slowMoveSpeed) * Input.GetAxis("Horizontal") * Time.deltaTime;
                transform.position += transform.forward * (normalMoveSpeed * slowMoveSpeed) * Input.GetAxis("Vertical") * Time.deltaTime;
            }
            else
            {
                transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
                transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                transform.position += transform.up * climbSpeed * Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                transform.position -= transform.up * climbSpeed * Time.deltaTime;
            }
        }
    }
}
