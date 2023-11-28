using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class Folllower4 : MonoBehaviour
{
    public PathCreator pathCreator;
    public PathCreator BackPathCreator;
    public float speed = 5f;
    float distance;
    public bool isFront;
    public bool isBack;
    Vector3 startPosition;
    public void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isFront)
        {
            distance += speed + Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distance);
        }
        if (isBack)
        {
            distance += speed + Time.deltaTime;
            transform.position = BackPathCreator.path.GetPointAtDistance(distance);
        }
        ClickDetector();
    }
    public void ClickDetector()
    {
        if (Input.GetMouseButtonDown(0)) // Sol tıklama için
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("front4"))
                {
                    isFront = true;
                    // Tıklanan obje istediğiniz tag'e sahiptir
                    Debug.Log("one tiklandi " + hit.collider.gameObject.name);
                }
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("back4"))
                {
                    isBack = true;
                    // Tıklanan obje istediğiniz tag'e sahiptir
                    Debug.Log("arkaya tiklandi " + hit.collider.gameObject.name);
                }
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "finish")
        {
            print("degdi");
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "car" || other.gameObject.tag == "wall")
        {
            print("arabaya degdi");
            isFront = false;
            isBack = false;

            //baslangic pozisyonuna don
            transform.position = startPosition;
            distance = 0;

            //Carptıgı arabanin hareket etmesini durdur
            Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                otherRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }

        }
    }
}
