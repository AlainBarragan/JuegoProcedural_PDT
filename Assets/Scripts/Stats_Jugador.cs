using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Jugador : LivingCreature
{
    float speed;
    int danioMele;
    int danioRange;

    GUI_Script gui;

    private void Start()
    {
        gui = FindObjectOfType<GUI_Script>();
        vida = 5;
        maxVida = 5;
        speed = 1.0f;
        gui.ActualizarVida();
    }

    public void SetRangeDanio(int _danio)
    {
        danioRange = _danio;
    }

    public int GetRangeDanio()
    {
        return danioRange;
    }

    public void SetMeleDanio(int _danio)
    {
        danioMele = _danio;
    }

    public int GetMeleDanio()
    {
        return danioMele;
    }

    public void SetSpeed(float _vel)
    {
        speed = _vel;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void bajarVida(int _danio)
    {
        vida -= _danio;
        gui.ActualizarVida();
        if (vida <= 0)
            FindObjectOfType<GameManager>().Perder();
    }

    public int getVida()
    {
        return vida;
    }
}
