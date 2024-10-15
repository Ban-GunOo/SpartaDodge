using UnityEngine;

// ĳ���� �̵��� ���� �����Դϴ�.
public class PlayerMovement : MonoBehaviour
{
    // ������ �̵��� ó���� ������Ʈ
    private PlayerInputController _controller; // �Է��� �޾� �̵� �̺�Ʈ�� ó���ϴ� ��Ʈ�ѷ�
    private Rigidbody2D _rigidbody; // ���� �̵��� ���� Rigidbody2D
    private CharacterStatHandler _characterStatHandler; // ĳ������ �ӵ� �� ������ �����ϴ� �ڵ鷯

    private Vector2 _movementDirection = Vector2.zero; // �̵� ���� ����

    private void Awake()
    {
        // ���� ���� ������Ʈ���� �ʿ��� ������Ʈ ��������
        _controller = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    private void OnEnable()
    {
        // �̵� �̺�Ʈ�� �����Ͽ� �̵� ������ ������Ʈ
        _controller.OnMoveEvent += UpdateMovementDirection;
    }

    private void OnDisable()
    {
        // �̵� �̺�Ʈ ���� ����
        _controller.OnMoveEvent -= UpdateMovementDirection;
    }

    private void UpdateMovementDirection(Vector2 direction)
    {
        // �̵� ���⸸ �����ϰ� ���� �̵��� FixedUpdate���� ó��
        _movementDirection = direction;
    }

    private void FixedUpdate()
    {
        // ���� ������Ʈ�� ���� �̵� ó��
        ApplyMovement(_movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        // ĳ������ ���ȿ��� �ӵ��� ������ �̵� ó��
        float speed = _characterStatHandler.CurrentStat.speed;
        _rigidbody.velocity = direction * speed; // Rigidbody2D�� �ӵ� ���� �����Ͽ� �̵�
    }
}
