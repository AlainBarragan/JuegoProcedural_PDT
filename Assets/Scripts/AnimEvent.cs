using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    Combate hitbox;

    private void Start()
    {
        hitbox = FindObjectOfType<Combate>();
    }

    public void Danio(int _num)
    {

        if (_num == 1)
            hitbox.atacando = true;
        else
            hitbox.atacando = false;
            
    }
}