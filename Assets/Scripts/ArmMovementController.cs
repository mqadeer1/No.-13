using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmMovementController : MonoBehaviour
{
    public Transform upperArm; // Transform reference for the upper arm

    // Rotation speeds for the upper arm's X and Y axes
    public float upperArmXTurnRate = 1.0f;
    public float upperArmYTurnRate = 1.0f;

    // Multiplier to amplify movement for fine-tuning
    public float rotationMultiplier = 2.0f;

    // Rotation limits for the upper arm
    private float upperArmXRot = 0.0f;
    private float upperArmYRot = 0.0f;
    public float upperArmXRotMin = -45.0f;
    public float upperArmXRotMax = 45.0f;
    public float upperArmYRotMin = -30.0f;
    public float upperArmYRotMax = 30.0f;

    private MouseInput mouseInput; // Reference to the Input System class for mouse actions

    void Awake()
    {
        // Initialize the Input System
        mouseInput = new MouseInput();
    }

    void OnEnable()
    {
        // Enable the Input Action Map
        mouseInput.PlayerControls.Enable();
    }

    void OnDisable()
    {
        // Disable the Input Action Map
        mouseInput.PlayerControls.Disable();
    }

    void Update()
    {
        // Get mouse delta input from the Input System
        Vector2 mouseDelta = mouseInput.PlayerControls.MouseMove.ReadValue<Vector2>();

        // Update the upper arm rotation based on the mouse delta
        upperArmXRot += mouseDelta.y * upperArmXTurnRate * rotationMultiplier * Time.deltaTime;
        upperArmYRot += mouseDelta.x * upperArmYTurnRate * rotationMultiplier * Time.deltaTime;

        // Clamp the rotations to their respective limits
        upperArmXRot = Mathf.Clamp(upperArmXRot, upperArmXRotMin, upperArmXRotMax);
        upperArmYRot = Mathf.Clamp(upperArmYRot, upperArmYRotMin, upperArmYRotMax);

        // Apply the rotation to the upper arm
        upperArm.localEulerAngles = new Vector3(
            upperArmXRot,
            upperArmYRot,
            upperArm.localEulerAngles.z
        );

        //Debug.Log($"Mouse Delta: {mouseDelta}, Rotations - X: {upperArmXRot}, Y: {upperArmYRot}");
    }
}

