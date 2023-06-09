using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public GameObject matrices;
    public GameObject attributes;

    void Start()
    {
        transform.Find("ShowMatrices").GetComponent<Toggle>().onValueChanged.AddListener((value) => matrices.SetActive(value));
        transform.Find("ShowAttributes").GetComponent<Toggle>().onValueChanged.AddListener((value) => attributes.SetActive(value));
    }
}
