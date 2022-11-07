using Ball.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public class MineAudio : MonoBehaviour
    {
        private void Start()
        {
            TriggerAction[] genericTAs = GetComponents<TriggerAction>();
            TriggerAction<GameObject>[] gameObjectTAs = GetComponents<TriggerAction<GameObject>>();

            Mine[] mines = FindObjectsOfType<Mine>();
            foreach (Mine mine in mines)
            {
                OnTouch onTouch = mine.GetComponent<OnTouch>();

                foreach (var ta in genericTAs)
                    onTouch.GenericActions.Subscribe(ta);

                foreach (var ta in gameObjectTAs)
                    onTouch.GameObjectActions.Subscribe(ta);
            }
        }
    }
}
