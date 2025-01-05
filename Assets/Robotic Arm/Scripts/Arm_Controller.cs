using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arm_Controller : MonoBehaviour
{
   public Slider armXSlider; // Slider for upper arm X-axis rotation
    public Slider armYSlider; // Slider for upper arm Y-axis rotation

    // Slider values for upper arm rotations (-1 to 1).
    public float upperArmXSliderValue = 0.0f;
    public float upperArmYSliderValue = 0.0f;

    // Transform reference for the upper arm.
    public Transform upperArm;

    // Rotation speeds for the upper arm's X and Y axes.
    public float upperArmXTurnRate = 1.0f;
    public float upperArmYTurnRate = 1.0f;

    // Rotation limits for the upper arm.
    private float upperArmXRot = 0.0f;
    private float upperArmYRot = 0.0f;
    public float upperArmXRotMin = -45.0f;
    public float upperArmXRotMax = 45.0f;
    public float upperArmYRotMin = -30.0f;
    public float upperArmYRotMax = 30.0f;

    void Start()
    {
        // Set default slider values for negative and positive rotations.
        armXSlider.minValue = -1;
        armYSlider.minValue = -1;
        armXSlider.maxValue = 1;
        armYSlider.maxValue = 1;
    }

    void CheckInput()
    {
        // Update slider values for the upper arm.
        upperArmXSliderValue = armXSlider.value;
        upperArmYSliderValue = armYSlider.value;
    }

    void ProcessMovement()
    {
        // Rotating the upper arm around the X-axis.
        upperArmXRot += upperArmXSliderValue * upperArmXTurnRate;
        upperArmXRot = Mathf.Clamp(upperArmXRot, upperArmXRotMin, upperArmXRotMax);

        // Rotating the upper arm around the Y-axis.
        upperArmYRot += upperArmYSliderValue * upperArmYTurnRate;
        upperArmYRot = Mathf.Clamp(upperArmYRot, upperArmYRotMin, upperArmYRotMax);

        // Apply the rotation to the upper arm.
        upperArm.localEulerAngles = new Vector3(
            upperArmXRot,
            upperArmYRot,
            upperArm.localEulerAngles.z
        );
    }

    public void ResetSliders()
    {
        // Reset sliders and rotation values to default.
        upperArmXSliderValue = 0.0f;
        upperArmYSliderValue = 0.0f;
        armXSlider.value = 0.0f;
        armYSlider.value = 0.0f;
    }

    void Update()
    {
        CheckInput();
        ProcessMovement();
    }
}
