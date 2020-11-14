using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GamePlayManager : MonoBehaviour
    {
        #region Sinlgeton

        public static GamePlayManager Instance;

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

        #region Public Variables

        public int GridSize = 5;
        public List<GridButton> AdjacentList = new List<GridButton>();

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject gridPrefab;
        [SerializeField] private RectTransform mainCanvas;

        #endregion

        #region Private Variables

        private GameObject _gridParent;
        private GridButton[,] _gridArray;
        private int _matchCount;

        #endregion

        private void OnEnable()
        {
            LevelManager.Instance.StartLevel += InitializeLevel;
            UIManager.Instance.ResetProgress += ResetProgress;
        }

        private void OnDisable()
        {
            LevelManager.Instance.StartLevel -= InitializeLevel;
            UIManager.Instance.ResetProgress -= ResetProgress;
        }


        private void Start()
        {
            gridPrefab = Resources.Load("GridPrefab") as GameObject;
        }

        public void ChangeGridSize(string newGridSize)
        {
            GridSize = int.Parse(newGridSize);
        }

        #region Initialize

        private void InitializeLevel()
        {
            CreateGridParent();
        }

        private void CreateGridParent()
        {
            if (_gridParent != null) Destroy(_gridParent);

            _gridParent = new GameObject {name = "GridsParent"};
            _gridParent.transform.SetParent(mainCanvas);
            _gridParent.AddComponent<RectTransform>();
            _gridParent.GetComponent<RectTransform>().transform.localPosition = Vector2.zero;
            InitializeGridMatrices();
        }

        private void InitializeGridMatrices()
        {
            var buttonDistance = mainCanvas.rect.width / GridSize;
            _gridArray = new GridButton[GridSize, GridSize];
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var newGridObject = Instantiate(gridPrefab, _gridParent.transform, true);
                    if (newGridObject is null) continue;
                    newGridObject.name = "Grid" + i + "_" + j;
                    var recTransform = newGridObject.GetComponent<RectTransform>();
                    recTransform.sizeDelta = new Vector2(buttonDistance, buttonDistance);
                    var rect = mainCanvas.rect;
                    recTransform.transform.position = new Vector2(
                        (j * buttonDistance)
                        , rect.height - ((i * buttonDistance)));


                    var gridButton = newGridObject.GetComponent<GridButton>();
                    gridButton.OnClickSprite.GetComponent<RectTransform>().sizeDelta =
                        new Vector2(buttonDistance, buttonDistance);
                    gridButton.GridPos = new Vector2(i, j);

                    _gridArray[i, j] = gridButton;
                }
            }
        }

        #endregion

        #region Reset

        private void ResetProgress()
        {
            _matchCount = 0;
        }

        #endregion

        #region Gameplay

        public void CheckForAdjacents(int x, int y)
        {
            AdjacentList.Add(_gridArray[x, y]);


            if (x - 1 >= 0)
            {
                if (_gridArray[x - 1, y].State)
                {
                    if (!AdjacentList.Contains(_gridArray[x - 1, y]))
                        CheckForOtherAdjacents(x - 1, y);
                }
            }


            if (x + 1 < GridSize)
            {
                if (_gridArray[x + 1, y].State)
                {
                    if (!AdjacentList.Contains(_gridArray[x + 1, y]))
                        CheckForOtherAdjacents(x + 1, y);
                }
            }


            if (y + 1 < GridSize)
            {
                if (_gridArray[x, y + 1].State)
                {
                    if (!AdjacentList.Contains(_gridArray[x, y + 1]))
                        CheckForOtherAdjacents(x, y + 1);
                }
            }


            if (y - 1 >= 0)
            {
                if (_gridArray[x, y - 1].State)
                {
                    if (!AdjacentList.Contains(_gridArray[x, y - 1]))
                        CheckForOtherAdjacents(x, y - 1);
                }
            }

            ControlTotalMatch();
        }

        public void CheckForOtherAdjacents(int x, int y)
        {
            AdjacentList.Add(_gridArray[x, y]);


            if (x - 1 >= 0)
            {
                if (_gridArray[x - 1, y].State)
                {
                    if (!AdjacentList.Contains(_gridArray[x - 1, y]))
                        CheckForOtherAdjacents(x - 1, y);
                }
            }


            if (x + 1 < GridSize)
            {
                if (_gridArray[x + 1, y].State)
                {
                    if (!AdjacentList.Contains(_gridArray[x + 1, y]))
                        CheckForOtherAdjacents(x + 1, y);
                }
            }


            if (y + 1 < GridSize)
            {
                if (_gridArray[x, y + 1].State)
                {
                    if (!AdjacentList.Contains(_gridArray[x, y + 1]))
                        CheckForOtherAdjacents(x, y + 1);
                }
            }


            if (y - 1 >= 0)
            {
                if (_gridArray[x, y - 1].State)
                {
                    if (!AdjacentList.Contains(_gridArray[x, y - 1]))
                        CheckForOtherAdjacents(x, y - 1);
                }
            }
        }

        private void ControlTotalMatch()
        {
            if (AdjacentList.Count < 3) return;
            _matchCount++;
            UIManager.Instance.UpdateProgress.Invoke(_matchCount);

            foreach (var t in AdjacentList)
            {
                t.OnClickSprite.enabled = false;
                t.State = false;
            }

            AdjacentList.Clear();
        }

        #endregion
    }
}