using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerInputController : MainController
{
    private Camera _camera;

    // 상속받은 Awake에서 기본적으로 설정된 것을 유지
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main; 
    }

    // 이동 입력 처리
    // 실제 이동 처리는 PlayerMovement에서
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput); 
    }

    // 마우스 또는 컨트롤러 조준 처리
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
