using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private CharacterStatHandler statsHandler;
    private bool isAttacked = false;

    // 체력이 변했을 때 할 행동들을 정의하고 적용 가능
    public event Action OnDamage;
    public event Action OnDeath;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => statsHandler.CurrentStat.maxHealth;

    private void Awake()
    {
        statsHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        // 초기 체력 최대값으로 설정
        CurrentHealth = MaxHealth;
    }

    public bool ChangeHealth(float change)
    {
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0f)
        {
            CallDeath();    // 사망 이벤트 호출
            return true;
        }
        // 체력 변동 값이 음수면
        if (change < 0)
        {
            OnDamage?.Invoke(); // 데미지 이벤트 호출
        }
        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}