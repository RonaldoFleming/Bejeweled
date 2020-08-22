using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private int _gridWidth = 8;
    [SerializeField] private int _gridHeight = 8;
    [SerializeField] private List<Color> _colorsList = default;

    private GridNode[,] _grid;
    private List<GridNode> _verticallyMatchedNodes;
    private List<GridNode> _horizontallyMatchedNodes;

    private class GridNode
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public Color NodeColor { get; private set; }
        public GameObject NodeGraphic { get; private set; }

        public GridNode(int posX, int posY, Color nodeColor, GameObject nodeGraphic)
        {
            PosX = posX;
            PosY = posY;
            NodeColor = nodeColor;
            NodeGraphic = nodeGraphic;
        }
    }

    private void Start()
    {
        _grid = new GridNode[_gridWidth, _gridHeight];
        _verticallyMatchedNodes = new List<GridNode>();
        _horizontallyMatchedNodes = new List<GridNode>();

        for(int y = 0; y < _gridHeight; y++)
        {
            for(int x = 0; x < _gridWidth; x++)
            {
                Color nodeColor = _colorsList[Random.Range(0, _colorsList.Count)];
                GameObject node = GetComponent<ObjectPooler>().GetPooledObject("Node");
                node.name = "Node_" + x + "_" + y;
                node.transform.SetParent(transform);
                node.transform.localPosition = new Vector3(x * 1f + 0.5f, -y * 1f - 0.5f, 0f);
                node.GetComponent<MeshRenderer>().materials[0].SetColor("_BaseColor", nodeColor);
                _grid[x, y] = new GridNode(x, y, nodeColor, node);
            }
        }

        transform.position = new Vector3(-_gridWidth * 0.5f, _gridHeight * 0.5f, 0f);

        FindMatches();
    }

    private void FindMatches()
    {
        double initialTime = Time.realtimeSinceStartup;
        FindVerticalMatches();
        FindHorizontalMatches();
        //Debug.Log("Elapsed time - " + (Time.realtimeSinceStartup - initialTime));
    }

    private void FindVerticalMatches()
    {
        List<GridNode> _currentMatches = new List<GridNode>();
        for (int x = 0; x < _gridWidth; x++)
        {
            _currentMatches.Clear();
            for(int y = 0; y < _gridHeight - 1; y++)
            {
                if(_currentMatches.Count == 0)
                {
                    _currentMatches.Add(_grid[x, y]);
                }
                if(_grid[x,y].NodeColor == _grid[x, y + 1].NodeColor)
                {
                    _currentMatches.Add(_grid[x, y + 1]);
                }
                else
                {
                    if(_currentMatches.Count >= 3)
                    {
                        _verticallyMatchedNodes.AddRange(_currentMatches);
                    }

                    _currentMatches.Clear();
                }

                if (y == _gridHeight - 2)
                {
                    if (_currentMatches.Count >= 3)
                    {
                        _verticallyMatchedNodes.AddRange(_currentMatches);
                    }
                }
            }
        }
    }

    private void FindHorizontalMatches()
    {
        List<GridNode> _currentMatches = new List<GridNode>();
        for (int y = 0; y < _gridHeight; y++)
        {
            _currentMatches.Clear();
            for (int x = 0; x < _gridWidth - 1; x++)
            {
                if (_currentMatches.Count == 0)
                {
                    _currentMatches.Add(_grid[x, y]);
                }
                if (_grid[x, y].NodeColor == _grid[x + 1, y].NodeColor)
                {
                    _currentMatches.Add(_grid[x + 1, y]);
                }
                else
                {
                    if (_currentMatches.Count >= 3)
                    {
                        _horizontallyMatchedNodes.AddRange(_currentMatches);
                    }

                    _currentMatches.Clear();
                }

                if(x == _gridWidth - 2)
                {
                    if (_currentMatches.Count >= 3)
                    {
                        _horizontallyMatchedNodes.AddRange(_currentMatches);
                    }
                }
            }
        }
    }
}
