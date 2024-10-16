using UnityEngine;

// 캐릭터 이동에 사용될 예정입니다.
public class EnemyMovement : MonoBehaviour
{
    // 실제로 이동을 처리할 컴포넌트
    private EnemyController _controller; // 입력을 받아 이동 이벤트를 처리하는 컨트롤러
    private Rigidbody2D _rigidbody; // 물리 이동을 위한 Rigidbody2D
    private CharacterStatHandler _characterStatHandler; // 캐릭터의 속도 등 스탯을 관리하는 핸들러

    private Vector2 _movementDirection = Vector2.zero; // 이동 방향 저장

    private void Awake()
    {
        // 현재 게임 오브젝트에서 필요한 컴포넌트 가져오기
        _controller = GetComponent<EnemyController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        _controller.OnMoveEvent -= UpdateMovementDirection;
        _controller.OnMoveEvent += UpdateMovementDirection;

    }
    private void UpdateMovementDirection(Vector2 direction)
    {
        // 이동 방향만 설정하고 실제 이동은 FixedUpdate에서 처리
        _movementDirection = direction;
    }

    private void FixedUpdate()
    {
        // 물리 업데이트를 통해 이동 처리
        ApplyMovement(_movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        // 캐릭터의 스탯에서 속도를 가져와 이동 처리
        float speed = _characterStatHandler.CurrentStat.speed;
        _rigidbody.velocity = direction * speed; // Rigidbody2D의 속도 값을 변경하여 이동
    }
}
