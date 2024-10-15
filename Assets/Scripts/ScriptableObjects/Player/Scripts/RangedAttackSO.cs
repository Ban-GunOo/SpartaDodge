using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    [Header("Ranged Attack Data")]
    public string bulletNameTag;  // 총알 태그
    public float duration;  // 발사체의 지속 시간
    public float spread;  // 발사체의 퍼짐 정도
    public int numberOfProjectilesPerShot;  // 한 번에 발사할 발사체의 수
    public float multipleProjectilesAngle;  // 여러 발사체의 각도
    public Color projectileColor;  // 발사체 색상


}
