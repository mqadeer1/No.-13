using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
   [Header("Rotation Settings")]
    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 50f;

    [Tooltip("Rotation axis (e.g., X: 1, Y: 0, Z: 0 for X-axis rotation)")]
    public Vector3 rotationAxis = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation for this frame
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Rotate the object
        transform.Rotate(rotationAxis.normalized * rotationAmount);
    }
}
