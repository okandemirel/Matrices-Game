using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class GridButton : MonoBehaviour
{
    #region Public Variables

    public Vector2 GridPos;
    public bool State;
    public Image OnClickSprite;

    #endregion

    public void ButtonPressed()
    {
        if (State) return;
        OnClickSprite.enabled = true;
        State = !State;
        GamePlayManager.Instance.AdjacentList.Clear();
        GamePlayManager.Instance.CheckForAdjacents((int) GridPos.x, (int) GridPos.y);
    }
}