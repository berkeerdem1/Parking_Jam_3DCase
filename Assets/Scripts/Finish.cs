using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public float height = 5.0f;
    public float speed = 2.0f;

    private bool isUp = false;
    private Vector3 targetPos;
    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        float hedefYukseklik = isUp ? transform.position.y + height : transform.position.y - height;
        targetPos.y = Mathf.Lerp(transform.position.y, hedefYukseklik, speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(targetPos);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("car"))
        {
            // Araba çizgiye yaklaştığında kalkanı yukarı kaldır
            isUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("car"))
        {
            // Araba çizgiyi geçtikten sonra kalkanı geri indir
            isUp = false;
        }
    }
}
