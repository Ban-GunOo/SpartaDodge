using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
  
    
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
            attackRange = enemyStats.attackRange; // 사거리 
            moveSpeed = enemyStats.moveSpeed; // 이동속도
        }
    }

    private void Update()
    {
        HandlePlayerInteraction(DistanceToTarget());
    }

    private void HandlePlayerInteraction(float distance)
    {

        RotateTowards(DirectionToTarget());
        Debug.Log(distance);
        if (distance <= attackRange) 
        {
            TryAttack(); 
            rb.velocity = Vector2.zero; 
        }
        else 
        {
            MoveTowardsPlayer(distance, DirectionToTarget()); 
        }
    }

    private void MoveTowardsPlayer(float distance , Vector2 direction)
    {

        Debug.Log(distance > attackRange);
        if (distance > attackRange)
        {
            Debug.Log(moveSpeed);
            rb.velocity = direction * moveSpeed;
            
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


