using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPool : MonoBehaviour
{
    
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {

        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in Pools)
        {

            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {

                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, GameObject spawn)
    {
       
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        // 사용 가능한 오브젝트가 없을 경우
        if (PoolDictionary[tag].Count == 0)
        {
            // 해당 풀의 정보를 가져옴
            Pool pool = Pools.Find(p => p.tag == tag);
            if (pool != null)
            {
                // 새로운 오브젝트를 생성하고 큐에 추가
                GameObject obj = Instantiate(pool.prefab, spawn.transform);
                obj.SetActive(false);
                PoolDictionary[tag].Enqueue(obj); // 새로 생성한 오브젝트를 큐에 추가
            }
        }

        GameObject reusedObject = PoolDictionary[tag].Dequeue();
        reusedObject.transform.position = spawn.transform.position;
        PoolDictionary[tag].Enqueue(reusedObject);
        reusedObject.SetActive(true);
        return reusedObject;
    }
}




