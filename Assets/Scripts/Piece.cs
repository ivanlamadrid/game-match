using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{
    public int x, y;
    public Board board;

    public enum Type
    {
        Elephant,
        Giraffe,
        Hippo,
        Monkey,
        Panda,
        Parrot,
        Penguin,
        Pig,
        Rabbit,
        Snake
    };

    public Type pieceType;

    public void SetUp(int _x, int _y, Board _board)
    {
        x = _x;
        y = _y;
        board = _board;
    }

    public void Move(int desX, int desY)
    {
        transform.DOMove(new Vector3(desX, desY, -5), 0.25f).SetEase(Ease.InOutCubic).onComplete = () =>
        {
            x = desX;
            y = desY;
        };
    }

    [ContextMenu("Test Move")]
    public void MoveTest()
    {
        Move(0,0);
    }
}
