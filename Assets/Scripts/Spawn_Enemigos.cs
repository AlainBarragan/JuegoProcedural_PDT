using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemigos : MonoBehaviour
{
    public Transform [] spawn;
    public GameObject [] enemigos;

	void Start ()
    {
        int num = Random.Range(1, 3);

        for (int i  = 0; i < num; i++)
        {
            int num_spawn = Random.Range(0, spawn.Length);
            int num_enem = Random.Range(0, enemigos.Length);

            Instantiate(enemigos[num_enem].transform, spawn[num_spawn]);
        }
    }
}
