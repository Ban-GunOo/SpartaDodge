using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "TopDownController/Attacks/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    [Header("Ranged Attack Data")]
    public string bulletNameTag;  // �Ѿ� �±�
    public float duration;  // �߻�ü�� ���� �ð�
    public float spread;  // �߻�ü�� ���� ����
    public int numberOfProjectilesPerShot;  // �� ���� �߻��� �߻�ü�� ��
    public float multipleProjectilesAngle;  // ���� �߻�ü�� ����
    public Color projectileColor;  // �߻�ü ����


}
