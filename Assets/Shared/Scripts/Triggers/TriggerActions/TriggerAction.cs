using UnityEngine;

namespace Ball.Triggers
{
    public abstract class TriggerAction : MonoBehaviour
    {
        public abstract void Execute();
    }

    public abstract class TriggerAction<T> : MonoBehaviour
    {
        public abstract void Execute(T arg);
    }

    public abstract class TriggerAction<T0, T1> : MonoBehaviour
    {
        public abstract void Execute(T0 arg0, T1 arg1);
    }
}
