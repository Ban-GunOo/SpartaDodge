using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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

    protected CharacterStatHandler stats { get; private set; }
    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatHandler>();
        // 필요한 초기화 작업은 여기다가
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        // TODO:: MAGIC NUMBER 수정
        if (timeSinceLastAttack < stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (IsAttacking && timeSinceLastAttack >= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0f;
            // 현재 장착된 무기의 attackSO전달
            CallAttackEvent(stats.CurrentStat.attackSO);

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

    public void CallAttackEvent(AttackSO attackSO)
    {
        // PlayerShooting에서 등록한 OnShoot 메소드가 구독되어 있음.
        if (attackSO != null)
        {
            // PlayerShooting에서 등록한 OnShoot 메소드가 구독되어 있음.
            OnAttackEvent?.Invoke(attackSO);
        }
        else
        {
            Debug.LogError("AttackSO가 null임.");
        }
    }
}
