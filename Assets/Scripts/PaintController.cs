using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaintController : MonoBehaviour
{
    public Transform sprayNozzle; // The tip of the robotic arm (source of the ray)
    public LayerMask paintableLayer; // Layer mask to define paintable objects
    public Material paintMaterial; // Material to apply as paint
    public float brushSize = 0.2f; // Size of the painted area
    public float rayDistance = 5f; // Maximum distance of the raycast

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
        // Check if the left mouse button is held down
        if (mouseInput.PlayerControls.MouseClick.ReadValue<float>() > 0)
        {
            Vector3 rayDirection = -sprayNozzle.up;
            Ray ray = new Ray(sprayNozzle.position, rayDirection);
            Debug.DrawRay(sprayNozzle.position, rayDirection * rayDistance, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, paintableLayer))
            {
                Debug.Log($"Raycast hit {hit.collider.name} at {hit.point}");
                Paint(hit.point, hit.normal);
            }
        }
    }

    void Paint(Vector3 position, Vector3 normal)
    {
        Debug.Log($"Painting at position {position} with normal {normal}");

        // Create a decal object
        GameObject paintDecal = new GameObject("PaintDecal");
        paintDecal.transform.position = position;
        paintDecal.transform.rotation = Quaternion.LookRotation(normal);

        // Add Decal Projector
        var decalProjector = paintDecal.AddComponent<UnityEngine.Rendering.HighDefinition.DecalProjector>();
        decalProjector.material = paintMaterial;
        decalProjector.size = new Vector3(brushSize, brushSize, 0.1f); // Set brush size
        decalProjector.fadeFactor = 1.0f;

        // Destroy the decal after a while
        Destroy(paintDecal, 10f);
    }
}
