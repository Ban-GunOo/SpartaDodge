using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
  
    [SerializeField] private Transform player;
    [SerializeField] private EnemyStats enemyStats; // ScriptableObject ����
    private float attackDamage;
    private float attackCooldown;
    private float attackRange; 
    private float lastAttackTime;
    private float moveSpeed;
    private Rigidbody2D rb; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


        if (enemyStats != null)
        {
            attackDamage = enemyStats.attackDamage; // ���ݷ�
            attackCooldown = enemyStats.attackCooldown; // ���� ��Ÿ��
            attackRange = enemyStats.attackRange; // ��Ÿ� �ʱ�ȭ
            moveSpeed = enemyStats.moveSpeed; // �̵��ӵ�
        }
    }

    private void Update()
    {
        HandlePlayerInteraction(player.position); 
    }

    private void HandlePlayerInteraction(Vector2 playerPosition)
    {
        
        float distance = Vector2.Distance(transform.position, playerPosition);

        if (distance <= attackRange) 
        {
            TryAttack(); 
            rb.velocity = Vector2.zero; 
        }
        else 
        {
            MoveTowardsPlayer(playerPosition); 
        }
    }

    private void MoveTowardsPlayer(Vector2 playerPosition)
    {
        float distance = Vector2.Distance(transform.position, playerPosition);

        if (distance > attackRange)
        {
            Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            RotateTowards(direction);
            CallMoveEvent(direction); 
        }
    }

    private void TryAttack()
    {
        if (CanAttack())
        {
            CallAttackEvent(); 
            lastAttackTime = Time.time;
        }
    }

    private bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    private void RotateTowards(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); 
    }
}


