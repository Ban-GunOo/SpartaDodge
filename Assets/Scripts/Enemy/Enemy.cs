using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
  
    [SerializeField] private Transform player;
    [SerializeField] private EnemyStats enemyStats; // ScriptableObject 참조
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
            attackDamage = enemyStats.attackDamage; // 공격력
            attackCooldown = enemyStats.attackCooldown; // 공격 쿨타임
            attackRange = enemyStats.attackRange; // 사거리 초기화
            moveSpeed = enemyStats.moveSpeed; // 이동속도
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


