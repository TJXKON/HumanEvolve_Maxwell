using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryRespawnController : MonoBehaviour
{
     public GameObject cherryPrefab;
    public float respawnTime = 30f; // Time in seconds before respawn

    // Define an array of spawn positions
    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(9.97f, -0.54f, 0f),
        new Vector3(34.12f, 2.17f, 1f),
        new Vector3(43.95f, -0.52f, 0f)
    };

    private bool isRespawnTimerActive = false; // Flag to track if the respawn timer is active

    private static CherryRespawnController instance;

    public static CherryRespawnController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CherryRespawnController>();
            }
            return instance;
        }
    }

    private void Start()
    {
        
        StartRespawnTimer();
    }

    public void StartRespawnTimer()
    {
       
        if (!isRespawnTimerActive)
        {
            StartCoroutine(RespawnCherry());
        }
    }

    private IEnumerator RespawnCherry()
    {
      
        isRespawnTimerActive = true;

        yield return new WaitForSeconds(respawnTime);

       
        Vector3 randomSpawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

        // Instantiate the cherry prefab at the randomly chosen spawn position
        GameObject newCherry = Instantiate(cherryPrefab, randomSpawnPosition, Quaternion.identity);

        newCherry.SetActive(true); 

        // Reset the flag to indicate that the respawn timer is no longer active
        isRespawnTimerActive = false;
    }
}
