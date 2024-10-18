using Assets.Scripts.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Vector2�� ���ڷ� �޴� �Լ�
    public event Action<Vector2> OnLookEvent; // Action�� ������ void�� ��ȯ

    public event Action<AttackSO> OnAttackEvent;

    protected bool IsAttacking { get; set; }

    private float timeSinceLastAttack = float.MaxValue;

    // protected ������Ƽ�� �� ���� : ���� �ٲٰ� ������ �������� �� �� ��ӹ޴� Ŭ�����鵵 �� �� �ְ�!
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
    {// move �̺�Ʈ�� �߻����� �� Invoke �ϴ� �Լ�
        OnMoveEvent?.Invoke(direction); // ?. ������ ����, ������ �����Ѵ�
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
