using UnityEngine;
using System.Collections.Generic;

public class CharacterStatHandler : MonoBehaviour
{
    // 기본 스탯과 추가스텟들을 계산해서 최종 스텟을 계산하는 로직
    // 지금은 기본스텟만

    [SerializeField] private CharacterStat baseStats;
    public CharacterStat CurrentStat { get; private set; }
    public List<CharacterStat> statsModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        CurrentStat = new CharacterStat { attackSO = attackSO };
        CurrentStat.statsChangeType = baseStats.statsChangeType;
        CurrentStat.maxHealth = baseStats.maxHealth;
        CurrentStat.speed = baseStats.speed;

    }
}