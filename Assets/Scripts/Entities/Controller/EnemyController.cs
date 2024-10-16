using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MainController
{
    [SerializeField] private float followRange = 15f;   // 추적 범위
    [SerializeField] private float shootRange = 10f;    // 사정 거리
    private int layerMaskLevel;     // 벽을 감지하기 위한 레이어 마스크
    private int layerMaskTarget;    // 플레이어를 감지하기 위한 레이어 마스크

    protected override void Start()
    {
        base.Start();
        layerMaskLevel = LayerMask.NameToLayer("Level");
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    private void FixedUpdate()
    {
        // 플레이어와의 거리에 따라 상태 갱신
        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);
    }

    // 적과 타겟 사이 거리 계산
    private float DistanceToTarget()
    {
        //Debug.Log(transform.position + " " + ClosestTarget.position);
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }
    // 타겟 향하는 방향 벡터 반환
    private Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
    }
    private void UpdateEnemyState(float distance, Vector2 direction)
    {
        IsAttacking = false; // 기본적으로 공격 상태를 false로 설정

        // 추적 범위 내에 있는지 확인
        if (distance <= followRange)
        {
            // 가까운지 확인
            CheckIfNear(distance, direction);
        }
    }

    private void CheckIfNear(float distance, Vector2 direction)
    {
        // 사정 거리 내에 있으면 발사
        if (distance <= shootRange)
        {
            TryShootAtTarget(direction);
        }
        else
        {
            CallMoveEvent(direction); // 사정거리 밖이지만 추적 범위 내에 있을 경우, 타겟 쪽으로 이동
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        // 몬스터 위치에서 direction 방향으로 레이 발사
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, GetLayerMaskForRaycast());
        Debug.DrawRay(transform.position, direction, Color.red );

        // 벽에 맞은게 아니라 실제 플레이어에 맞았는지 확인
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
        // "Level" 레이어와 타겟 레이어 모두를 포함하는 LayerMask를 반환
        return (1 << layerMaskLevel) | layerMaskTarget;
    }

    private bool IsTargetHit(RaycastHit2D hit)
    {
        // RaycastHit2D 결과를 바탕으로 실제 타겟을 명중했는지 확인
        return hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer));
    }

    private void PerformAttackAction(Vector2 direction)
    {
        // 타겟을 정확히 명중했을 경우의 행동을 정의
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); // 공격 중에는 이동 중지
        IsAttacking = true;
    }
}
