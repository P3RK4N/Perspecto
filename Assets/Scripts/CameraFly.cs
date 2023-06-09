using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly : MonoBehaviour
{
    public bool flying = false;

    Transform tf;

    private void Awake() {
        tf = transform;
    }

    private void Update() {
        if(flying)
        {
            Vector2 val = FindObjectOfType<PlayerMovement>().m_Movement;
            tf.position += val.y * tf.forward * Time.deltaTime * 10;
            tf.position += val.x * tf.right * Time.deltaTime * 10;
            if(Input.GetKey(KeyCode.Space)) tf.position += Vector3.up * 3 * Time.deltaTime;
            if(Input.GetKey(KeyCode.LeftShift)) tf.position += Vector3.down * 3 * Time.deltaTime;
        }
    }
}
