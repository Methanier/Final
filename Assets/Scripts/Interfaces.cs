using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GPE338Final
{
    namespace Interfaces
    {
        public interface IDamagable
        {
            void TakeDamage();
            void Heal();
            void Die();
        }
    }
}

