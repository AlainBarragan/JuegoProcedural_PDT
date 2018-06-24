using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate : MonoBehaviour
{
    public bool atacando;
    float timer;
    Stats_Jugador stats;

    private void Start()
    {
        stats = this.GetComponent<Stats_Jugador>();
        timer = 0.0f;
        atacando = false;
    }

    private void OnTriggerStay2D(Collider2D c)
    {
        if (c.CompareTag("Enemigo") && atacando)
        {
            c.GetComponent<Enemigo>().GetHit(stats.GetMeleDanio());
        }
    }
}