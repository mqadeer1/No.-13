using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmMovementController : MonoBehaviour
{
   [Header("First Arm Settings")]
    public Transform upperArm1; // First arm's upper arm Transform
    public float upperArm1XTurnRate = 1.0f;
    public float upperArm1YTurnRate = 1.0f;

    [Header("Second Arm Settings")]
    public Transform upperArm2; // Second arm's upper arm Transform
    public float upperArm2XTurnRate = 1.0f;
    public float upperArm2YTurnRate = 1.0f;

    [Header("Shared Settings")]
    public float rotationMultiplier = 2.0f;

    // Rotation limits for both arms
    private float upperArm1XRot = 0.0f, upperArm2XRot = 0.0f;
    private float upperArm1YRot = 0.0f, upperArm2YRot = 0.0f;
    public float upperArmXRotMin = -45.0f, upperArmXRotMax = 45.0f;
    public float upperArmYRotMin = -30.0f, upperArmYRotMax = 30.0f;

    private MouseInput mouseInput;

    void Awake()
    {
        mouseInput = new MouseInput();
    }

    void OnEnable()
    {
        mouseInput.PlayerControls.Enable();
    }

    void OnDisable()
    {
        mouseInput.PlayerControls.Disable();
    }

    void Update()
    {
        // Get mouse delta input
        Vector2 mouseDelta = mouseInput.PlayerControls.MouseMove.ReadValue<Vector2>();

        // Update rotation for Arm 1
        upperArm1XRot += mouseDelta.y * upperArm1XTurnRate * rotationMultiplier * Time.deltaTime;
        upperArm1YRot += mouseDelta.x * upperArm1YTurnRate * rotationMultiplier * Time.deltaTime;
        upperArm1XRot = Mathf.Clamp(upperArm1XRot, upperArmXRotMin, upperArmXRotMax);
        upperArm1YRot = Mathf.Clamp(upperArm1YRot, upperArmYRotMin, upperArmYRotMax);
        upperArm1.localEulerAngles = new Vector3(upperArm1XRot, upperArm1YRot, upperArm1.localEulerAngles.z);

        // Update rotation for Arm 2 (Invert mouse for demonstration purposes)
        upperArm2XRot -= mouseDelta.y * upperArm2XTurnRate * rotationMultiplier * Time.deltaTime;
        upperArm2YRot -= mouseDelta.x * upperArm2YTurnRate * rotationMultiplier * Time.deltaTime;
        upperArm2XRot = Mathf.Clamp(upperArm2XRot, upperArmXRotMin, upperArmXRotMax);
        upperArm2YRot = Mathf.Clamp(upperArm2YRot, upperArmYRotMin, upperArmYRotMax);
        upperArm2.localEulerAngles = new Vector3(upperArm2XRot, upperArm2YRot, upperArm2.localEulerAngles.z);
    }
}

