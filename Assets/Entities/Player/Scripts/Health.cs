using System;
using UnityEngine;

namespace Ball.Entities.Player
{
    public class Health : MonoBehaviour
    {
        public event Action TookDamage;
        public event Action Died;

        [SerializeField] GameObject deathEffects;
        [Space]
        [SerializeField] int maxHealth;
        [SerializeField] bool takesDamage;

        public int CurrentHealth { get; private set; }
        public bool Alive { get; private set; }

        void Start()
        {
            CurrentHealth = maxHealth;
            Alive = true;
        }

        public void TakeDamage(int damage)
        {
            if (!Alive || !takesDamage)
                return;

            CurrentHealth -= damage;
            TookDamage?.Invoke();

            if (CurrentHealth <= 0)
                Die();
        }

        void Die()
        {
            Instantiate(deathEffects, transform.position, Quaternion.identity);
            Alive = false;
            Died?.Invoke();
        }
    }
}
