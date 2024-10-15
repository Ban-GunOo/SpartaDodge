using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // 이동, 조준, 공격 이벤트
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    protected bool IsAttacking { get; set; }
    private float timeSinceLastAttack = float.MaxValue;

    [SerializeField] private AttackSO currentAttackSO;

    protected virtual void Awake()
    {
        // 필요한 초기화 작업은 여기다가
    }

    private void Update()
    {
        HandleAttackCooldown();
    }

    private void HandleAttackCooldown()
    {
        // 공격 딜레이 관리 
        // 혹시몰라서 공격도 미리 긁어온상태
        if (IsAttacking && timeSinceLastAttack >= currentAttackSO.delay) // AttackSO에서 딜레이 사용
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent(); 
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
        }
    }

    // 외부에서 호출
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
