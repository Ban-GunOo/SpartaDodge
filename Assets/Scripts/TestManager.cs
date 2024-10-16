using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;

    public ObjectPooled ObjectPool { get; private set; }



    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
       
        ObjectPool = GetComponent<ObjectPooled>();
    }

}

