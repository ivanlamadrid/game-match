using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width, height;
    public GameObject titleObject;
    void Start()
    {
        SetupBoard();
    }

    private void SetupBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var myObject = Instantiate(titleObject, new Vector3(x, y, -5), Quaternion.identity);
                myObject.transform.parent = transform;
            }
        }
    }
    void Update()
    {
        
    }
}
