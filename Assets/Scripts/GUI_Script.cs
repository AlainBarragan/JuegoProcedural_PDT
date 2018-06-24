using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Script : MonoBehaviour
{
    public Text vida;

    Stats_Jugador stats;

	void Start ()
    {
        stats = FindObjectOfType<Stats_Jugador>();
        vida.text = stats.getVida().ToString();
	}

    public void ActualizarVida()
    {
        vida.text = stats.getVida().ToString();
    }
}