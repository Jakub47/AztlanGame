using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aztlan.Combat
{
    public class Health : MonoBehaviour
    {
        //change for current and max health;
        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
        }
    }
}
