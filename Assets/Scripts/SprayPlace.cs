using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.InputSystem; // Add this for the new Input System



public class SprayPlace : MonoBehaviour
{
    [Header("First Arm Settings")]
    [SerializeField] private Transform sprayNozzle1;
    [SerializeField] private LayerMask layerMask1;
    [SerializeField] private GameObject decalPrefab1; // Decal prefab for Arm 1

    [Header("Second Arm Settings")]
    [SerializeField] private Transform sprayNozzle2;
    [SerializeField] private LayerMask layerMask2;
    [SerializeField] private GameObject decalPrefab2; // Decal prefab for Arm 2

    [Header("Shared Settings")]
    [SerializeField] private Vector3 decalSize = new Vector3(0.2f, 0.2f, 0.1f);
    [SerializeField] private float sprayDistance = 5f;

    private InputAction sprayAction;

    void Awake()
    {
        // Set up the input action for spraying
        Debug.Log("SprayPlace: Initializing spray action.");
        sprayAction = new InputAction("Spray", binding: "<Mouse>/leftButton");
        sprayAction.Enable();
        Debug.Log("SprayPlace: Spray action enabled.");
    }

    void OnDestroy()
    {
        // Disable the input action
        Debug.Log("SprayPlace: Disabling spray action.");
        sprayAction.Disable();
    }

    void Update()
    {
        // Log that Update is running
        Debug.Log("SprayPlace: Update running.");

        // Perform raycasting for Arm 1
        Debug.Log("SprayPlace: Performing spray for Arm 1.");
        PerformSpray(sprayNozzle1, layerMask1, decalPrefab1, "Arm 1");

        // Perform raycasting for Arm 2
        Debug.Log("SprayPlace: Performing spray for Arm 2.");
        PerformSpray(sprayNozzle2, layerMask2, decalPrefab2, "Arm 2");
    }

    private void PerformSpray(Transform sprayNozzle, LayerMask layerMask, GameObject decalPrefab, string armName)
    {
        if (sprayNozzle == null)
        {
            Debug.LogError($"{armName}: Spray nozzle is not assigned.");
            return;
        }

        Ray ray = new Ray(sprayNozzle.position, -sprayNozzle.up);
        Debug.DrawRay(ray.origin, ray.direction * sprayDistance, Color.red);
        Debug.Log($"{armName}: Raycast drawn from {ray.origin} in direction {ray.direction}.");

        if (sprayAction.ReadValue<float>() > 0)
        {
            Debug.Log($"{armName}: Spray button pressed.");

            if (Physics.Raycast(ray, out RaycastHit hit, sprayDistance, layerMask))
            {
                Debug.Log($"{armName}: Raycast hit {hit.collider.name} at {hit.point}.");
                PlaceDecal(hit.point, hit.normal, hit.collider.transform, decalPrefab, armName);
            }
            else
            {
                Debug.Log($"{armName}: Raycast did not hit anything.");
            }
        }
        else
        {
            Debug.Log($"{armName}: Spray button not pressed.");
        }
    }

    private void PlaceDecal(Vector3 position, Vector3 normal, Transform parent, GameObject decalPrefab, string armName)
    {
        Debug.Log($"{armName}: Placing decal at {position} with normal {normal}.");

        // Slightly offset the decal from the surface to prevent z-fighting
        position += normal * 0.01f;

        GameObject decalInstance = Instantiate(decalPrefab, position, Quaternion.LookRotation(normal));
        if (decalInstance != null)
        {
            Debug.Log($"{armName}: Decal instantiated at {position}.");

            DecalProjector decalProjector = decalInstance.GetComponent<DecalProjector>();
            if (decalProjector != null)
            {
                decalProjector.size = decalSize;
                Debug.Log($"{armName}: Decal size set to {decalProjector.size}.");
            }
            else
            {
                Debug.LogError($"{armName}: No DecalProjector found on decal prefab.");
            }

            decalInstance.transform.SetParent(parent, worldPositionStays: true);
            Debug.Log($"{armName}: Decal parent set to {parent.name}.");
        }
        else
        {
            Debug.LogError($"{armName}: Failed to instantiate decal prefab.");
        }
    }
}
