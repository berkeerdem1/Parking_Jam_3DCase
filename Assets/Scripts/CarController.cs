using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
public class CarController : MonoBehaviour
{
    public bool isMoving = false;
    public float moveSpeed = 5f;
    public Transform frontClickPoint;  // Ön tıklama noktasının referansı
    public Transform backClickPoint;   // Ark tıklama noktasının referansı
    // public Clickd click;
    // void Start()
    // {
    //     click=GetComponent<ClickSelector>();
    // }


    void Update()
    {
        if (isMoving)
        {
            MoveCar();
        }
    }

    void MoveCar()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.y = transform.position.y; // Araç yerden yükseklikte hareket etmeli

        transform.LookAt(targetPosition);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    void BackMoveCar()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.y = -transform.position.y; // Araç yerden yükseklikte hareket etmeli

        transform.LookAt(targetPosition);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        isMoving = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "finish")
        {
            print("degdi");
            Destroy(gameObject);
        }
    }
}
