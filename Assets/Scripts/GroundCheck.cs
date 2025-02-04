using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public Transform groundCheck; // Assign the empty GameObject here
    public float groundDistance = 0.2f;
    public LayerMask groundMask; // Assign your ground layer

    private bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isGrounded)
        {
            // Logic to keep the character grounded (e.g., snapping them to the ground)
            Vector3 position = transform.position;
            position.y = Mathf.Max(position.y - Time.deltaTime * 5f, 0); // Adjust Y-axis smoothly
            transform.position = position;
        }
    }


}
