using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    float distance;
    public CarController cars;
    public void Start()
    {
        cars = GetComponent<CarController>();
    }

    void Update()
    {
        if (cars.isMoving)
        {
            distance += speed + Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distance);
        }
    }
}
