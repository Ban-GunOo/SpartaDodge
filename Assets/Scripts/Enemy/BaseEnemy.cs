using System;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnAttackEvent;
    [SerializeField] protected GameObject TargetPlayer;

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);

    }

    public void CallAttackEvent() 
    { 
        OnAttackEvent?.Invoke(); 
    }

    
    protected float DistanceToTarget()
    {

        return Vector2.Distance(transform.position, TargetPlayer.transform.position);

    }

    protected Vector2 DirectionToTarget()
    {

        return (TargetPlayer.transform.position - transform.position).normalized;
    }


}


