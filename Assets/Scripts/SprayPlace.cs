using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.InputSystem; // Add this for the new Input System



public class SprayPlace : MonoBehaviour
{
   [SerializeField] private Transform sprayNozzle; // The tip of the robotic arm (source of the ray)
    [SerializeField] private LayerMask layerMask; // Layer mask for paintable surfaces
    [SerializeField] private GameObject decalPrefab; // Prefab for the decal
    [SerializeField] private Vector3 decalSize = new Vector3(0.2f, 0.2f, 0.1f); // Size of the decal
    [SerializeField] private float sprayDistance = 5f; // Maximum distance for spraying
    

    private InputAction sprayAction; // InputAction for spraying
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        // Set up the InputAction for spraying
        sprayAction = new InputAction("Spray", binding: "<Mouse>/leftButton");
        sprayAction.Enable();
    }

    private void OnDestroy()
    {
        // Cleanup the InputAction
        sprayAction.Disable();
    }

    private void Update()
    {

         
        // Perform the raycast from the spray nozzle
    ray = new Ray(sprayNozzle.position, -sprayNozzle.up);
   Debug.DrawRay(ray.origin, ray.direction * sprayDistance, Color.red);

    Debug.Log($"SprayPlace: Ray created from {ray.origin} in direction {ray.direction}");

    // Check if the spray button is pressed and the raycast hits a target
    if (sprayAction.ReadValue<float>() > 0)
    {
        Debug.Log("SprayPlace: Spray button pressed.");

        if (Physics.Raycast(ray, out hit, sprayDistance, layerMask))
        {
            Debug.Log($"SprayPlace: Raycast hit {hit.collider.name} at {hit.point}");
            PlaceDecal(hit.point, hit.normal);
        }
        else
        {
            Debug.Log("SprayPlace: Raycast did not hit anything.");
        }
    }
    }

    private void PlaceDecal(Vector3 position, Vector3 normal)
    {
         // Instantiate the decal prefab
    GameObject decalInstance = Instantiate(decalPrefab, position, Quaternion.LookRotation(normal));

    // Configure the Decal Projector
    DecalProjector decalProjector = decalInstance.GetComponent<DecalProjector>();
    if (decalProjector != null)
    {
        decalProjector.size = decalSize;
    }

    // Make the decal a child of the hit object to follow its animation
    decalInstance.transform.SetParent(hit.collider.transform, worldPositionStays: true);

    // Optional: Destroy the decal after some time to avoid clutter
    //Destroy(decalInstance, 10f);

    Debug.Log($"Decal placed at {position}, aligned with normal {normal}");
    }
}
