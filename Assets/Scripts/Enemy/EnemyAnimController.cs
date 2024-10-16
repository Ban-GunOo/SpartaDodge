using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;





namespace Assets.Scripts.Enemy
{


    public class EnemyAnimController : MonoBehaviour
    {
        private Animator animator;
        private BaseEnemy baseEnemy;


        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsHit = Animator.StringToHash("IsHit");
        private static readonly int Attack = Animator.StringToHash("Attack");

        private readonly float magnituteThreshold = 0.5f;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            baseEnemy = GetComponent<BaseEnemy>();

        }

        private void Start()
        {
            baseEnemy.OnMoveEvent += Move;
            baseEnemy.OnAttackEvent += Attacking;

        }


        private void Move(Vector2 obj)
        {
            animator.SetBool(IsWalking, obj.magnitude > magnituteThreshold);
        }
        
        private void Attacking()
        {

            animator.SetTrigger(Attack);    
        }
    }
}
