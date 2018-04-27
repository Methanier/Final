using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GPE338Final.Units;

namespace GPE338Final
{
    namespace Projectiles
    {
        [RequireComponent(typeof(Rigidbody2D))]
        public class Projectile : MonoBehaviour
        {

            [SerializeField]
            private float _damage;
            [SerializeField]
            private float _lifeSpan;
            [SerializeField]
            private float _projectileSpeed;
            [SerializeField]
            private float _timeToDestroy;
            public Rigidbody2D Rb;
            private Unit _firedFrom;


            public float Damage { get { return _damage; } set { _damage = value; } }
            public float LifeSpan { get { return _lifeSpan; } set { _lifeSpan = value; } }
            public float ProjectileSpeed { get { return _projectileSpeed; } set { _projectileSpeed = value; } }
            public Unit FiredFrom { get { return _firedFrom; } set { _firedFrom = value; } }

            private void Awake()
            {
                Rb = GetComponent<Rigidbody2D>();
            }

            // Use this for initialization
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {

            }

            private void OnEnable()
            {
                _timeToDestroy = Time.time + LifeSpan;
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                
            }
        }
    }
}