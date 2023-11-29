using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car1TagControl : MonoBehaviour
{
    public bool isFront;
    public bool isBack;
    
    void Start()
    {
        
    }

    void Update()
    {
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
}
