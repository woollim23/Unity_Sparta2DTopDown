using UnityEngine;
using UnityEngine.UIElements;

public class TopDownEnemyController : TopDownController
{
    protected Transform ClosetTarget { get; private set; }
    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        ClosetTarget = GameManager.Instance.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosetTarget.position);
        // 값 두개만 넣으면 Vector2가 되는건가??
        // 꼭 Vector3를 써야하나? Vector2는 안되나?
    }

    protected Vector2 DirectionToTarget()
    {
        return (ClosetTarget.position - transform.position).normalized;
    }
}
