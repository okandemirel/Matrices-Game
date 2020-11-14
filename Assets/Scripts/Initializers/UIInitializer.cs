using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Initializers
{
    public class UIInitializer : MonoBehaviour
    {
        #region Public Variables

        public TextMeshProUGUI ProgressText;

        #endregion

        private void OnEnable()
        {
            UIManager.Instance.ResetProgress += ResetProgress;
            UIManager.Instance.UpdateProgress += UpdateProgress;
        }

        private void OnDisable()
        {
            UIManager.Instance.ResetProgress -= ResetProgress;
            UIManager.Instance.UpdateProgress -= UpdateProgress;
        }

        private void UpdateProgress(int value)
        {
            ProgressText.text = "Match Count: " + value;
        }

        private void ResetProgress()
        {
            ProgressText.text = "Match Count: 0";
        }
    }
}