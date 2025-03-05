using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject wallPrefab;      // Prefab for the wall
    public GameObject floorPrefab;     // Prefab for the floor
    public GameObject rampPrefab;      // Prefab for the ramp
    public GameObject conePrefab;      // Prefab for the cone

    public float buildDistance = 5f;  // Distance from the player where the structure is placed
    public LayerMask buildLayer;      // Layer mask to ensure structures are built on valid surfaces
    public float structureHeight = 2f; // Height of structures (e.g., walls)

    void Update()
    {
        // Build Wall
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Press "1" to build a wall
        {
            BuildStructure(wallPrefab);
        }

        // Build Floor
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Press "2" to build a floor
        {
            BuildStructure(floorPrefab);
        }

        // Build Ramp
        if (Input.GetKeyDown(KeyCode.Alpha3)) // Press "3" to build a ramp
        {
            BuildStructure(rampPrefab);
        }

        // Build Cone
        if (Input.GetKeyDown(KeyCode.Alpha4)) // Press "4" to build a cone
        {
            BuildStructure(conePrefab);
        }
    }

    void BuildStructure(GameObject structurePrefab)
    {
        // Calculate the build position in front of the player
        Vector3 buildPosition = transform.position + transform.forward * buildDistance;

        // Check if the build position is valid (e.g., on the ground or on top of another structure)
        if (IsValidBuildPosition(buildPosition))
        {
            // Instantiate the structure at the build position
            Instantiate(structurePrefab, buildPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("Invalid build position!");
        }
    }

    bool IsValidBuildPosition(Vector3 position)
    {
        // Check if the position is on the ground or on top of another structure
        if (Physics.Raycast(position, -Vector3.up, out RaycastHit hit, structureHeight, buildLayer))
        {
            // Check if the hit object is a structure or the ground
            if (hit.collider.CompareTag("Structure") || hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
}