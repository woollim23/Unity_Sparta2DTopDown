using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;


    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        controller.OnAttackEvent += OnShoot;
        // OnLookEvent에 이제 두개가 등록되는 것 - 하나는 TopDownAimRotation.OnAim(Vec2)
        // 한 개의 델리게이트에 여러 개의 함수가 등록되어있는 것을 multicast delegate라고 함.
        // Action이나 Func도 델리게이트의 일종

        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        aimDirection = direction;
    }

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        float projectileAngleSpace = rangedAttackSO.multipleProjectilesAngle;
        int numberOfProjectilesPerShot = rangedAttackSO.numberOfProjectilesPerShot;

        // 중간부터 펼쳐지는게 아니라 minangle부터 커지면서 쏘는 것으로 설계 
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectileAngleSpace + 0.5f * rangedAttackSO.multipleProjectilesAngle;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + i * projectileAngleSpace;
            // 그냥 올라가면 재미없으니 랜덤으로 변하는 randomSpread를 넣음
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;
            CreateProjectile(rangedAttackSO, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {

        GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool(rangedAttackSO.bulletNameTag);

        // 발사체 기본 세팅
        obj.transform.position = projectileSpawnPosition.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);

        // 다음강에서 개선 시 활용할 코드
        // obj.SetActive(true);
    }

    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        // 벡터 회전하기 : 쿼터니언 * 벡터 순
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}