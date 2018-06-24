using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Enemigo
{
    bool saltando;
    float timer, timer2;

    private void Start()
    {
        saltando = false;
        maxVida = 2;
        danio = 1;
        SetStats();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        if (timer >= 2.0f && !saltando)
        {
            Saltar();
            timer = 0.0f;
        }

        else if (timer <= 1 && saltando)
        {
            this.transform.Translate(Vector2.left * 3 * Time.deltaTime);
            
        }
        else if (timer >= 1 && saltando)
        {
            saltando = false;
        }

        if (timer2 >= 5.0f)
        {
            timer2 = 0.0f;
            this.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    void Saltar()
    {
        saltando = true;
        rb.AddForce(Vector2.up * 200);
        anim.SetTrigger("Saltar");
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player") && saltando)
        {
            c.gameObject.GetComponent<Stats_Jugador>().bajarVida(danio);
        }
    }
}