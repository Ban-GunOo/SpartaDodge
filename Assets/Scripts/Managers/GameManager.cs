using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform Player { get; private set; }
    public ObjectPool ObjectPool { get; private set; }
    [SerializeField] private string playerTag = "Player";

    private void Awake()
    {
        // �ϳ��� �����ǵ��� ����
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        Debug.Log(Player.position);

        ObjectPool = GetComponent<ObjectPool>();
    }
}
