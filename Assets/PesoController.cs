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

    public void ChestGiftSet()
    {
        _chestGiftText.text = _chestGifts[0].ToString();
        _rewardQuantity = _chestGifts[0];
    }

    public void ChestBigRewardChest()
    {
        _chestGiftText.text = _chestGifts[2].ToString();
        _rewardQuantity = _chestGifts[2];
    }

    public void ReceiveChestPrice()
    {
        _chestParent.GetComponent<Animator>().SetTrigger("Open");
        SaveSystem.Instance.data._pesos += _rewardQuantity;
        SaveSystem.Instance.Save();
        _scriptMain._onDailyReward = false;
    }
}
