using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/Enemy/EnemyStats", order = 1)]

public class EnemyStats : ScriptableObject
{
    public float attackRange; // ���� ��Ÿ�
    public float attackDamage; // ���ݷ�
    public float attackCooldown; // ���� ��Ÿ��
    public float moveSpeed; // �̵� �ӵ�

    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public EnemyStats BatStats;
}
