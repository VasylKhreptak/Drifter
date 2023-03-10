using CBA.EventListeners.Core;
using Gameplay;
using UnityEngine;

namespace Events
{
    public class OnBonusEarnedMonoEvent : EventListener
    {
        [Header("References")]
        [SerializeField] private BonusEarner _bonusEarner;

        protected override void AddListener()
        {
            _bonusEarner.onBonusEarned += Invoke;
        }
        protected override void RemoveListener()
        {
            _bonusEarner.onBonusEarned -= Invoke;
        }

        private void Invoke(GameObject bonus) => Invoke();
    }
}
