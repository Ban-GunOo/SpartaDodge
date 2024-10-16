using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    [SerializeField] private EnemyStats baseStats;

    public EnemyStats CurrentStat { get; private set; }

    private void Awake()
    {
        UpdateEnemyStats();
    }

    private void UpdateEnemyStats()
    {
        EnemyStats enemyStat = null;
        if (baseStats.BatStats != null)
        {
            enemyStat = Instantiate(baseStats.BatStats);
        }

        CurrentStat = new EnemyStats { BatStats = enemyStat };
        CurrentStat.attackRange = baseStats.attackRange;
        CurrentStat.attackDamage = baseStats.attackDamage;
        CurrentStat.attackCooldown = baseStats.attackCooldown;
        CurrentStat.moveSpeed = baseStats.moveSpeed;
        CurrentStat.maxHealth = baseStats.maxHealth;
        CurrentStat.speed = baseStats.speed;
    }
}
