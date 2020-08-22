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
    }
}
