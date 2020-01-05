using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnObstacle", 3);
    }

    // Update is called once per frame
    void Update()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);

        if (playerControllerScript.isGameOver == false)
        {
            Invoke("SpawnObstacle", Random.Range(3, 5));
        }
    }
}
