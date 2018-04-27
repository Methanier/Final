using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GPE338Final.Interfaces;
using GPE338Final.Projectiles;

namespace GPE338Final
{
    namespace Units
    {
        public class Unit : MonoBehaviour, IDamagable
        {
            public UnitData data;
            public GameObject projectilePrefab;
            private Transform _tf;
            private GameObject _closestEnemy;
            private float _timeToNextAttack;

            private void Awake()
            {
                _tf = GetComponent<Transform>();
                _timeToNextAttack = Time.time;
                Physics.IgnoreLayerCollision(this.gameObject.layer, this.gameObject.layer);
            }

            // Use this for initialization
            void Start()
            {
                StartCoroutine(MoveUnit());
            }

            // Update is called once per frame
            void Update()
            {
                if (_closestEnemy == null)
                    CheckClosestEnemy();
            }

            IEnumerator MoveUnit()
            {
                while(true)
                {
                    if (data.BIsPlayerUnit)
                    {
                        _tf.Translate(Vector2.right * data.UnitMoveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        _tf.Translate(Vector2.right * data.UnitMoveSpeed * Time.deltaTime);
                    }
                    yield return new WaitForFixedUpdate();
                }
            }

            void Attack()
            {
                Projectile proj = null;
                if(Time.time >= _timeToNextAttack)
                {
                    GameObject temp = ObjectPoolManager.instance.GetObject(projectilePrefab, _tf.position, _tf.rotation);
                    proj = temp.GetComponent<Projectile>();
                }
                if(proj == null)
                {

                }
                else
                {
                    proj.Rb.AddForce(Vector2.right * proj.ProjectileSpeed, ForceMode2D.Force);
                    proj.gameObject.layer = this.gameObject.layer;
                    _timeToNextAttack = Time.time + data.UnitAttackSpeed;
                }
            }

            public void CheckClosestEnemy()
            {
                List<GameObject> temp;
                temp = (from enemy in ObjectPoolManager.instance.objectPool where enemy.GetType() == typeof(Unit) && enemy.GetComponent<Unit>().data.BIsPlayerUnit == false && (enemy.GetComponent<Transform>().position - _tf.position).magnitude < data.UnitAttackRange select enemy).ToList<GameObject>();

                if(temp.Count > 0)
                {
                    _closestEnemy = temp[0];
                }

                if (_closestEnemy != null)
                {
                    Attack();
                }
            }

            public void TakeDamage()
            {

            }

            public void Heal()
            {

            }

            public void Die()
            {

            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                if(collision.gameObject.GetType() == typeof(Unit))
                {
                    StopCoroutine(MoveUnit());
                }
            }

            private void OnCollisionExit2D(Collision2D collision)
            {
                if(collision.gameObject.GetType() == typeof(Unit))
                {
                    StartCoroutine(MoveUnit());
                }
            }
        }
    }
}