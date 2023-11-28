using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public PathCreator BackPathCreator;
    public float speed = 5f;
    float distance;
    public CarController cars;
    public bool isFront;
    public bool isBack;
    public void Start()
    {
        cars = GetComponent<CarController>();
    }

    void Update()
    {
        if (cars.isMoving && isFront)
        {
            distance += speed + Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distance);
        }
        if (cars.isMoving && isBack)
        {
            distance += speed + Time.deltaTime;
            transform.position = BackPathCreator.path.GetPointAtDistance(distance);
        }
        ClickDetectr();
    }
    public void ClickDetectr()
    {
        if (Input.GetMouseButtonDown(0)) // Sol tıklama için
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("front"))
                {
                    isFront = true;
                    // Tıklanan obje istediğiniz tag'e sahiptir
                    Debug.Log("one tiklandi " + hit.collider.gameObject.name);
                }
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("back"))
                {
                    isBack = true;
                    // Tıklanan obje istediğiniz tag'e sahiptir
                    Debug.Log("arkaya tiklandi " + hit.collider.gameObject.name);
                }
            }
        }
    }
}
