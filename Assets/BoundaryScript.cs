using System;
using System.Security.Cryptography;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    
    
    public static event Action<GameObject> OnBoundaryCreated;
    
    
    private Transform playerTransform;

    public static BoundaryScript Instance;
    
    // Referencing the Boundary game object to set it active if there is 10m of radius between them
    [SerializeField] private GameObject boundaryObject;

    private GameObject boundaryClone;
    
    public bool isBoss = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Get the player transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (!playerTransform)
        {
            Debug.LogError("Player object not found!");
        }
        
        // Technique to find Inactive object
        //boundaryObject = FindInactiveObjectByTag("BossBoundary");
        if(!boundaryObject)
            Debug.Log("Unable to find Boundary Object!");
    }
    
    private void Update()
    {
        // Only check distance if playerTransform and boundaryObject are not null
        if (transform.CompareTag("88") && playerTransform && boundaryObject)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            Debug.Log($"Distance to player: {distance}");

            if (isBoss && distance <= 20f && !boundaryObject.activeInHierarchy)
            {
                // Set the position of the boundary to be halfway between the player and the boss
                Vector2 middlePoint = (transform.position + playerTransform.position) / 2;
                //boundaryObject.transform.position = middlePoint;
                
                // Whenever the boundary is made, it should tell player of its presence

                if (boundaryClone == null)
                {
                    boundaryClone = Instantiate(boundaryObject);
                    Debug.Log("Making Boundary Active!");
                    boundaryClone.transform.position = middlePoint;
                    boundaryClone.SetActive(true);  // Ensure the boundary is active
                    OnBoundaryCreated?.Invoke(boundaryClone);
                }
                //boundaryObject.SetActive(true);
            }
        }
    }

    
    public void DeactivateBoundary()
    {
        Destroy(boundaryClone);
    }

    
    // Find that Game Boundary Inactive GameObject 
    private GameObject FindInactiveObjectByTag(string id)
    {
        var objects = FindObjectsOfType<Transform>(true);  // This searches for all Transforms, including inactive ones.
        foreach (Transform t in objects)
        {
            if (t.CompareTag(id))
                return t.gameObject;
        }
        return null;
    }
}