using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aztlan.Combat
{
    public class Health : MonoBehaviour
    {
        private bool _isDead;

        public bool IsDead()
        {
            return _isDead;
        }

        //change for current and max health;
        [SerializeField] float healthPoints = 100f;

        public void TakeDamage(float damage)
        {
            if (_isDead) return;

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            _isDead = true;
        }
    }
}
