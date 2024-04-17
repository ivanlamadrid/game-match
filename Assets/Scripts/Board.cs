using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width, height;
    public GameObject titleObject;

    public float cameraSizeOffSet;
    public float cameraVerticalOffSet;
    void Start()
    {
        SetupBoard();
        CameraPosition();
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
            }
        }
    }
    void Update()
    {
        
    }
}
