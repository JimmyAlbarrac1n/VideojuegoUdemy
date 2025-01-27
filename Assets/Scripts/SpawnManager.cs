using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance{ get; private set;}
    public GameObject enemyprefab;

    public float spawnTimer = 2f;
    void Awake()
    {
        Instance = this;
    }

    //Iniciar la generación
    public void StarSpawn()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnTimer);
    }

    //Encargado de general el enemigo
    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyprefab, transform.position, Quaternion.identity);
      
    }
    //Para la generación
    public void StopSpawn()
    {
        CancelInvoke("SpawnEnemy");
    }
}
