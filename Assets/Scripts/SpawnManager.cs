using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject spawnPoint;
    private ObjectPooled pool;
    private float time;
    private GameObject[] spawnPointArray;

    private void Awake()
    {

        spawnPointArray = GameObject.FindGameObjectsWithTag("SpawnPoint");
        pool = GetComponent<ObjectPooled>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
         time += Time.deltaTime;


        if(time > 1)
        {
            SpawnEnemy();
            time = 0;
        }


    }

    public void SpawnEnemy()
    {

        if (spawnPointArray.Length == 0)
        {
            Debug.LogWarning("Spawn point array is empty!");
            return; // 배열이 비어있으면 메서드를 종료
        }
        int RandomIndex = Random.Range(0, spawnPointArray.Length);
        
        pool.SpawnFromPool("Bat", spawnPointArray[RandomIndex]);


    }
    
}
