using UnityEngine;

public partial class LevelCreator
{
    //Clase nodo, de aqui salen las habitaciones
    private class Nodo
    {
        private int x, y;
        private int pared, techo;
        private Nodo anterior;
        private Direcciones dir;
        private GameObject cuarto;

        public Nodo()
        {
        }

        public void SetAnterior(Nodo _anterior)
        {
            anterior = _anterior;
        }

        public void SetX(int _x)
        {
            x = _x;
        }

        public void SetY(int _y)
        {
            y = _y;
        }

        public void SetDireccion(Direcciones _dir)
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

        public Nodo GetAnterior()
        {
            return anterior;
        }

        public int GetX()
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

        public Direcciones GetDireccion()
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
}