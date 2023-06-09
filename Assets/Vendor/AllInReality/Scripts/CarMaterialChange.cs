using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMaterialChange : MonoBehaviour
{
    [SerializeField]
    GameObject carModel;
    [SerializeField]
    GameObject frustumCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        carModel.GetComponent<TurnTranslucentThenBack>().TurnTranslucent();
        
        for(int i = 0; i < frustumCamera.transform.childCount; i++)
        {
            frustumCamera.transform.GetChild(i).GetComponent<Renderer>().enabled = false;
        }
    }
    private void OnDisable()
    {
        carModel.GetComponent<TurnTranslucentThenBack>().TurnBack();
        
        for(int i = 0; i < frustumCamera.transform.childCount; i++)
        {
            frustumCamera.transform.GetChild(i).GetComponent<Renderer>().enabled = true;
        }
    }
}
