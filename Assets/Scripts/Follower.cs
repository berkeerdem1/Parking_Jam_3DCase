using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public PathCreator BackPathCreator;
    public float speed = 0.3f;
    float distance;
    public float timer = 0f;
    [Header("Kontroller")]
    public bool isFront;
    public bool isBack;
    private bool isTurn = false;
    Vector3 startPosition;
    [Header("Yol takip donusu")]
    public Transform[] Targets;
    public Transform[] BackTargets;
    private int indexTarget = 0;
    public float TurnSpeed = 30f;
    [Header("Konfeti ")]
    public Transform ConfettiPos;
    public GameObject Confetti;
    public void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isFront)
        {
            timer += Time.deltaTime;
            Sound_Manager.playsound("car_drive");
            // One tikladiysak one gider
            distance += speed + Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distance);

            // Her kose noktasindan sonra 90 derece donmesini saglar
            if (!isTurn && Vector3.Distance(transform.position, Targets[indexTarget].position) < 0.5f)
            {
                transform.Rotate(0, 90, 0);
                isTurn = true;
            }
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Arabanin kose noktasina olan uzakligini alir
            float distance2 = Vector3.Distance(transform.position, Targets[indexTarget].position);
            if (distance2 < 0.5f)
            {
                indexTarget++;
                isTurn = false;
            }

        }
        if (isBack)
        {
            timer += Time.deltaTime;
            Sound_Manager.playsound("car_drive");
            // Arkaya tikladiysak arkaya gider
            distance += speed + Time.deltaTime;
            transform.position = BackPathCreator.path.GetPointAtDistance(distance);

            // Ilk kose noktasinda -90 derece donmesini saglar
            if (indexTarget == 0 && !isTurn && Vector3.Distance(transform.position, BackTargets[indexTarget].position) < 0.5f)
            {
                transform.Rotate(0, -90, 0);
                isTurn = true;
            }
            //sonrakilerde 90 derece donmesini saglar
            if (indexTarget > 0 && !isTurn && Vector3.Distance(transform.position, BackTargets[indexTarget].position) < 0.5f)
            {
                transform.Rotate(0, 90, 0);
                isTurn = true;
            }

            float distance2 = Vector3.Distance(transform.position, BackTargets[indexTarget].position);
            if (distance2 < 0.5f)
            {
                indexTarget++;
                isTurn = false;
            }
        }
        ClickDetectr();
    }
    public void ClickDetectr()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Obejnin neresine tikladigimizi kontrol eder
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("front"))
                {
                    isFront = true;
                }
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("back"))
                {
                    isBack = true;
                }
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "finish")
        {
            Sound_Manager.stopsound("car_drive");
            Instantiate(Confetti, ConfettiPos.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "car" || other.gameObject.tag == "wall")
        {
            Sound_Manager.playsound("car_horn");
            if (timer < 0.5f)
            {
                isFront = false;
                isBack = false;

                //baslangic pozisyonuna dondurur
                transform.position = startPosition;
                distance = 0;

                //Carptigi arabanin ve duvarin hareket etmesini durdurur
                Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();
                if (otherRigidbody != null)
                {
                    otherRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                }
                timer = 0f;
            }
        }
    }
}
