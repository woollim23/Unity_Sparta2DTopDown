using UnityEngine;

[CreateAssetMenu(fileName = "RangeAttackSO", menuName = "TopDownController/Attack/Ranged", order = 1)]
public class RangedAttackSO : AttackSO
{
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public float duration;
    public float spread;
    public int numberOfProjectilesPerShot;
    public float multipleProjectilesAngle;
    public Color projectileColor;
}