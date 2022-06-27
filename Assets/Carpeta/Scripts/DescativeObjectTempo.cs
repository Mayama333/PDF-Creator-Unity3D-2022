using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescativeObjectTempo : MonoBehaviour
{
    public float numTime;
    public GameObject[] objectDesactive;

    private void Start()
    {
        Invoke("desactive", numTime);
    }

    private void desactive()
    {
        for (int i = 0; i < objectDesactive.Length; i++)
        {
            objectDesactive[i].SetActive(false);
        }
    }

}
