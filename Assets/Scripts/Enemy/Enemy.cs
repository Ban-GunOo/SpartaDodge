using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseEnemy
{
    [SerializeField] private CharacterStatHandler characterStatHandler; // ScriptableObject ����
    private float attackDamage;
    private float attackCooldown;
    private float attackRange; 
    private float lastAttackTime;
    private float moveSpeed;
    private Rigidbody2D rb;

    private int layerMaskLevel;
    private int layerMaskTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


        if (characterStatHandler != null)
        {
            //attackDamage = enemyStats.attackDamage; // ���ݷ�
            //attackCooldown = enemyStats.attackCooldown; // ���� ��Ÿ��
            //attackRange = enemyStats.attackRange; // ��Ÿ� 
            //moveSpeed = enemyStats.moveSpeed; // �̵��ӵ�
            attackDamage = characterStatHandler.CurrentStat.attackSO.power;
            attackCooldown = characterStatHandler.CurrentStat.attackSO.delay;
            attackRange = characterStatHandler.CurrentStat.attackSO.size;
            moveSpeed = characterStatHandler.CurrentStat.speed;
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
            Debug.Log("�����㰡");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, DirectionToTarget(), attackRange);
            Debug.Log(hit.collider.name);
       
            if (hit.collider != null) // "Player" �±� Ȯ��
            {      
                Debug.Log("���ݼ���");
                CallAttackEvent();
                lastAttackTime = Time.time;
            }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)DirectionToTarget() * attackRange);
    }


}


