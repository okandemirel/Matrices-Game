using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Application.targetFrameRate = 60;
        Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
        
        DontDestroyOnLoad(gameObject);
    }

    #endregion
}