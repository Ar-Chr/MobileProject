using UnityEngine;

namespace Ball.Triggers
{
    public class OnTouch : MonoBehaviour
    {
        [SerializeField] TriggerActionComposite genericActions;
        [SerializeField] TriggerActionComposite<GameObject> gameObjectActions;

        public TriggerActionComposite GenericActions => genericActions;
        public TriggerActionComposite<GameObject> GameObjectActions => gameObjectActions;

        void OnTriggerEnter(Collider other)
        {
            genericActions.Execute();
            gameObjectActions.Execute(other.gameObject);
        }
    }
}