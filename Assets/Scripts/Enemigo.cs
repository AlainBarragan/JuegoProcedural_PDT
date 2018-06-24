using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Enemigo : LivingCreature
{
    protected int nivel;
    protected int danio;
    protected Rigidbody2D rb;

    protected void SetStats()
    {
        vida = maxVida;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetHit(int _danio)
    {
        this.vida -= _danio;
        if (vida <= 0)
        {
            vida = 0;
            Morir();
        }
    }

    void Morir()
    {
        Destroy(this.gameObject);
    }
}
