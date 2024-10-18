using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    public enum StatsChangeType
    {
        Add, // 0
        Multiple, // 1
        Override // 2
    }

    // 데이터 폴더처럼 사용할 수 있게 만들어주는 Attribute
    [System.Serializable] 
    public class CharacterStat
    {
        public StatsChangeType statsChangeType;
        [Range(1, 100)] public int maxHealth;
        [Range(1f, 20f)] public float speed;
        public AttackSO attackSO;
    }
}
