using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShooting : MonoBehaviour
{
    public ObjectPool projectilePool;  // ����ü Ǯ�� �ý���
    [SerializeField] public Transform projectileSpawnPoint;    // ����ü �߻� ��ġ

    private EnemyController _controller;
    private PlayerAimRotation _aimRotation;

    private void Awake()
    {
        _controller = GetComponent<EnemyController>();
        _aimRotation = GetComponent<PlayerAimRotation>();
    }

    private void OnEnable()
    {
        // ���� �̺�Ʈ ����
        _controller.OnAttackEvent += OnShoot;
    }

    private void OnDisable()
    {
        // ���� �̺�Ʈ ���� ����
        _controller.OnAttackEvent -= OnShoot;
    }

    // ���� �޼���
    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        if (rangedAttackSO == null) return;
        Debug.Log(rangedAttackSO.target);

        //�߻簢��
        float projectilesAngleSpace = rangedAttackSO.multipleProjectilesAngle;
        //�߻�ü����
        int numberOfProjectilesPerShot = rangedAttackSO.numberOfProjectilesPerShot;
        //�߻� �ּ� ����
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackSO.multipleProjectilesAngle;

        // �߻� �� �� ���� ���� ����
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;
            // �߻�ü ����
            CreateProjectile(rangedAttackSO, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        // ȭ�� ����
        GameObject obj = projectilePool.SpawnFromPool(rangedAttackSO.bulletNameTag);

        // �߻�ü �⺻ ����
        obj.transform.position = projectileSpawnPoint.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();

        Vector2 aimDirection = _aimRotation.AimDirection;
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);


        obj.SetActive(true);

    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        // ���� ȸ���ϱ� : ���ʹϾ� * ���� ��
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
