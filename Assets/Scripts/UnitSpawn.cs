using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPE338Final
{
    namespace Units
    {
        public enum SpawnType
        {
            Player,
            Enemy
        }

        public class UnitSpawn : MonoBehaviour
        {

            [HideInInspector]
            public Transform tf;
            public SpawnType spawnType;

            private void Awake()
            {
                tf = GetComponent<Transform>();
            }
            // Use this for initialization
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {

            }
        }
    }
}
