using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LantasRotate : MonoBehaviour
{

    public GameObject originalWheel;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = originalWheel.transform.rotation;
    }
}

