using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Sinlgeton

        public static UIManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion

        #region Unity Actions

        public UnityAction ResetProgress = delegate { };
        public UnityAction<int> UpdateProgress = delegate { };

        #endregion

        // public void Start()
        // {
        //     var RecalculateButton = GameObject.Find("Recalculate").GetComponent<Button>();
        //     RecalculateButton.onClick.AddListener(RecalculateGrid);
        // }

        public void RecalculateGrid()
        {
            LevelManager.Instance.StartLevel.Invoke();
            ResetProgress.Invoke();
        }
    }
}