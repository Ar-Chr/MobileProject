using Ball.Entities.Player;
using UnityEngine;

namespace Ball.Triggers
{
    public class DamageTA : TriggerAction<GameObject>
    {
        [SerializeField] int damage = 1;

        public override void Execute(GameObject target)
        {
            if (target.TryGetComponent(out Health health))
                health.TakeDamage(damage);
        }
    }
}