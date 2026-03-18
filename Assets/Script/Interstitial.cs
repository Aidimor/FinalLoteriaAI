using UnityEngine;
using UnityEngine.Advertisements;

public class Interstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";

    string _adUnitId;
    bool adLoaded = false;

    void Awake()
    {
        // Obtener el Ad Unit según la plataforma
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    // Cargar anuncio
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Mostrar anuncio
    public void ShowAd()
    {
        if (!adLoaded)
        {
            Debug.Log("Ad not ready yet");
            return;
        }

        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Cuando el anuncio termina de cargar
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId == _adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);
            adLoaded = true;
        }
    }

    // Error al cargar
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error} - {message}");
    }

    // Error al mostrar
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error} - {message}");
    }

    // Cuando inicia el anuncio
    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsShowClick(string adUnitId) { }

    // Cuando termina el anuncio
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState state)
    {
        Debug.Log("Ad Finished");

        adLoaded = false;

        // cargar el siguiente anuncio automáticamente
        LoadAd();
    }
}