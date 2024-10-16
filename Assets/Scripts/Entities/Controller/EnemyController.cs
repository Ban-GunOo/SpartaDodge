using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MainController
{
    [SerializeField] private float followRange = 15f;   // ���� ����
    [SerializeField] private float shootRange = 10f;    // ���� �Ÿ�
    private int layerMaskLevel;     // ���� �����ϱ� ���� ���̾� ����ũ
    private int layerMaskTarget;    // �÷��̾ �����ϱ� ���� ���̾� ����ũ

    protected override void Start()
    {
        base.Start();
        layerMaskLevel = LayerMask.NameToLayer("Level");
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    private void FixedUpdate()
    {
        // �÷��̾���� �Ÿ��� ���� ���� ����
        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);
    }

    // ���� Ÿ�� ���� �Ÿ� ���
    private float DistanceToTarget()
    {
        //Debug.Log(transform.position + " " + ClosestTarget.position);
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }
    // Ÿ�� ���ϴ� ���� ���� ��ȯ
    private Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
    private void UpdateEnemyState(float distance, Vector2 direction)
    {
        IsAttacking = false; // �⺻������ ���� ���¸� false�� ����

        // ���� ���� ���� �ִ��� Ȯ��
        if (distance <= followRange)
        {
            // ������� Ȯ��
            CheckIfNear(distance, direction);
        }
    }

    private void CheckIfNear(float distance, Vector2 direction)
    {
        // ���� �Ÿ� ���� ������ �߻�
        if (distance <= shootRange)
        {
            TryShootAtTarget(direction);
        }
        else
        {
            CallMoveEvent(direction); // �����Ÿ� �������� ���� ���� ���� ���� ���, Ÿ�� ������ �̵�
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        // ���� ��ġ���� direction �������� ���� �߻�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, GetLayerMaskForRaycast());
        Debug.DrawRay(transform.position, direction, Color.red );

        // ���� ������ �ƴ϶� ���� �÷��̾ �¾Ҵ��� Ȯ��
        if (IsTargetHit(hit))
        {
            PerformAttackAction(direction);
        }
        else
        {
            CallMoveEvent(direction);
        }
    }

    private int GetLayerMaskForRaycast()
    {
        // "Level" ���̾�� Ÿ�� ���̾� ��θ� �����ϴ� LayerMask�� ��ȯ
        return (1 << layerMaskLevel) | layerMaskTarget;
    }

    private bool IsTargetHit(RaycastHit2D hit)
    {
        // RaycastHit2D ����� �������� ���� Ÿ���� �����ߴ��� Ȯ��
        return hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer));
    }

    private void PerformAttackAction(Vector2 direction)
    {
        // Ÿ���� ��Ȯ�� �������� ����� �ൿ�� ����
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); // ���� �߿��� �̵� ����
        IsAttacking = true;
    }
}
