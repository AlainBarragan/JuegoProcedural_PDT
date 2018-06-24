using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RyanNielson.InputBinder;

public class Movimiento_Jugador : MonoBehaviour
{
    InputBinder inputBinder;
    Stats_Jugador stats;
    Rigidbody2D rb;
    bool enSuelo, apuntando;
    Animator anim;

    private void Start()
    {
        inputBinder = FindObjectOfType<InputBinder>();
        anim = this.GetComponentInChildren<Animator>();
        stats = FindObjectOfType<Stats_Jugador>();
        rb = this.GetComponent<Rigidbody2D>();

        inputBinder.BindAxis("Walk", Caminar);
        inputBinder.BindAxis("Aim", Apuntar);

        apuntando = false;
        enSuelo = true;
    }

    public void Atacar()
    {
        anim.SetTrigger("Atacar");
    }

    public void Saltar()
    {
        if (enSuelo)
        {
            rb.AddForce(Vector2.up * 520);
            enSuelo = false;
            anim.SetBool("Saltar", true);
        }
    }

    public void Disparar()
    {

    }

    public void Especial()
    {

    }

    public void ApuntarArDerecha()
    {

    }

    public void ApuntarArIzquierda()
    {

    }

    public void Caminar(float value)
    {
        if (!apuntando)
        {
            if (value < 0.0f)
                CaminarIzquierda();
            else if (value > 0.0f)
                CaminarDerecha();
            else
                anim.SetBool("Caminar", false);
        }
    }

    private void CaminarDerecha()
    {
        this.transform.Translate(Vector2.right * Time.deltaTime * 10 * stats.GetSpeed());
        this.transform.rotation = new Quaternion(0, 0, 0, Quaternion.identity.z);
        anim.SetBool("Caminar", true);
    }

    private void CaminarIzquierda()
    {
        this.transform.Translate(Vector2.right * Time.deltaTime * 10 * stats.GetSpeed());
        this.transform.rotation = new Quaternion(0, 180, 0, Quaternion.identity.z);
        anim.SetBool("Caminar", true);
    }

    void Apuntar(float value)
    {
        if (value > 0.0f)
            ApuntarIzquierda();
        else if (value < 0.0f)
            ApuntarDerecha();
        else
            apuntando = false;
    }

    private void ApuntarDerecha()
    {
        this.transform.rotation = new Quaternion(0, 0, 0, Quaternion.identity.z);
        apuntando = true;
    }

    private void ApuntarIzquierda()
    {
        this.transform.rotation = new Quaternion(0, 180, 0, Quaternion.identity.z);
        apuntando = true;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Suelo"))
        {
            enSuelo = true;
            anim.SetBool("Saltar", false);
        }

        /*if (c.CompareTag("AgarreI") || c.CompareTag("AgarreD"))
        {
            this.rb.gravityScale = 0;
            this.rb.velocity = Vector2.zero;
            this.transform.position = c.transform.position;
            enSuelo = true;
        }*/
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        //this.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}