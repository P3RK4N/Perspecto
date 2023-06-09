using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWalker : MonoBehaviour
{
    Transform r_TF;
    Transform r_Player;

    void Awake()
    {
        r_TF = transform;
        r_Player = FindObjectOfType<PlayerMovement>().transform.GetChild(0);

        GetComponent<MeshFilter>().mesh.bounds = new Bounds(r_TF.position, new Vector3(1,1,1) * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        rotateSphere();
    }

    void rotateSphere()
    {
        if(Input.GetKey(KeyCode.W))
        {
            r_TF.RotateAround(r_TF.position, r_Player.right, 60*Time.deltaTime);
        }
    }
}
