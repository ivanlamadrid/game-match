using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public int x, y;
    public Board board;

    public void SetUp(int positionX, int positionY, Board boardTiles)
    {
        x = positionX;
        y = positionY;
        board = boardTiles;
    }

    public void OnMouseDown()
    {
        board.TileDown(this);
    }

    public void OnMouseEnter()
    {
        board.TileOver(this);
    }
    public void OnMouseUp()
    {
        board.TileUp(this);
    }
}
