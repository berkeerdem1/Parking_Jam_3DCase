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
    [Header("Yolu takip donusu")]
    public Transform[] Targets;
    public Transform[] BackTargets;
    public int indexTarget = 0;
    public float TurnSpeed = 30f;
    public bool isTurn = false;
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
            distance += speed + Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distance);
            
            if (!isTurn && Vector3.Distance(transform.position, Targets[indexTarget].position) < 0.5f)
            {
                Debug.Log("4. araba one gidiyor ");
                // Aracin y ekseni etrafinda 90 derece donus
                transform.Rotate(0, 90, 0);
                isTurn = true;
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            float mesafe = Vector3.Distance(transform.position, Targets[indexTarget].position);
            if (mesafe < 0.5f)
            {
                Debug.Log("indexTarget artti ");
                indexTarget++;
                isTurn = false;
            } 
        
        }
        if (isBack)
        {
            distance += speed + Time.deltaTime;
            transform.position = BackPathCreator.path.GetPointAtDistance(distance);

            // Eger ilk hedefe yaklasiliyorsa ve daha once donus yapimamissa, eksi 90 derece don
            if (indexTarget==0 && !isTurn && Vector3.Distance(transform.position, BackTargets[indexTarget].position) < 0.5f)
            {
                transform.Rotate(0, -90, 0);
                isTurn = true;
                // if (indexTarget == 0)
                // {
                //     Debug.Log("4. araba ilk kez donus yapti ");
                //     transform.Rotate(0, -90, 0);
                //     isTurn = true;
                // }
            }
            if (indexTarget > 0 && !isTurn && Vector3.Distance(transform.position, BackTargets[indexTarget].position) < 0.5f)
            {
                transform.Rotate(0, 90, 0);
                isTurn = true;
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            float mesafe = Vector3.Distance(transform.position, BackTargets[indexTarget].position);
            if (mesafe < 0.5f)
            {
                indexTarget++;
                isTurn = false;
            }
        }
        ClickDetector();
    }
    public void ClickDetector()
    {
        if (Input.GetMouseButtonDown(0)) // Sol tiklama icin
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("front4"))
                {
                    isFront = true;
                }
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("back4"))
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
