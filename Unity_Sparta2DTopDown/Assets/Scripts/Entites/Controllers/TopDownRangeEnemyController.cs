using System;
using UnityEngine;
public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange = 15f;
    [SerializeField][Range(0f, 100f)] private float shootRange = 10f;

    private int layerMaskTarget;

    protected override void Start()
    {
        base.Start();
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);
    }

    private void UpdateEnemyState(float distanceToTarget, Vector2 directionToTarget)
    {
        IsAttacking = false;
        if(distanceToTarget < followRange)
        {
            CheckifNear(distanceToTarget, directionToTarget);
        }
    }

    private void CheckifNear(float distance, Vector2 direction)
    {
        if(distance <= shootRange)
        {
            TryShootAtTarget(direction);
        }
        else
        {
            CallMoveEvent(direction);
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, layerMaskTarget); // 오리진, 방향, 거리

        if(hit.collider != null)
        {
            PerformAttackAction(direction);
        }
        else
        {
            CallMoveEvent(direction);
        }
    }

    private void PerformAttackAction(Vector2 direction)
    {
        // 타겟을 정확히 명중했을 경우의 행동을 정의
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); // 공격 중에는 이동을 멈춥니다.
        IsAttacking = true;
    }
}