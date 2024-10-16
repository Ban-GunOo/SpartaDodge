using UnityEngine;

// ĳ���� �̵��� ���� �����Դϴ�.
public class EnemyMovement : MonoBehaviour
{
    // ������ �̵��� ó���� ������Ʈ
    private EnemyController _controller; // �Է��� �޾� �̵� �̺�Ʈ�� ó���ϴ� ��Ʈ�ѷ�
    private Rigidbody2D _rigidbody; // ���� �̵��� ���� Rigidbody2D
    private CharacterStatHandler _characterStatHandler; // ĳ������ �ӵ� �� ������ �����ϴ� �ڵ鷯

    private Vector2 _movementDirection = Vector2.zero; // �̵� ���� ����

    private void Awake()
    {
        // ���� ���� ������Ʈ���� �ʿ��� ������Ʈ ��������
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
