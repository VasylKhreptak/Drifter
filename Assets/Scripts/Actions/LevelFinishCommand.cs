using CBA.Actions.Core;
using CBA.Events.General;
using UnityEngine;

namespace Actions
{
    public class LevelFinishCommand : Action
    {
        [Header("References")]
        [SerializeField] private ManualMonoEvent _manualMonoEvent;

        public override void Do()
        {
            _manualMonoEvent.Invoke();
        }
    }
}
