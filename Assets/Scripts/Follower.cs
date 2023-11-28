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
    public CarController cars;
    public bool isFront;
    public bool isBack;
    Vector3 startPosition;
    [Header("Yolu takip donusu")]
    public Transform[] Targets;
    private int indexTarget = 0;
    public float TurnSpeed = 30f;
    private bool isTurn = false;
    public Transform ConfettiPos;
    public GameObject Confetti;
    public void Start()
    {
        cars = GetComponent<CarController>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (cars.isMoving && isFront)
        {
            distance += speed + Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distance);
            if (indexTarget < Targets.Length)
            {
                // Eğer dönüş yapılmadıysa ve araç hedef noktaya yeterince yaklaştıysa, dönüş yap
                if (!isTurn && Vector3.Distance(transform.position, Targets[indexTarget].position) < 0.5f)
                {
                    // Aracın y ekseni etrafında 90 derece dönüş
                    transform.Rotate(0, 90, 0);
                    isTurn = true; // Dönüş yapıldı
                }

                // Aracın hareketi
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

                // Eğer araç hedef noktaya yeterince yaklaştıysa, bir sonraki hedefe geç
                float mesafe = Vector3.Distance(transform.position, Targets[indexTarget].position);
                if (mesafe < 0.5f)
                {
                    indexTarget++;
                    isTurn = false; // Bir sonraki hedefe geçildiğinde dondu değişkenini sıfırla
                }
            }
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
        if (Input.GetMouseButtonDown(0))
        {
            //Obejnin neresine tikladigimizi kontrol etme
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("front"))
                {
                    isFront = true;
                    Debug.Log("one tiklandi " + hit.collider.gameObject.name);
                }
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("back"))
                {
                    isBack = true;
                    Debug.Log("arkaya tiklandi " + hit.collider.gameObject.name);
                }
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "finish")
        {
            Instantiate(Confetti, ConfettiPos.transform.position, Quaternion.identity);
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
