using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    public class CharacterStatHandler : MonoBehaviour
    {
        // 기본스텟과 추가스텟들을 계산해서 최종 스텟을 도출하는 로직이 있음
        // 근데 지금은 그냥 기본 스텟만

        [SerializeField] private CharacterStat baseStat;
        public CharacterStat CurrentStat { get; private set; }

        public List<CharacterStat> statModifiers = new List<CharacterStat>();

        private void Awake()
        {
            updateCharacterStat();
        }

        private void updateCharacterStat()
        {
            AttackSO attackSO = null;
            if(baseStat.attackSO != null)
            {
                attackSO = Instantiate(baseStat.attackSO);
            }

            CurrentStat = new CharacterStat { attackSO = attackSO };
            // TODO : 지금은 기본 능력치만 적용되지만, 앞으로는 능력치강화 기능이 적용된다.

            // 일단 기본 능력치를 복사한 것
            CurrentStat.statsChangeType = baseStat.statsChangeType;
            CurrentStat.maxHealth = baseStat.maxHealth;
            CurrentStat.speed = baseStat.speed;
        }
    }
}
