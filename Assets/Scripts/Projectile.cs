using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GPE338Final.Units;

namespace GPE338Final
{
    namespace Projectiles
    {
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
            //private Rigidbody2D _rb;
            private Unit _firedFrom;
            private Transform _tf;


            public float Damage { get { return _damage; } set { _damage = value; } }
            public float LifeSpan { get { return _lifeSpan; } set { _lifeSpan = value; } }
            public float ProjectileSpeed { get { return _projectileSpeed; } set { _projectileSpeed = value; } }
            public Unit FiredFrom { get { return _firedFrom; } set { _firedFrom = value; } }
            //public Rigidbody2D RigidBody { get { return _rb; } }

            private void Awake()
            {
                //_rb = GetComponent<Rigidbody2D>();
                _tf = GetComponent<Transform>();
            }

            // Use this for initialization
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {
                if(Time.time >= _timeToDestroy)
                {
                    this.gameObject.SetActive(false);
                    transform.position = ObjectPoolManager.instance.transform.position;
                }
            }

            private void OnEnable()
            {
                _timeToDestroy = Time.time + LifeSpan;
            }

            public IEnumerator ProjectileFlight()
            {
                while(true)
                {
                    _tf.Translate(Vector2.right * ProjectileSpeed * Time.deltaTime);
                    yield return new WaitForFixedUpdate();
                }
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                if(this.gameObject.tag == "Player")
                {
                    if(collision.gameObject.tag == "Enemy")
                    {
                        Unit temp = collision.gameObject.GetComponent<Unit>();
                        temp.TakeDamage(Damage, FiredFrom.gameObject);
                        this.gameObject.SetActive(false);
                        transform.position = ObjectPoolManager.instance.transform.position;
                    }
                    else if(collision.gameObject.tag != "Player")
                    {
                        this.gameObject.SetActive(false);
                        transform.position = ObjectPoolManager.instance.transform.position;
                    }
                }
                else if(this.gameObject.tag == "Enemy")
                {
                    if(collision.gameObject.tag == "Player")
                    {
                        Unit temp = collision.gameObject.GetComponent<Unit>();
                        temp.TakeDamage(Damage, FiredFrom.gameObject);
                        this.gameObject.SetActive(false);
                        transform.position = ObjectPoolManager.instance.transform.position;
                    }
                    else if(collision.gameObject.tag != "Enemy")
                    {
                        this.gameObject.SetActive(false);
                        transform.position = ObjectPoolManager.instance.transform.position;
                    }
                }
            }
        }
    }
}