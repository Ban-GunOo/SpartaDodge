
using UnityEngine;


//public class EnemyAttack
//{
//    private void ShootProjectile(Transform target)
//    {
//        GameObject projectile = (projectilePrefab, transform.position, Quaternion.identity);

//        // 발사 방향 설정
//        Vector2 direction = (target.position - transform.position).normalized;
//        float randomSpreadAngle = Random.Range(-spread, spread);
//        direction = Quaternion.Euler(0, 0, randomSpreadAngle) * direction; // 스프레드 각도를 적용

//        // Projectile에 방향 설정 (Rigidbody2D 사용)
//        Projectile projectileScript = projectile.GetComponent<Projectile>();
//        if (projectileScript != null)
//        {
//            projectileScript.Initialize(direction); // projectile의 방향 초기화
//        }
//    }
//}

