using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShooting : MonoBehaviour
{
    public ObjectPool projectilePool;  // 투사체 풀링 시스템
    [SerializeField] public Transform projectileSpawnPoint;    // 투사체 발사 위치

    private EnemyController _controller;
    private PlayerAimRotation _aimRotation;

    private void Awake()
    {
        _controller = GetComponent<EnemyController>();
        _aimRotation = GetComponent<PlayerAimRotation>();
    }

    private void OnEnable()
    {
        // 공격 이벤트 구독
        _controller.OnAttackEvent += OnShoot;
    }

    private void OnDisable()
    {
        // 공격 이벤트 구독 해제
        _controller.OnAttackEvent -= OnShoot;
    }

    // 공격 메서드
    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        if (rangedAttackSO == null) return;
        Debug.Log(rangedAttackSO.target);

        //발사각도
        float projectilesAngleSpace = rangedAttackSO.multipleProjectilesAngle;
        //발사체개수
        int numberOfProjectilesPerShot = rangedAttackSO.numberOfProjectilesPerShot;
        //발사 최소 각도
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackSO.multipleProjectilesAngle;

        // 발사 할 때 마다 랜덤 각도
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;
            // 발사체 생성
            CreateProjectile(rangedAttackSO, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        // 화살 생성
        GameObject obj = projectilePool.SpawnFromPool(rangedAttackSO.bulletNameTag);

        // 발사체 기본 세팅
        obj.transform.position = projectileSpawnPoint.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();

        Vector2 aimDirection = _aimRotation.AimDirection;
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);


        obj.SetActive(true);

    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        // 벡터 회전하기 : 쿼터니언 * 벡터 순
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
