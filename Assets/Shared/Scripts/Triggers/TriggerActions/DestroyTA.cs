using UnityEngine;

namespace Ball.Triggers
{
    public class DestroyTA : TriggerAction
    {
        [SerializeField] GameObject target;

        public override void Execute()
        {
            Destroy(target);
        }
    }
}
