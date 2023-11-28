using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickDetector : MonoBehaviour
{
    public bool isFront;
    public bool isBack;
    public void Update()
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
                    isBack = false;
                    // Tıklanan obje istediğiniz tag'e sahiptir
                    Debug.Log("arkaya tiklandi " + hit.collider.gameObject.name);
                }
            }
        }
    }
}

