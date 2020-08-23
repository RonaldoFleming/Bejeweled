using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private int _gridWidth = 8;
    [SerializeField] private int _gridHeight = 8;
    [SerializeField] private List<Color> _colorsList = default;

    private NodeController[,] _grid;
    private List<NodeController> _verticallyMatchedNodes;
    private List<NodeController> _horizontallyMatchedNodes;
    private EventBus.MoveNodeEventArgs _currentMoveArgs;
    private bool _nodeMoved = false;
    private List<NodeController> _movingNodesList;

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
        _grid = new NodeController[_gridWidth, _gridHeight];
        _verticallyMatchedNodes = new List<NodeController>();
        _horizontallyMatchedNodes = new List<NodeController>();
        _movingNodesList = new List<NodeController>();

        for(int y = 0; y < _gridHeight; y++)
        {
            for(int x = 0; x < _gridWidth; x++)
            {
                Color nodeColor = _colorsList[Random.Range(0, _colorsList.Count)];
                GameObject go = GetComponent<ObjectPooler>().GetPooledObject("Node");
                NodeController node = go.GetComponent<NodeController>();
                node.name = "Node_" + x + "_" + y;
                node.transform.SetParent(transform);
                node.transform.localPosition = new Vector3(x * 1f + 0.5f, -y * 1f - 0.5f, 0f);
                node.PosX = x;
                node.PosY = y;
                node.NodeColor = nodeColor;
                _grid[x, y] = node;
            }
        }

        transform.position = new Vector3(-_gridWidth * 0.5f, _gridHeight * 0.5f, 0f);

        FindDestroyAndSettleMatchedNodes();

        EventBus.OnMoveNode += OnMoveNode;
        EventBus.OnNodeMovementEnded += OnNodeMovementEnded;
    }

    #region Node Movement
    private void OnMoveNode(object sender, System.EventArgs e)
    {
        _currentMoveArgs = e as EventBus.MoveNodeEventArgs; ;
        _nodeMoved = true;
        _movingNodesList.Clear();
        MoveUsingCurrentArgs();
    }

    private void MoveUsingCurrentArgs()
    {
        switch (_currentMoveArgs.MovementDirection)
        {
            case NodeController.NodeMovementDirection.Up:
                MoveNodeUp(_currentMoveArgs.PosX, _currentMoveArgs.PosY);
                break;

            case NodeController.NodeMovementDirection.Down:
                MoveNodeDown(_currentMoveArgs.PosX, _currentMoveArgs.PosY);
                break;

            case NodeController.NodeMovementDirection.Left:
                MoveNodeLeft(_currentMoveArgs.PosX, _currentMoveArgs.PosY);
                break;

            case NodeController.NodeMovementDirection.Right:
                MoveNodeRight(_currentMoveArgs.PosX, _currentMoveArgs.PosY);
                break;
        }
    }

    private void MoveNodeUp(int posX, int posY)
    {
        if (posY > 0)
        {
            NodeController node01 = _grid[posX, posY];
            NodeController node02 = _grid[posX, posY - 1];
            node01.PosY--;
            node02.PosY++;
            _grid[posX, posY] = node02;
            _grid[posX, posY - 1] = node01;
            _movingNodesList.Add(node01);
            _movingNodesList.Add(node02);
            node01.GoToNewPosition(true);
            node02.GoToNewPosition(true);
        }
        else
        {
            _nodeMoved = false;
        }
    }

    private void MoveNodeDown(int posX, int posY)
    {
        if (posY < _gridHeight - 1)
        {
            NodeController node01 = _grid[posX, posY];
            NodeController node02 = _grid[posX, posY + 1];
            node01.PosY++;
            node02.PosY--;
            _grid[posX, posY] = node02;
            _grid[posX, posY + 1] = node01;
            _movingNodesList.Add(node01);
            _movingNodesList.Add(node02);
            node01.GoToNewPosition(true);
            node02.GoToNewPosition(true);
        }
        else
        {
            _nodeMoved = false;
        }
    }

    private void MoveNodeLeft(int posX, int posY)
    {
        if (posX > 0)
        {
            NodeController node01 = _grid[posX, posY];
            NodeController node02 = _grid[posX - 1, posY];
            node01.PosX--;
            node02.PosX++;
            _grid[posX, posY] = node02;
            _grid[posX - 1, posY] = node01;
            _movingNodesList.Add(node01);
            _movingNodesList.Add(node02);
            node01.GoToNewPosition(true);
            node02.GoToNewPosition(true);
        }
        else
        {
            _nodeMoved = false;
        }
    }

    private void MoveNodeRight(int posX, int posY)
    {
        if (posX < _gridWidth - 1)
        {
            NodeController node01 = _grid[posX, posY];
            NodeController node02 = _grid[posX + 1, posY];
            node01.PosX++;
            node02.PosX--;
            _grid[posX, posY] = node02;
            _grid[posX + 1, posY] = node01;
            _movingNodesList.Add(node01);
            _movingNodesList.Add(node02);
            node01.GoToNewPosition(true);
            node02.GoToNewPosition(true);
        }
        else
        {
            _nodeMoved = false;
        }
    }

    private void OnNodeMovementEnded(object sender, System.EventArgs e)
    {
        NodeController node = sender as NodeController;

        _movingNodesList.Remove(node);
        if (_movingNodesList.Count == 0)
        {
            FindDestroyAndSettleMatchedNodes();
        }
    }

    private void SwapNodePlaces(NodeController firstNode, NodeController secondNode)
    {
        int firstNodeStartingY = firstNode.PosY;
        firstNode.PosY = secondNode.PosY;
        secondNode.PosY = firstNodeStartingY;
        _grid[firstNode.PosX, firstNode.PosY] = firstNode;
        _grid[secondNode.PosX, secondNode.PosY] = secondNode;
        if (!_movingNodesList.Contains(firstNode))
        {
            _movingNodesList.Add(firstNode);
        }
        firstNode.GoToNewPosition(true);
    }

    private void SettleNodes()
    {
        for(int x = 0; x < _gridWidth; x++)
        {
            List<NodeController> emptyNodes = new List<NodeController>();
            for(int y = _gridHeight-1; y >= 0; y--)
            {
                if (!_grid[x, y].gameObject.GetComponent<MeshRenderer>().enabled)
                {
                    emptyNodes.Add(_grid[x, y]);
                }
                else
                {
                    if (emptyNodes.Count > 0)
                    {
                        SwapNodePlaces(_grid[x, y], emptyNodes[0]);
                        emptyNodes.Add(_grid[x, y]);
                        emptyNodes.RemoveAt(0);
                    }
                }
            }
            for(int i = 0; i < emptyNodes.Count; i++)
            {

                emptyNodes[i].NodeColor = _colorsList[Random.Range(0, _colorsList.Count)];
                emptyNodes[i].transform.localPosition = new Vector3(emptyNodes[i].PosX * 1f + 0.5f, /*_gridHeight * 0.5f +*/ i + 1f, 0f);
                emptyNodes[i].GetComponent<MeshRenderer>().enabled = true;
                emptyNodes[i].GoToNewPosition(true);
            }
        }
    }
    #endregion

    #region Matching
    private void FindMatches()
    {
        FindVerticalMatches();
        FindHorizontalMatches();
    }

    private void FindVerticalMatches()
    {
        _verticallyMatchedNodes.Clear();
        List<NodeController> _currentMatches = new List<NodeController>();
        for (int x = 0; x < _gridWidth; x++)
        {
            _currentMatches.Clear();
            for (int y = 0; y < _gridHeight - 1; y++)
            {
                if (_currentMatches.Count == 0)
                {
                    if(_grid[x, y].gameObject.activeSelf)
                    {
                        _currentMatches.Add(_grid[x, y]);
                    }
                }
                if (_grid[x,y].NodeColor == _grid[x, y + 1].NodeColor)
                {
                    if (_grid[x, y + 1].gameObject.activeSelf)
                    {
                        _currentMatches.Add(_grid[x, y + 1]);
                    }
                }
                else
                {
                    if (_currentMatches.Count >= 3)
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
        _horizontallyMatchedNodes.Clear();
        List<NodeController> _currentMatches = new List<NodeController>();
        for (int y = 0; y < _gridHeight; y++)
        {
            _currentMatches.Clear();
            for (int x = 0; x < _gridWidth - 1; x++)
            {
                if (_currentMatches.Count == 0)
                {
                    if (_grid[x, y].gameObject.activeSelf)
                    {
                        _currentMatches.Add(_grid[x, y]);
                    }
                }
                if (_grid[x, y].NodeColor == _grid[x + 1, y].NodeColor)
                {
                    if (_grid[x + 1, y].gameObject.activeSelf)
                    {
                        _currentMatches.Add(_grid[x + 1, y]);
                    }
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
    #endregion

    #region Destroying
    private void DestroyMatchedNodes()
    {
        DestroyVerticallyMatchedNodes();
        DestroyHorizontallyMatchedNodes();
        _nodeMoved = false;
        SettleNodes();
    }

    private void DestroyVerticallyMatchedNodes()
    {
        foreach(NodeController node in _verticallyMatchedNodes)
        {
            node.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void DestroyHorizontallyMatchedNodes()
    {
        foreach (NodeController node in _horizontallyMatchedNodes)
        {
            node.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    #endregion

    #region Find, Destroy and Settle
    private void FindDestroyAndSettleMatchedNodes()
    {
        FindMatches();
        if (_verticallyMatchedNodes.Count > 0 || _horizontallyMatchedNodes.Count > 0)
        {
            DestroyMatchedNodes();
        }
        else if(_nodeMoved)
        {
            MoveUsingCurrentArgs();
            _nodeMoved = false;
        }
    }
    #endregion
}
