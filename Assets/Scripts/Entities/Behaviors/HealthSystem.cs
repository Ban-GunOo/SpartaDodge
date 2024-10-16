using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private CharacterStatHandler statsHandler;
    private bool isAttacked = false;

    // ü���� ������ �� �� �ൿ���� �����ϰ� ���� ����
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
        // �ʱ� ü�� �ִ밪���� ����
        CurrentHealth = MaxHealth;
    }

    public bool ChangeHealth(float change)
    {
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0f)
        {
            CallDeath();    // ��� �̺�Ʈ ȣ��
            return true;
        }
        // ü�� ���� ���� ������
        if (change < 0)
        {
            OnDamage?.Invoke(); // ������ �̺�Ʈ ȣ��
        }
        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}