using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
public class CarController : MonoBehaviour
{
    public bool isMoving = false;
    public float moveSpeed = 5f;



    void Update()
    {
        if (isMoving)
        {
            MoveCar();
        }
    }

    void MoveCar()
    {
        // Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // targetPosition.y = transform.position.y; // Araç yerden yükseklikte hareket etmeli

        // transform.LookAt(targetPosition);
        // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
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

}
