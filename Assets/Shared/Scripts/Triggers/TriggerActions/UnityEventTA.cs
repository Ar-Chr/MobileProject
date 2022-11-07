using UnityEngine;
using UnityEngine.Events;

namespace Ball.Triggers
{
    public class UnityEventTA : TriggerAction
    {
        [SerializeField] UnityEvent OnTrigger;

        public override void Execute()
        {
            OnTrigger.Invoke();
        }
    }
}
