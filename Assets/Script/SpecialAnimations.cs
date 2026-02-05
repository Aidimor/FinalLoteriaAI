using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnimations : MonoBehaviour
{
    public GameObject _controlador;
    // Start is called before the first frame update

    public void to30()
    {
        _controlador.GetComponent<ControladorCarta>().Camara30Void();
    }

    public void to60()
    {
        _controlador.GetComponent<ControladorCarta>().Camara60Void();
    }

    public void CardParticle()
    {
        _controlador.GetComponent<ControladorCarta>()._cardParticle.Play();
    }

}
