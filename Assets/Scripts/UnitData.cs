using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPE338Final
{
    namespace Units
    {

        public enum UnitType
        {
            Archer,
            Mage,
            Swordsmen,
            Axemen,
            Spearmen,
            Healer,
        }

        public enum UnitRace
        {
            Elf,
            Fairy,
            Human,
            Knight
        }

        [CreateAssetMenu(fileName = "Unit Data", menuName = "Units", order = 0)]
        public class UnitData : ScriptableObject
        {
            [SerializeField]
            private string _unitName;
            [SerializeField]
            private float _unitMoveSpeed;
            [SerializeField]
            private float _unitAttackRange;
            [SerializeField]
            private float _unitAttackDamage;
            [SerializeField]
            private float _unitAttackSpeed;
            [SerializeField]
            private float _unitMaxHealth;
            private float _unitCurrentHealth;
            [SerializeField]
            private bool _bIsPlayerUnit;
            [SerializeField]
            private UnitType _type;
            [SerializeField]
            private UnitRace _race;

            public string UnitName { get { return _unitName; } set { _unitName = value; } }
            public float UnitMoveSpeed { get { return _unitMoveSpeed; } set { _unitMoveSpeed = value; } }
            public float UnitAttackRange { get { return _unitAttackRange; } set { _unitAttackRange = value; } }
            public float UnitAttackDamage { get { return _unitAttackDamage; } set { _unitAttackDamage = value; } }
            public float UnitAttackSpeed { get { return _unitAttackSpeed; } set { _unitAttackSpeed = value; } }
            public float UnitMaxHealth { get { return _unitMaxHealth; } set { _unitMaxHealth = value; } }
            public float UnitCurrentHealth { get { return _unitCurrentHealth; } set { _unitCurrentHealth = value; } }
            public bool BIsPlayerUnit { get { return _bIsPlayerUnit; } set { _bIsPlayerUnit = value; } }
            public UnitType Type { get { return _type; } }
            public UnitRace Race { get { return _race; } }

        }
    }
}
