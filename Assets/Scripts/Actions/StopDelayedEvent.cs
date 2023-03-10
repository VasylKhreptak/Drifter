using CBA.Events.General;
using UnityEngine;
using Action = CBA.Actions.Core.Action;

namespace Actions
{
    public class StopDelayedEvent : Action
    {
        [Header("References")]
        [SerializeField] private DelayedMonoEvent _delayedMonoEvent;

        #region MonoBehaviour

        private void OnValidate()
        {
            _delayedMonoEvent ??= GetComponent<DelayedMonoEvent>();
        }

        #endregion

        public override void Do()
        {
            _delayedMonoEvent.StopDelay();
        }
    }
}
