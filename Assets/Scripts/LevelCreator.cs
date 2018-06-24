//Codigo que se encarga de crear el nivel de forma procedural 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    //Direccion a la que se creara el siguiente nodo
    enum direcciones
    {
        Derecha, 
        Izquierda,
        Arriba,
        Abajo
    }

    //Clase nodo, de aqui salen las habitaciones
    class nodo
    {
        int x, y;
        int pared, techo;
        nodo anterior;
        direcciones dir;
        GameObject cuarto;

        public void SetAnterior(nodo _anterior)
        {
            anterior = _anterior;
        }

        public void SetX (int _x)
        {
            x = _x;
        }

        public void SetY(int _y)
        {
            y = _y;
        }

        public void SetDireccion (direcciones _dir)
        {
            dir = _dir;
        }

        public void SetTecho(int _techo)
        {
            techo = _techo;
        }

        public void SetPared(int _pared)
        {
            pared = _pared;
        }

        public nodo GetAnterior()
        {
            return anterior;
        }

        public int GetX ()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public int GetPared()
        {
            return pared;
        }

        public int GetTecho()
        {
            return techo;
        }

        public direcciones GetDireccion()
        {
            return dir;
        }

        public void SetCuarto(GameObject _cua)
        {
            cuarto = _cua;
        }

        public GameObject GetCuarto()
        {
            return cuarto;
        }
    }

    //Prefab de cuarto, espacio de 60 * 30
    public GameObject prefab_Cuarto, meta;
    public Mapa camara2;
    public int nivel;

    //Las paredes que puede tener los cuartos
    [Header("Paredes")]
    public GameObject pared0;
    public GameObject pared1;
    public GameObject pared2;
    public GameObject pared3;
    public GameObject paredFinal;

    //Los techos que pueden tener los cuartos
    [Header("Techos")]
    public GameObject techo0;
    public GameObject techo1;
    public GameObject techo2;
    public GameObject techo3;
    public GameObject techoFinal;

    [Header("Platadormas")]
    public GameObject [] plataformas;

    //La direccion a la que se mueve
    direcciones direccion;
    //Array de cuartos en el nivel
    nodo[] cuartos;
    //El primer cuarto y la raiz de los nodos
    nodo principal;
    //numero de cuartos que habra en el nivel
    int num_Cuartos;

    //medidas del mapa
    float  alto, bajo, derecha, izquierda;

    void Start ()
    {
        alto = bajo = izquierda = derecha = 0;
        nivel = FindObjectOfType<GameManager>().GetNivel() + 2;

        //La semilla para intentar replicar bugs
        Debug.Log("Seed: " + Random.state);
        //Posiciona el array de cuartos en el mapa
        CrearCuartos();
        //Pone los muros de los cuartos, solo el contorno
        CrearMuros();
        //poner las plataformas en el cuarto
        LlenarCuartos();
        //Setear el mapa dependiedno del tamaño del nivel
        camara2.SetCamara(alto, bajo, derecha, izquierda);
    }

    void CrearCuartos()
    {
        //El numero de cuartos en el nivel es un numero random entre el 7 y 10 mas el nivel en el que esta el jugador
        num_Cuartos = Random.Range(7, 10) + nivel;
        cuartos = new nodo[num_Cuartos];
        for (int i = 0; i < num_Cuartos; i++)
            cuartos[i] = new nodo();
        principal = new nodo();

        //Se pone el nodo raiz en el centro del mundo y se le asigna su GO
        principal.SetX(0);
        principal.SetY(0);
        principal.SetCuarto(Instantiate(prefab_Cuarto, new Vector2(principal.GetX(), principal.GetY()), Quaternion.identity));
        //Se pone el nodo raiz como padre del primer nodo
        cuartos[0].SetAnterior(principal);

        //loop que pasa por todos los nodos
        for (int i = 0; i < num_Cuartos; i++)
        {
            bool direccion_valida = false;
            int intentos = 0;
            int x = 0, y = 0;

            //se repite mientras la direccion a la que vaya no este ocupada
            while (!direccion_valida || (x==0 && y ==0))
            {
                //setea la direccion como valida para comenzar
                direccion_valida = true;
                //elige una direccion al azar a la cual moverse
                direccion = (direcciones)Random.Range(0, 4);
                //Dependiendo de la direccion que elige pone el cuarto en una posicion adyacente al nodo anterior
                switch (direccion)
                {
                    case direcciones.Arriba:
                        x = cuartos[i].GetAnterior().GetX();
                        y = cuartos[i].GetAnterior().GetY() + 15;
                        break;
                    case direcciones.Abajo:
                        x = cuartos[i].GetAnterior().GetX();
                        y = cuartos[i].GetAnterior().GetY() - 15;
                        break;
                    case direcciones.Derecha:
                        x = cuartos[i].GetAnterior().GetX() + 30;
                        y = cuartos[i].GetAnterior().GetY();
                        break;
                    case direcciones.Izquierda:
                        x = cuartos[i].GetAnterior().GetX() - 30;
                        y = cuartos[i].GetAnterior().GetY();
                        break;
                } //switch
                //si ya hay un cuarto en esa pocicion elige otra
                for (int j = i-1; j >= 0; j--)
                {
                    if (cuartos[j].GetX() == x && cuartos[j].GetY() == y)
                        direccion_valida = false;
                }
                //si todos los lados estan llenos regresa al nodo anterior
                intentos++;
                if (intentos > 5)
                {
                    cuartos[i].SetAnterior(cuartos[i].GetAnterior().GetAnterior());
                    intentos = 0;
                }
            }//while

            //se llenan los datos del nodo en base a la posicion en la que quedo el cuarto
            if (i != num_Cuartos-1)
                cuartos[i+1].SetAnterior(cuartos[i]);
            cuartos[i].SetDireccion(direccion);
            cuartos[i].SetX(x);
            cuartos[i].SetY(y);
            //Se crea un cuarto en la posicion elegida
            cuartos[i].SetCuarto(Instantiate(prefab_Cuarto, new Vector2(cuartos[i].GetX(), cuartos[i].GetY()), Quaternion.identity));

            if (x < izquierda)
                izquierda = x;
            if (x > derecha)
                derecha = x;
            if (y < bajo)
                bajo = y;
            if (y > alto)
                alto = y;
        }//for
    }//CrearCuartos

    void CrearMuros()
    {
        //Se comienza en el ultimo nodo del arreglo
        nodo actual = cuartos[num_Cuartos - 1];
        //loop que pasa por todos los nodos desde el ultimo hasta la raiz
        while (actual != null)
        {
            int num = 0;
            //Se determina el numero de bared que ira a los lados de la habitacion con un algoritmo que garantiza que los 
            //cuartos de los lados se conectaran siempre
            int pared = (((600 + actual.GetX()) / 30) + ((600 + actual.GetY()) / 30)) % 4;
            actual.SetPared(pared);
            GameObject paredActual;

            //dependiendo de el tipo de paredes que llevara se pone una de cada tipo a cada lado del cuarto
            if (pared == 0)
            {
                paredActual = Instantiate(pared0, actual.GetCuarto().transform);
                paredActual = Instantiate(pared1, actual.GetCuarto().transform);
                paredActual.transform.Translate(30, 0, 0);
            }
            else if (pared == 1)
            {
                paredActual = Instantiate(pared1, actual.GetCuarto().transform);
                paredActual = Instantiate(pared2, actual.GetCuarto().transform);
                paredActual.transform.Translate(30, 0, 0);
            }
            else if (pared == 2)
            {
                paredActual = Instantiate(pared2, actual.GetCuarto().transform);
                paredActual = Instantiate(pared3, actual.GetCuarto().transform);
                paredActual.transform.Translate(30, 0, 0);
            }
            else
            {
                paredActual = Instantiate(pared3, actual.GetCuarto().transform);
                paredActual = Instantiate(pared0, actual.GetCuarto().transform);
                paredActual.transform.Translate(30, 0, 0);
            }

            //Se revisa si el cuarto es el ultimo y no hay nada a alguno de sus lados
            bool finD = true;
            bool finI = true;
            nodo actual2 = cuartos[num_Cuartos - 1];
            //Loop que pasa por todos los nodos
            while (actual2 != null)
            {
                //si hay un cuarto a su derecha o izquierda no es el ultimo
                if ((actual2.GetX() == actual.GetX() + 30) && (actual2.GetY() == actual.GetY()))
                    finD = false;
                if ((actual2.GetX() == actual.GetX() - 30) && (actual2.GetY() == actual.GetY()))
                    finI = false;
                //pasa al siguiente nodo
                if (actual2.GetAnterior() != null)
                    actual2 = actual2.GetAnterior();
                //si no hay mas nodos termina el loop
                else
                    break;
            }//while
            //Si es el ultimo de la izquierda o derecha quita la pared que se puso antes y se sustituye pos una pared de fin
            if (finI)
            {
                num++;
                paredActual = Instantiate(paredFinal, actual.GetCuarto().transform);
                Destroy(actual.GetCuarto().transform.GetChild(0).gameObject);
            }
            if (finD)
            {
                num++;
                Destroy(actual.GetCuarto().transform.GetChild(1).gameObject);
                paredActual = Instantiate(paredFinal, actual.GetCuarto().transform);
                paredActual.transform.Translate(30, 0, 0);
            }

            //crear techos
            //Se elige un techo al azar asegurandose que los cuartos de arriba y abajo tengan coneccion
            int techo = ((600 + actual.GetY()) / 15) % 4;
            actual.SetTecho(techo);
            GameObject techoActual;

            //Se ponen el suelo y techo en base al tipo de techo elegido anteriormente
            if (techo == 0)
            {
                techoActual = Instantiate(techo1, actual.GetCuarto().transform);
                techoActual = Instantiate(techo0, actual.GetCuarto().transform);
                techoActual.transform.Translate(0, -15, 0);

            }
            else if (techo == 1)
            {
                techoActual = Instantiate(techo2, actual.GetCuarto().transform);
                techoActual = Instantiate(techo1, actual.GetCuarto().transform);
                techoActual.transform.Translate(0, -15, 0);

            }
            else if (techo == 2)
            {
                techoActual = Instantiate(techo3, actual.GetCuarto().transform);
                techoActual = Instantiate(techo2, actual.GetCuarto().transform);
                techoActual.transform.Translate(0, -15, 0);

            }
            else
            {
                techoActual = Instantiate(techo0, actual.GetCuarto().transform);
                techoActual = Instantiate(techo3, actual.GetCuarto().transform);
                techoActual.transform.Translate(0, -15, 0);

            }

            bool finA = true;
            bool finB = true;
            //se inicia en el ultimo nodo
            actual2 = cuartos[num_Cuartos - 1];
            //loop que pasa por todos los nodos desde el ultimo hasta la raiz
            while (actual2 != null)
            {
                //Se revisa si el cuarto es el ultimo de arriba o abajo
                if ((actual2.GetX() == actual.GetX()) && (actual2.GetY() == actual.GetY() + 15))
                    finA = false;
                if ((actual2.GetX() == actual.GetX()) && (actual2.GetY() == actual.GetY() - 15))
                    finB = false;

                //pasa al siguiente nodo
                if (actual2.GetAnterior() != null)
                    actual2 = actual2.GetAnterior();
                //si no hay mas nodos termina el loop
                else
                    break;
            }//while
            //si es el ultimo cuarto se sustituye el techo anterior por un techo o suelo de fin
            if (finA)
            {
                Destroy(actual.GetCuarto().transform.GetChild(2+num).gameObject);
                techoActual = Instantiate(techoFinal, actual.GetCuarto().transform);
            }
            if (finB)
            {
                Destroy(actual.GetCuarto().transform.GetChild(3+num).gameObject);
                techoActual = Instantiate(techoFinal, actual.GetCuarto().transform);
                techoActual.transform.Translate(0, -15, 0);
            }
            //siguiente nodo
            if (actual.GetAnterior() != null)
                actual = actual.GetAnterior();
            //si no hay mas nodos termina el loop
            else
                break;
        }//while
    }//Crear Mundos

    void LlenarCuartos()
    {
        //Se comienza en el ultimo nodo del arreglo
        nodo actual = cuartos[num_Cuartos - 1];

        //loop que pasa por todos los nodos desde el ultimo hasta la raiz
        while (actual != null)
        {
                int plataforma = Random.Range(0, 4);
                Instantiate(plataformas[plataforma], actual.GetCuarto().transform);

            //siguiente nodo
            if (actual.GetAnterior() != null)
                actual = actual.GetAnterior();
            //si no hay mas nodos termina el loop
            else
                break;
        }

        Transform[] pos = cuartos[num_Cuartos - 1].GetCuarto().GetComponentInChildren<Spawn_Enemigos>().spawn;
        Instantiate(meta, pos[Random.Range(0, pos.Length)]);
    }
}