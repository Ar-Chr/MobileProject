using UnityEngine;

namespace Ball.Triggers
{
    public class OnTimeElapsed : MonoBehaviour
    {
        [SerializeField] float time;
        [SerializeField] bool active = true;
        [SerializeField] TriggerActionComposite actions;

        void Update()
        {
            if (!active)
                return;

            time -= Time.deltaTime;

            if (time <= 0)
            {
                actions.Execute();
                active = false;
            }
        }

        public void SetActive(bool active)
        {
            this.active = active;
        }
    }
}
