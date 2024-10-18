using Assets.Scripts.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Vector2를 인자로 받는 함수
    public event Action<Vector2> OnLookEvent; // Action은 무조건 void만 반환

    public event Action<AttackSO> OnAttackEvent;

    protected bool IsAttacking { get; set; }

    private float timeSinceLastAttack = float.MaxValue;

    // protected 프로퍼티를 한 이융 : 나만 바꾸고 싶지만 가져가는 건 내 상속받는 클래스들도 볼 수 있게!
    protected CharacterStatHandler stats { get; private set; }

    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatHandler>();
    }

    private void Update()
    {
        HandleAttckDelay();
    }

    private void HandleAttckDelay()
    {
        
        if(timeSinceLastAttack < stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if(IsAttacking && timeSinceLastAttack >= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0f;
            CallAttckEvent(stats.CurrentStat.attackSO);

        }
    }


    public void CallMoveEvent(Vector2 direction)
    {// move 이벤트가 발생했을 때 Invoke 하는 함수
        OnMoveEvent?.Invoke(direction); // ?. 없으면 말고, 있으면 실행한다
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    private void CallAttckEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }
}
