using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform lookat;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<PlayerMovement>())
            lookat = FindObjectOfType<PlayerMovement>().transform.GetChild(1);
        else 
            lookat = GameObject.Find("CameraARL").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookat);
    }
}
