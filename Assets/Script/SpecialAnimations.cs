using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnimations : MonoBehaviour
{
    public GameObject _controlador;
    public PesoController _scriptPeso;
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

    public void BellSoundEffect()
    {
        _controlador.GetComponent<ControladorCarta>()._bellSound.Play();
    }

    public void ChargeSoundEffect()
    {
        _controlador.GetComponent<ControladorCarta>()._chargeSound.Play();
    }

    public void StopChargeSoundEffect()
    {
        _controlador.GetComponent<ControladorCarta>()._chargeSound.Stop();
    }

    public void PesoCoinParticleEffect()
    {
        _scriptPeso._chessAssets._coinParticle.Play();
    }

}
