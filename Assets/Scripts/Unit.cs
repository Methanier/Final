using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GPE338Final.Interfaces;
using GPE338Final.Projectiles;
using GPE338Final.CustomEvents;

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
            private bool _bIsMoving;
            private bool _bIsAttacking;

            private void Awake()
            {
                _tf = GetComponent<Transform>();
                _timeToNextAttack = Time.time;
                data.UnitCurrentHealth = data.UnitMaxHealth;
            }

            // Use this for initialization
            void Start()
            {
                _bIsMoving = true;
                _bIsAttacking = false;
                StartCoroutine("MoveUnit");
                StartCoroutine(FindClosestEnemy());

                EventManager.CallBackMethod eventDelegate = OnUnitDeath;

                EventManager.SubscribeListeners(GameManger.instance.UNIT_DEATH, OnUnitDeath);
            }

            // Update is called once per frame
            void Update()
            {

            }

            IEnumerator MoveUnit()
            {
                while(_bIsMoving)
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

            IEnumerator Attack()
            {
                while(_bIsAttacking)
                {
                    _bIsAttacking = true;
                    Projectile proj = null;
                    if (Time.time >= _timeToNextAttack)
                    {
                        Vector2 projPosition;
                        if (gameObject.tag == "Player")
                        {
                            projPosition = new Vector2(_tf.position.x + 1f, _tf.position.y);
                        }
                        else
                        {
                            projPosition = new Vector2(_tf.position.x - 1f, _tf.position.y);
                        }
                        GameObject temp = ObjectPoolManager.instance.GetObject(projectilePrefab, projPosition, _tf.rotation);
                        proj = temp.GetComponent<Projectile>();
                    }
                    if (proj == null)
                    {

                    }
                    else
                    {
                        proj.gameObject.layer = proj.gameObject.layer + 2;
                        proj.FiredFrom = this;
                        proj.Damage = data.UnitAttackDamage;
                        proj.gameObject.tag = this.gameObject.tag;
                        proj.StartCoroutine("ProjectileFlight");
                    }
                    yield return new WaitForSeconds(data.UnitAttackSpeed);
                }
            }

            IEnumerator FindClosestEnemy()
            {
                while(!_bIsAttacking)
                {
                    CheckClosestEnemy();
                    yield return new WaitForSeconds(1);
                }
            }

            public void CheckClosestEnemy()
            {
                List<GameObject> temp;
                temp = (from enemy in ObjectPoolManager.instance.objectPool where enemy.GetComponent<Unit>() != null && enemy.GetComponent<Unit>().data.BIsPlayerUnit != this.data.BIsPlayerUnit && enemy.gameObject.activeInHierarchy == true && (enemy.transform.position - _tf.position).magnitude < data.UnitAttackRange select enemy).ToList();

                if(temp.Count > 0)
                {
                    _closestEnemy = temp[0];
                }

                if (_closestEnemy != null)
                {
                    _bIsMoving = false;
                    _bIsAttacking = true;
                    StartCoroutine("Attack");
                }
            }

            public void TakeDamage(float damage, GameObject instigator)
            {
                data.UnitCurrentHealth -= damage;

                if (data.UnitCurrentHealth <= 0)
                    Die();
            }

            public void Heal(float amount, GameObject instigator)
            {
                data.UnitCurrentHealth += amount;

                if (data.UnitCurrentHealth > data.UnitMaxHealth)
                    data.UnitCurrentHealth = data.UnitMaxHealth;
            }

            public void Die()
            {
                this.gameObject.SetActive(false);
                _tf.position = ObjectPoolManager.instance.transform.position;

                EventManager.TriggerEvent(GameManger.instance.UNIT_DEATH);
            }

            public void OnUnitDeath(string inEvent)
            {
                _closestEnemy = null;
                _bIsAttacking = false;
                StopCoroutine("Attack");

                if(gameObject.activeInHierarchy)
                    StartCoroutine("MoveUnit");
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                if(this.gameObject.tag == "Player")
                {
                    if(collision.gameObject.tag == "Enemy")
                    {
                        _bIsMoving = false;
                    }
                }
                else if(this.gameObject.tag == "Enemy")
                {
                    if(collision.gameObject.tag == "Player")
                    {
                        _bIsMoving = false;
                    }
                }
            }

            private void OnCollisionExit2D(Collision2D collision)
            {
                if(this.gameObject.tag == "Player")
                {
                    if(collision.gameObject.tag == "Enemy")
                    {
                        _bIsMoving = true;
                    }
                }
                else if(this.gameObject.tag == "Enemy")
                {
                    if(collision.gameObject.tag == "Player")
                    {
                        _bIsMoving = true;
                    }
                }
            }
        }
    }
}