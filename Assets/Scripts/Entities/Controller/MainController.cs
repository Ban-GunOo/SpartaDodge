using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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

    protected CharacterStatHandler stats { get; private set; }
    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatHandler>();
        // �ʿ��� �ʱ�ȭ �۾��� ����ٰ�
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        // TODO:: MAGIC NUMBER ����
        if (timeSinceLastAttack < stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (IsAttacking && timeSinceLastAttack >= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0f;
            // ���� ������ ������ attackSO����
            CallAttackEvent(stats.CurrentStat.attackSO);

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

    public void CallAttackEvent(AttackSO attackSO)
    {
        // PlayerShooting���� ����� OnShoot �޼ҵ尡 �����Ǿ� ����.
        if (attackSO != null)
        {
            // PlayerShooting���� ����� OnShoot �޼ҵ尡 �����Ǿ� ����.
            OnAttackEvent?.Invoke(attackSO);
        }
        else
        {
            Debug.LogError("AttackSO�� null��.");
        }
    }
}
