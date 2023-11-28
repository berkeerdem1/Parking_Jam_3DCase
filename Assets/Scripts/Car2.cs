using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2 : MonoBehaviour
{
    public bool isMoving = false;
    public float moveSpeed = 5f;

    private void OnMouseDown()
    {
        isMoving = true;
    }
}
