using Ball.Entities.Player;
using UnityEngine;

namespace Ball.Triggers
{
    public class MoveToTargetTA : TriggerAction<GameObject>
    {
        new Transform transform;

        private void Start()
        {
            transform = base.transform;
        }

        public override void Execute(GameObject target)
        {
            transform.position = target.transform.position;
        }
    }
}