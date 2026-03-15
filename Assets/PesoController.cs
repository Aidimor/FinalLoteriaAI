using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PesoController : MonoBehaviour
{
    [SerializeField] private ControladorCarta _scriptMain;
    [SerializeField] private TextMeshProUGUI _totalPesosText;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _watchAdsPanel;
    [SerializeField] private GameObject _chestParent;
    [System.Serializable]
    public class ChestAssets
    {
        public Animator _chestAnimator;
        public ParticleSystem _coinParticle;
    }
    public ChestAssets _chessAssets;

    public Animator _startAnimator;
    public int _totalPesos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _totalPesosText.text = _totalPesos.ToString("f0");
    }

    public void StartButton()
    {
        _startAnimator.SetBool("Start", false);
        if (_totalPesos > 10)
        {
            _totalPesos -= 10;
            _startButton.SetActive(false);

            StartCoroutine(_scriptMain.NewCardNumerator());
        }
        else
        {
            _watchAdsPanel.SetActive(true);
        }

    }

    public void WatchAddButton()
    {
        _watchAdsPanel.SetActive(false);
    }

    public void BackAds()
    {
        _startAnimator.SetBool("Start", true);
        _watchAdsPanel.SetActive(false);
    }
}
