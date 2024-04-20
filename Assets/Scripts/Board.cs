using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width, height;
    public GameObject titleObject;

    public float cameraSizeOffSet;
    public float cameraVerticalOffSet;

    public GameObject[] availablePieces;

    private Tiles[,] _tiles;
    private Piece[,] _pieces;

    private Tiles startTile;
    private Tiles endTile;
    void Start()
    {
        _tiles = new Tiles[width, height];
        _pieces = new Piece[width, height];
        SetupBoard();
        CameraPosition();
        SetupPieces();
    }

    private void SetupPieces()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var selectedPiece = availablePieces[UnityEngine.Random.Range(0, availablePieces.Length)];
                var myObject = Instantiate(selectedPiece, new Vector3(x, y, -5), Quaternion.identity);
                myObject.transform.parent = transform;
                _pieces[x, y] = myObject.GetComponent<Piece>();
                _pieces[x, y].SetUp(x, y, this);
            }
        }
    }

    private void CameraPosition()
    {
        float newX = (float)width / 2f;
        float newY = (float)height / 2f;
        if (Camera.main != null) Camera.main.transform.position = new Vector3(newX -0.5f, newY - 0.5f + cameraVerticalOffSet, -10f);
        float horizontal = width + 1;
        float vertical = ((float)height/2) + 1;
        if (Camera.main != null) Camera.main.orthographicSize = horizontal > vertical ? horizontal + cameraSizeOffSet : vertical + cameraSizeOffSet;
    }
    private void SetupBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var myObject = Instantiate(titleObject, new Vector3(x, y, -5), Quaternion.identity);
                myObject.transform.parent = transform;
                _tiles[x, y] = myObject.GetComponent<Tiles>();
                _tiles[x,y]?.SetUp(x, y, this);
            }
        }
    }
    public void TileDown(Tiles tile)
    {
        startTile = tile;
    }
    public void TileOver(Tiles tile)
    {
        endTile = tile;
    }
    public void TileUp(Tiles tile)
    {
        if (startTile != null && endTile != null)
        {
            SwapTiles();
        }

        startTile = null;
        endTile = null;
    }

    private void SwapTiles()
    {
        var StartPiece = _pieces[startTile.x, startTile.y];
        var EndPiece = _pieces[endTile.x, endTile.y];
        StartPiece.Move(endTile.x, endTile.y);
        EndPiece.Move(startTile.x,startTile.y);
        _pieces[startTile.x, startTile.y] = EndPiece;
        _pieces[endTile.x, endTile.y] = StartPiece;
    }
}
