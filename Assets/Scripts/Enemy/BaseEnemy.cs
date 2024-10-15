using System;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnAttackEvent;


    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);

    }

    public void CallAttackEvent() 
    { 
        OnAttackEvent?.Invoke(); 
    }

    

  
}


