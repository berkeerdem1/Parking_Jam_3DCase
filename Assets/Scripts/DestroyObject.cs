using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    void Start()
    {
        Destroy(this.gameObject, 2f);
    }

}
