using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GPE338Final.Units;

namespace GPE338Final
{
    namespace GPEGUI
    {

        [System.Serializable]
        public class MyOnClick : UnityEvent<UnitType, UnitRace, bool>
        {

        }

        public class CustomButton : MonoBehaviour
        {

            private MyOnClick onClick;
            public UnitType type;
            public UnitRace race;

            // Use this for initialization
            void Start()
            {
                if (onClick == null)
                    onClick = new MyOnClick();

                onClick.AddListener(SpawnArcher);
            }

            public void SpawnArcher(UnitType type, UnitRace race, bool bIsPlayerUnit)
            {
                if(bIsPlayerUnit)
                {
                    GameManger.instance.SpawnPlayerUnit(type, race);
                }
                else
                {
                    GameManger.instance.SpawnEnemyUnit(type, race);
                }
                
            }

            
            public void Click(bool bIsPlayerUnit)
            {
                onClick.Invoke(type, race, bIsPlayerUnit);
            }
        }
    }
}