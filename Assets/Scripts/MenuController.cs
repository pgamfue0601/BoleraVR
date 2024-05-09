using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Text texto;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComenzarJuego()
    {
        texto.text = "Cargando...";
        button.interactable = false;
        SceneManager.LoadSceneAsync("BowlingScene");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
