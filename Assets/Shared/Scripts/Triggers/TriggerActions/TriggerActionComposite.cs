using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ball.Triggers
{
    [Serializable]
    public class TriggerActionComposite
    {
        [SerializeField] List<TriggerAction> actions;

        public void Execute()
        {
            for (int i = 0; i < actions.Count; i++)
                actions[i].Execute();
        }

        public void Subscribe(TriggerAction action)
        {
            if (!actions.Contains(action))
                actions.Add(action);
        }

        public void Unsubscribe(TriggerAction action)
        {
            if (actions.Contains(action))
                actions.Remove(action);
        }
    }

    [Serializable]
    public class TriggerActionComposite<T>
    {
        [SerializeField] List<TriggerAction<T>> actions;

        public void Execute(T arg)
        {
            for (int i = 0; i < actions.Count; i++)
                actions[i].Execute(arg);
        }

        public void Subscribe(TriggerAction<T> action)
        {
            if (!actions.Contains(action))
                actions.Add(action);
        }

        public void Unsubscribe(TriggerAction<T> action)
        {
            if (actions.Contains(action))
                actions.Remove(action);
        }
    }

    [Serializable]
    public class TriggerActionComposite<T0, T1>
    {
        [SerializeField] TriggerAction<T0, T1>[] actions;

        public void Execute(T0 arg0, T1 arg1)
        {
            for (int i = 0; i < actions.Length; i++)
                actions[i].Execute(arg0, arg1);
        }
    }
}
