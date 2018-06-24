using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    public void SetCamara(float arriba, float abajo, float derecha, float izquierda)
    {
        float medioX = izquierda + (Mathf.Abs(izquierda) + Mathf.Abs(derecha)) * 0.5f;
        float medioY = abajo + (Mathf.Abs(abajo) + Mathf.Abs(arriba)) * 0.5f;

        this.transform.position = new Vector3(medioX, medioY, -15);

        arriba /= 15;
        abajo /= 15;
        derecha /= 30;
        izquierda /= 30;

        medioX = Mathf.Abs(izquierda) + Mathf.Abs(derecha);
        medioY = Mathf.Abs(abajo) + Mathf.Abs(arriba);

        if (medioX < medioY)
        {
            this.GetComponent<Camera>().orthographicSize = (medioY + 1)  * 10;
            Debug.Log("alto: "  + this.GetComponent<Camera>().orthographicSize);
        }
        else
        {
            this.GetComponent<Camera>().orthographicSize = (medioX + 1)  * 10;
            Debug.Log("ancho: " + this.GetComponent<Camera>().orthographicSize);
        }

        this.transform.parent.gameObject.SetActive(false);
    }
}