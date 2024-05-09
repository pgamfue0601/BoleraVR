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
    private int linea = 0;
    private GameObject[] bolas;
    [SerializeField] private TextMeshProUGUI puntuacionText;
    [SerializeField] private TextMeshProUGUI bolosText;
    [SerializeField] private GameObject bolasConjunto, bolosConjunto;
    private GameObject newBolos, newBolas;

    // Start is called before the first frame update
    void Start()
    {
        strike = GameObject.Find("AudioSourceStrike").GetComponent<AudioSource>();
        bolos = GameObject.FindGameObjectsWithTag("Bolo");
        bolas = GameObject.FindGameObjectsWithTag("Bola");

        foreach (GameObject bolo in bolos)
        {
            bolo.SetActive(false);
        }

        foreach (GameObject bola in bolas)
        {
            bola.SetActive(false);
        }

        GetBolosBack();
        GetBolasBack();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (newBolas.Length == 0)
        //{
        //    AñadirBolas();
        //    GetBolasBack();
        //}
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(1.5f);

        if (other.CompareTag("BolaClone"))
        {
            Debug.Log("Llegó la bola");
            DetectarBolos();
            Destroy(other.gameObject);
        }
    }

    private void DetectarBolos()
    {
        if (GameObject.FindGameObjectsWithTag("BoloClone") != null)
        {
            foreach (GameObject bolo in GameObject.FindGameObjectsWithTag("BoloClone"))
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

            GetBolosBack();
            bolosText.text = caidos.ToString();
            puntuacionText.text = (Int32.Parse(puntuacionText.text) + caidos).ToString();
        }
        else
        {
            if (secondChance)
            {
                Debug.Log("!SPARE!");
                foreach (GameObject bolo in GameObject.FindGameObjectsWithTag("BoloClone"))
                {
                    Destroy(bolo);
                }
                GetBolosBack();
                bolosText.text = caidos.ToString();
                puntuacionText.text = (Int32.Parse(puntuacionText.text) + caidos).ToString();
                caidos = 0;
            } else {
                secondChance = true;
                bolosText.text = caidos.ToString();
            }
        }
        
    }

    private IEnumerator BloquearBolas()
    {
        foreach (GameObject bola in GameObject.FindGameObjectsWithTag("BolaClone"))
        {
            Destroy(bola);
        }
        yield return new WaitForSeconds(1.5f);
        GetBolasBack();
    }

    private void GetBolosBack()
    {
        newBolos = Instantiate(bolosConjunto, new Vector3(21.5f, -0.35f, 19.09f), bolosConjunto.transform.rotation);
    }

    private void GetBolasBack()
    {
        newBolas = Instantiate(bolasConjunto, new Vector3(18.9f, -0.35f, 19.12f), bolasConjunto.transform.rotation);
    }
}
