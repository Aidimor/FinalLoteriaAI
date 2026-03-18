using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PesoController : MonoBehaviour
{
    [SerializeField] private ControladorCarta _scriptMain;
    [SerializeField] private TextMeshProUGUI _totalPesosText;
    [SerializeField] private Interstitial _scriptAd;
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

    public int[] _chestGifts;
    public TextMeshProUGUI _chestGiftText;
    public int _rewardQuantity;
    
    // Start is called before the first frame update
    void Start()
    {
        _rewardQuantity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _totalPesosText.text = SaveSystem.Instance.data._pesos.ToString("f0");
    }

    public void StartButton()
    {
        _startAnimator.SetBool("Start", false);
        if (SaveSystem.Instance.data._pesos > 10)
        {
            SaveSystem.Instance.data._pesos -= 10;
            _startAnimator.SetBool("Start", false);
            //_startButton.SetActive(false);
            SaveSystem.Instance.Save();
            StartCoroutine(_scriptMain.NewCardNumerator());
        }
        else
        {
            _watchAdsPanel.SetActive(true);
            _startAnimator.SetBool("Start", false);
        }

    }

    public void WatchAddButton()
    {
        _watchAdsPanel.SetActive(false);
        _scriptAd.ShowAd();
        ChestGiftSet();
    }

    public void BackAds()
    {
        _startAnimator.SetBool("Start", true);
        _watchAdsPanel.SetActive(false);
    }

    public void ChestGiftSet()
    {
        _chestGiftText.text = _chestGifts[0].ToString();
        _rewardQuantity = _chestGifts[0];
        _chestParent.GetComponent<Animator>().SetTrigger("Chest");
    }

    public void ChestBigRewardChest()
    {
        _chestGiftText.text = _chestGifts[2].ToString();
        _rewardQuantity = _chestGifts[2];
        _chestParent.GetComponent<Animator>().SetTrigger("Chest");
    }

    public void ReceiveChestPrice()
    {
        StartCoroutine(ReceivePrizeNumerator());
    }

    public IEnumerator ReceivePrizeNumerator()
    {
        _chestParent.GetComponent<Animator>().SetTrigger("Open");
        SaveSystem.Instance.data._pesos += _rewardQuantity;
        SaveSystem.Instance.Save();
        _scriptMain._onDailyReward = false;
        if (!GetComponent<ControladorCarta>()._gameStarts)
        {

         yield return new WaitForSeconds(3);
            _startAnimator.SetBool("Start", true);
        }
    }
}
