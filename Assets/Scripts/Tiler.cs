using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.z);
    }
}
