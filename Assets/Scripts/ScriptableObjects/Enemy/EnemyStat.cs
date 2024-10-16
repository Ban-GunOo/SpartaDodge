using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/Enemy/EnemyStats", order = 1)]

public class EnemyStats : ScriptableObject
{
    public float attackRange; // 공격 사거리
    public float attackDamage; // 공격력
    public float attackCooldown; // 공격 쿨타임
    public float moveSpeed; // 이동 속도

    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public EnemyStats BatStats;
}
