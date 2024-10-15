using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // �̵�, ����, ���� �̺�Ʈ
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    protected bool IsAttacking { get; set; }
    private float timeSinceLastAttack = float.MaxValue;

    [SerializeField] private AttackSO currentAttackSO;

    protected virtual void Awake()
    {
        // �ʿ��� �ʱ�ȭ �۾��� ����ٰ�
    }

    private void Update()
    {
        HandleAttackCooldown();
    }

    private void HandleAttackCooldown()
    {
        // ���� ������ ���� 
        // Ȥ�ø��� ���ݵ� �̸� �ܾ�»���
        if (IsAttacking && timeSinceLastAttack >= currentAttackSO.delay) // AttackSO���� ������ ���
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent(); 
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
        }
    }

    // �ܺο��� ȣ��
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction); 
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction); 
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke(currentAttackSO);
    }
}
