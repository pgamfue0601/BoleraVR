using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarBolosCaidos : MonoBehaviour
{
    private GameObject[] bolos;
    float threshold = 0.4f;
    private int caidos = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(3f);

        if (other.CompareTag("Bola"))
        {
            Debug.Log("Llegó la bola");
            DetectarBolos();
        }
    }

    private void DetectarBolos()
    {
        bolos = GameObject.FindGameObjectsWithTag("Bolo");
        foreach (GameObject bolo in bolos) 
        {
            if (bolo.transform.up.y < threshold)
            {
                caidos++;
                Destroy(bolo);
            }
            Debug.Log(caidos);
            if (caidos == 10)
            {
                Debug.Log("¡STRIKE!");
            } else
            {
                Debug.Log("!SPLIT!");
            }
        }
    }
}
