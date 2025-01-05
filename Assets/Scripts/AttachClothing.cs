using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachClothing : MonoBehaviour
{
   [SerializeField] private GameObject armature;
    [SerializeField] private string boneName; // Name of the bone
    [SerializeField] private GameObject clothing;

    void Start()
    {
        if (armature == null || clothing == null)
        {
            Debug.LogError("Armature or clothing not assigned!");
            return;
        }

        // Search all bones for the specified name
        Transform bone = null;
        foreach (Transform child in armature.GetComponentsInChildren<Transform>())
        {
            if (child.name == boneName)
            {
                bone = child;
                break;
            }
        }

        if (bone == null)
        {
            Debug.LogError($"Bone '{boneName}' not found in the armature!");
            return;
        }

        // Match the clothing's world position/rotation with the bone before parenting
        clothing.transform.position = bone.position;
        clothing.transform.rotation = bone.rotation;

        // Parent the clothing to the bone
        clothing.transform.SetParent(bone);

        Debug.Log($"Clothing successfully attached to bone: {bone.name}");
    }

}
