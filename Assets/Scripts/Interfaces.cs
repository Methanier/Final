using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GPE338Final
{
    namespace Interfaces
    {
        public interface IDamagable
        {
            void TakeDamage(float damage, GameObject instigator);
            void Heal(float amount, GameObject instigator);
            void Die();
        }
    }
}

