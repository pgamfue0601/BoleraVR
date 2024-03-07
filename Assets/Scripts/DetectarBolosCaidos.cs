using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectarBolosCaidos : MonoBehaviour
{
    private GameObject[] bolos;
    float threshold = 0.4f;
    private int caidos = 0;
    private AudioSource strike;
    private bool secondChance = false;
    private GameObject[] newBolos;
    private int linea = 0;
    private GameObject[] bolas;
    private GameObject[] newBolas;
    [SerializeField] private TextMeshProUGUI puntuacionText;
    [SerializeField] private TextMeshProUGUI bolosText;

    // Start is called before the first frame update
    void Start()
    {
        strike = GameObject.Find("AudioSourceStrike").GetComponent<AudioSource>();
        bolos = GameObject.FindGameObjectsWithTag("Bolo");
        bolas = GameObject.FindGameObjectsWithTag("Bola");

        newBolos = (GameObject[])bolos.Clone();
        newBolas = (GameObject[])bolas.Clone();

        foreach (GameObject bolo in bolos)
        {
            bolo.SetActive(false);
        }

        foreach (GameObject bola in bolas)
        {
            bola.SetActive(false);
        }

        AñadirBolos();
        GetBolosBack();

        AñadirBolas();
        GetBolasBack();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (newBolas.Length == 0)
        {
            AñadirBolas();
            GetBolasBack();
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(1.5f);

        if (other.CompareTag("Bola"))
        {
            Debug.Log("Llegó la bola");
            DetectarBolos();
            Destroy(other.gameObject, 2f);
        }
    }

    private void DetectarBolos()
    {

        
        if (newBolos != null)
        {
            foreach (GameObject bolo in newBolos)
            {
                if (bolo.transform.up.y < threshold)
                {
                    caidos++;
                    Destroy(bolo);
                }
                Debug.Log(caidos);
            }
            
            StartCoroutine(BolosCaidos());
            StartCoroutine(BloquearBolas());

        }
        
    }

    private IEnumerator BolosCaidos()
    {
        yield return new WaitForSeconds(2f);
        if (caidos == 10)
        {
            Debug.Log("¡STRIKE!");
            strike.PlayOneShot(strike.clip);

            newBolos = (GameObject[])bolos.Clone();
            newBolas = (GameObject[])bolas.Clone();
            AñadirBolos();
            GetBolosBack();
            GetBolasBack();
            bolosText.text = caidos.ToString();
            puntuacionText.text = (Int32.Parse(puntuacionText.text) + caidos).ToString();
        }
        else
        {
            if (secondChance)
            {
                Debug.Log("!SPARE!");

                newBolos = (GameObject[])bolos.Clone();
                newBolas = (GameObject[])bolas.Clone();

                AñadirBolos();
                GetBolosBack();
                GetBolasBack();
                caidos = 0;
                puntuacionText.text = (Int32.Parse(puntuacionText.text) + caidos).ToString();
            } else {
                secondChance = true;
                bolosText.text = caidos.ToString();
            }
        }
        
    }

    private IEnumerator BloquearBolas()
    {
        foreach (GameObject bola in newBolas)
        {
            bola.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        foreach (GameObject bola in newBolas)
        {
            bola.SetActive(true);
        }
    }

    private void GetBolosBack()
    {
        foreach (GameObject bolo in newBolos)
        {
            bolo.SetActive(true);
        }
    }

    private void GetBolasBack()
    {
        foreach (GameObject bola in newBolas)
        {
            bola.SetActive(true);
        }
    }

    private void AñadirBolos()
    {
        for (int i = 0; i < bolos.Length; i++)
        {
            Instantiate(newBolos[i]);
            newBolos[i].transform.position = bolos[i].transform.position;
        }
    }

    private void AñadirBolas()
    {
        for (int i = 0; i < bolas.Length; i++)
        {
            Instantiate(newBolas[i]);
            newBolas[i].transform.position = bolas[i].transform.position;
        }
    }
}
