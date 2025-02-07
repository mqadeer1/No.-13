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
    [SerializeField] private GameObject decalPrefab1;

    [Header("Second Arm Settings")]
    [SerializeField] private Transform sprayNozzle2;
    [SerializeField] private LayerMask layerMask2;
    [SerializeField] private GameObject decalPrefab2;

    [Header("Shared Settings")]
    [SerializeField] private Vector3 decalSize = new Vector3(0.2f, 0.2f, 0.1f);
    [SerializeField] private float sprayDistance = 5f;

    private InputAction sprayAction;
    private List<GameObject> decals = new List<GameObject>(); // Track all instantiated decals

    void Awake()
    {
        sprayAction = new InputAction("Spray", binding: "<Mouse>/leftButton");
        sprayAction.Enable();
    }

    void OnDestroy()
    {
        sprayAction.Disable();
    }

    void Update()
    {
        PerformSpray(sprayNozzle1, layerMask1, decalPrefab1, "Arm 1");
        PerformSpray(sprayNozzle2, layerMask2, decalPrefab2, "Arm 2");
    }

    private void PerformSpray(Transform sprayNozzle, LayerMask layerMask, GameObject decalPrefab, string armName)
    {
        if (sprayNozzle == null) return;

        Ray ray = new Ray(sprayNozzle.position, -sprayNozzle.up);
        if (sprayAction.ReadValue<float>() > 0 && Physics.Raycast(ray, out RaycastHit hit, sprayDistance, layerMask))
        {
            PlaceDecal(hit.point, hit.normal, hit.collider.transform, decalPrefab);
        }
    }

    private void PlaceDecal(Vector3 position, Vector3 normal, Transform parent, GameObject decalPrefab)
    {
        position += normal * 0.01f;
        GameObject decalInstance = Instantiate(decalPrefab, position, Quaternion.LookRotation(normal));
        if (decalInstance != null)
        {
            DecalProjector decalProjector = decalInstance.GetComponent<DecalProjector>();
            if (decalProjector != null)
            {
                decalProjector.size = decalSize;
            }
            decalInstance.transform.SetParent(parent, worldPositionStays: true);
            decals.Add(decalInstance); // Add decal to the list
        }
    }

    // Function to remove all decals
    public void RemoveAllDecals()
    {
        foreach (GameObject decal in decals)
        {
            if (decal != null)
            {
                Destroy(decal);
            }
        }
        decals.Clear(); // Clear the list
    }
}