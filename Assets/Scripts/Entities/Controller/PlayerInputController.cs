using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerInputController : MainController
{
    private Camera _camera;

    // ��ӹ��� Awake���� �⺻������ ������ ���� ����
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main; 
    }

    // �̵� �Է� ó��
    // ���� �̵� ó���� PlayerMovement����
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput); 
    }

    // ���콺 �Ǵ� ��Ʈ�ѷ� ���� ó��
    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); 
        newAim = (worldPos - (Vector2)transform.position).normalized; 

        CallLookEvent(newAim); 
    }


    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            IsAttacking = true;
            CallAttackEvent(stats.CurrentStat.attackSO); 
        }
        else
        {
            IsAttacking = false;
        }

    }
}
