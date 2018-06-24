using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    public GameObject menu;
    public GameObject GameInputs, PauseInputs;

    bool pausa;

    private void Start()
    {
        pausa = false;
        NoPausa();
    }

    public void PauseButton()
    {
        pausa = !pausa;
        if (pausa)
            Pausar();
        else
            NoPausa();
    }

    private void Pausar()
    {
        menu.SetActive(true);
        PauseInputs.SetActive(true);
        GameInputs.SetActive(false);
        Time.timeScale = 0;
    }

    private void NoPausa()
    {
        menu.SetActive(false);
        PauseInputs.SetActive(false);
        GameInputs.SetActive(true);
        Time.timeScale = 1;
    }
}
