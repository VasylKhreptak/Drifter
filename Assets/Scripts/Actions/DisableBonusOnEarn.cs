using System;
using Gameplay;
using UnityEngine;

namespace Actions
{
    public class DisableBonusOnEarn : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BonusEarner _bonusEarner;

        #region MonoBehaviour

        private void OnValidate()
        {
            _bonusEarner ??= GetComponent<BonusEarner>();
        }

        private void OnEnable()
        {
            _bonusEarner.onBonusEarned += DisableBonus;
        }

        private void OnDisable()
        {
            _bonusEarner.onBonusEarned -= DisableBonus;
        }

        #endregion

        private void DisableBonus(GameObject bonus)
        {
            bonus.SetActive(false);
        }
    }
}
