using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RyanNielson.InputBinder;

public class Menu : MonoBehaviour
{
    public Button [] botones;

    InputBinder inputBinder;
    int index;
    bool mover;

    private void Start()
    {
        mover = true;
        inputBinder = FindObjectOfType<InputBinder>();
        index = 0;
        botones[index].GetComponent<Image>().color = Color.green;
        inputBinder.BindAxis("MenuMove", MoverMenu);
    }

    void MoverMenu(float value)
    {
        if (mover)
        {
            if (value < 0.0f)
                SubirMenu();
            else if (value > 0.0f)
                BajarMenu();
        }
        else
        {
            if (value == 0.0f)
                mover = true;
        }
    }

    void SubirMenu()
    {
        botones[index].GetComponent<Image>().color = Color.white;
        index--;
        if (index < 0)
            index = botones.Length - 1;
        botones[index].GetComponent<Image>().color = Color.green;
        mover = false;
    }

    void BajarMenu()
    {
        botones[index].GetComponent<Image>().color = Color.white;
        index++;
        if (index > botones.Length -1)
            index = 0;
        botones[index].GetComponent<Image>().color = Color.green;
        mover = false;
    }

    public void Seleccionar()
    {
        switch (index)
        {
            case 0:
                Jugar();
                break;
            case 1:
                Creditos();
                break;
            case 2:
                Salir();
                break;
        }
    }

    void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    void Creditos()
    {

    }

    void Salir()
    {
        Application.Quit();
    }
}
