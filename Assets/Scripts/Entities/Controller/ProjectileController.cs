using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;  // 벽과의 충돌을 위한 레이어
    [SerializeField] private LayerMask targetLayer;  // 적과의 충돌을 위한 레이어

    private RangedAttackSO attackData;  // 발사체의 공격 데이터를 저장
    private float currentDuration;  // 발사체가 살아있는 시간
    private Vector2 direction;  // 발사체가 나아갈 방향
    private bool isReady;  // 발사체가 준비되었는지 여부

    private Rigidbody2D _rigidbody;  // 물리적 이동을 위한 Rigidbody2D
    private SpriteRenderer _spriteRenderer;  // 발사체의 스프라이트 렌더러
    private TrailRenderer _trailRenderer;  // 발사체의 잔상 효과

    public bool fxOnDestroy = true;  // 파괴 시 이펙트를 생성할지 여부

    private ObjectPool _objectPool;  // 오브젝트 풀을 참조

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();  // 오브젝트 풀을 찾아 참조
    }

    private void Update()
    {
        if (!isReady) return;

        currentDuration += Time.deltaTime;

        // 발사체가 최대 지속 시간을 초과하면 파괴
        if (currentDuration > attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // 발사체 이동 처리
        _rigidbody.velocity = direction * attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽과 충돌한 경우
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestroyProjectile(destroyPosition, fxOnDestroy);
        }
        // 적과 충돌한 경우
        else if (IsLayerMatched(targetLayer.value, collision.gameObject.layer))
        {
            // 충돌한 오브젝트에서 HealthSystem 컴포넌트를 가져오기
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                // 충돌한 오브젝트의 체력 감소시키기
                healthSystem.ChangeHealth(-attackData.power);
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    // 발사체가 특정 레이어와 충돌했는지 확인하는 메서드
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    // 발사체를 초기화하는 메서드 (발사 방향 및 공격 데이터 설정)
    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        // 발사체 속성 설정 (크기, 색상)
        UpdateProjectileSprite();
        _trailRenderer.Clear();  // 발사체 이동 중에 남은 자취를 초기화
        currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor;

        // 발사체의 방향 설정
        transform.right = this.direction;

        isReady = true;  // 발사체 준비 완료
    }

    // 발사체의 크기를 설정하는 메서드
    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }

    // 발사체 파괴 메서드 (필요 시 이펙트 생성)
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // TODO: ParticleSystem 등을 이용한 파티클 이펙트 생성 가능
        }
        // 오브젝트 풀에 발사체를 반환
        //_objectPool.SpawnFromPool(tag);
        gameObject.SetActive(false);
    }
}
