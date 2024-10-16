using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;  // ������ �浹�� ���� ���̾�
    [SerializeField] private LayerMask targetLayer;  // ������ �浹�� ���� ���̾�

    private RangedAttackSO attackData;  // �߻�ü�� ���� �����͸� ����
    private float currentDuration;  // �߻�ü�� ����ִ� �ð�
    private Vector2 direction;  // �߻�ü�� ���ư� ����
    private bool isReady;  // �߻�ü�� �غ�Ǿ����� ����

    private Rigidbody2D _rigidbody;  // ������ �̵��� ���� Rigidbody2D
    private SpriteRenderer _spriteRenderer;  // �߻�ü�� ��������Ʈ ������
    private TrailRenderer _trailRenderer;  // �߻�ü�� �ܻ� ȿ��

    public bool fxOnDestroy = true;  // �ı� �� ����Ʈ�� �������� ����

    private ObjectPool _objectPool;  // ������Ʈ Ǯ�� ����

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();  // ������Ʈ Ǯ�� ã�� ����
    }

    private void Update()
    {
        if (!isReady) return;

        currentDuration += Time.deltaTime;

        // �߻�ü�� �ִ� ���� �ð��� �ʰ��ϸ� �ı�
        if (currentDuration > attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // �߻�ü �̵� ó��
        _rigidbody.velocity = direction * attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹�� ���
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestroyProjectile(destroyPosition, fxOnDestroy);
        }
        // ���� �浹�� ���
        else if (IsLayerMatched(targetLayer.value, collision.gameObject.layer))
        {
            // �浹�� ������Ʈ���� HealthSystem ������Ʈ�� ��������
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                // �浹�� ������Ʈ�� ü�� ���ҽ�Ű��
                healthSystem.ChangeHealth(-attackData.power);
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    // �߻�ü�� Ư�� ���̾�� �浹�ߴ��� Ȯ���ϴ� �޼���
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    // �߻�ü�� �ʱ�ȭ�ϴ� �޼��� (�߻� ���� �� ���� ������ ����)
    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        // �߻�ü �Ӽ� ���� (ũ��, ����)
        UpdateProjectileSprite();
        _trailRenderer.Clear();  // �߻�ü �̵� �߿� ���� ���븦 �ʱ�ȭ
        currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor;

        // �߻�ü�� ���� ����
        transform.right = this.direction;

        isReady = true;  // �߻�ü �غ� �Ϸ�
    }

    // �߻�ü�� ũ�⸦ �����ϴ� �޼���
    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }

    // �߻�ü �ı� �޼��� (�ʿ� �� ����Ʈ ����)
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // TODO: ParticleSystem ���� �̿��� ��ƼŬ ����Ʈ ���� ����
        }
        // ������Ʈ Ǯ�� �߻�ü�� ��ȯ
        //_objectPool.SpawnFromPool(tag);
        gameObject.SetActive(false);
    }
}
