using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "TopDownController/Attack/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")] // 카데고리
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    [Header("Knock Back Info")]
    public bool isOnKnockBack;
    public float knockbackPower;
    public float knockbackTime;
}
