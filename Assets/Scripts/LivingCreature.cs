using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LivingCreature : MonoBehaviour
{
    protected int vida;
    protected int maxVida;
    protected Animator anim;
}