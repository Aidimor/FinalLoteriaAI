using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class ControladorCarta : MonoBehaviour
{
    //[SerializeField] private ClickDragScroller _scriptDrag;
    public GameObject[] _objectsScript;
    public Color MainColor;
    public Image NameBackImage;
    public Image NumberBackImage;
    public Image BackBackImage;
    public GameObject MainCard;
    public GameObject Outline;
    public GameObject BackCard;
    public GameObject InsideBack;
    public float speed;
    MeshRenderer meshRenderer;
    Material instanceMaterial;

    public int OnCard;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private SpriteRenderer _cardImages;
    [SerializeField] private TextMeshProUGUI _number;
    [SerializeField] private TextMeshProUGUI _description;

    public bool _holdingCard;

    [System.Serializable]
    public class AllCards
    {
        public Color _color;
        public Color _color2;
        public Color _color3;
        public Color _color4;
        public Sprite _cardSprite;
        public string _name;
        public string _description;
        public AudioClip _soundEffect;

    }
    public AllCards[] _allCards;


    public int maxNumber = 10; // Maximum number to generate up to
    public List<int> numbers = new List<int>();
    public bool _changingCard;

    public Animator CardAnimator;
    public Animator CameraAnimator;

    public GameObject CamaraParent;
    public Camera _mainCamara;


    public GameObject _cardUIpanel;
    public GameObject _cardParentUI;
    public GameObject _cardParentUIleft;
    public ParticleSystem _cardParticle;
    //public Animator _buttonCardAnimator;
    public TextMeshProUGUI _totalCardsText;
    public List<Transform> _cardUIpos = new List<Transform>();
    public List<GameObject> _cardUIobject = new List<GameObject>();
    public List<GameObject> _cardLeftUIobject = new List<GameObject>();
    public int _cardOnGoing;

    public Image _firstTransform;
    public GameObject _dragController;
    public bool _dragOn;

    public Image[] _mandalas;
    public float _mandalasSpeed;

    public float _camaraSize;

    public bool _leftOn;
    public GameObject _leftPanelParent;
    public Image _rotController;
    public GameObject _mitadMazo;
    public TextMeshProUGUI _middleText;
    public AudioSource _audioS;
    public AudioSource _flipAudio;
    public AudioSource _boomAudio;

    public PostProcessVolume volume;
    private LensDistortion lensDistortion;

    public float _cardUiPos;
    public float _cardLeftUiPos;

    public bool _automaticOn;
    public float _automaticTimer;
    public Image _timerImage;
    public Image _buttonImage;
    public TextMeshProUGUI _timerText;
    public Color[] _buttonColor;
    public int _maxTimer;

    public GameObject _loteriaButton;
    public float _loteriaTimer = 1;
    public Image _loteriaFillImage;
    public GameObject _loteriaParent;
    public Image _loteriaCenterImage;
    public TextMeshProUGUI _loteriaLastCardName;

    public Animator _topMenuAnimator;
    public TextMeshProUGUI _maxTimerButtonText;
    public bool _topMenuDisplayed;
    public Image _imageBlocker;

    public GameObject _cardsLeftParent;

    public Animator _loteriaIconAnimator;
    public Animator _mainMenuAnimator;

    public Image _autoBackground;
    public Color[] _autoBackgroundColor;
    public Animator _shine;
    public AudioSource _loteriaAS;

    public AudioClip[] _chooseSounds;

    public bool _gameStarts;

    // Start is called before the first frame update
    void Start()
    {
     

        meshRenderer = Outline.GetComponent<MeshRenderer>();
        //instanceMaterial = meshRenderer.materials[3]; // Adjust index as needed




        // Retrieve the Lens Distortion effect from the volume
        if (volume.profile.TryGetSettings(out lensDistortion))
        {
            // Optional: Set an initial intensity value
            lensDistortion.intensity.value = 30;
        }
        NewTransformCreation();
        StartCoroutine(StartNumerator());
      
    }

    public IEnumerator StartNumerator()
    {
        yield return new WaitForSeconds(1);
        _loteriaIconAnimator.SetBool("MainIn", true);
    }

    public void DeckCreation()
    {
        MainCard.gameObject.SetActive(true);
        numbers = GenerateList(maxNumber);


        // Shuffle the list
        ShuffleList(numbers);


        List<int> GenerateList(int max)
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= max; i++)
            {
                list.Add(i);
            }
            return list;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, 30, 2 * Time.deltaTime);
        _mainCamara.fieldOfView = Mathf.Lerp(_mainCamara.fieldOfView, _camaraSize, 4 * Time.deltaTime);

        if (OnCard > 0)
        {
            _name.text = _allCards[OnCard - 1]._name;
            _description.text = _allCards[OnCard - 1]._description;
            _cardImages.sprite = _allCards[OnCard - 1]._cardSprite;
            _number.text = OnCard.ToString();
            InsideBack.GetComponent<MeshRenderer>().materials[0].color = _allCards[OnCard - 1]._color;
            //instanceMaterial.SetColor("_OutlineColor", _allCards[OnCard - 1]._color2);
            NumberBackImage.color = _allCards[OnCard - 1]._color3;
            NameBackImage.color = _allCards[OnCard - 1]._color3;
            BackBackImage.color = _allCards[OnCard - 1]._color4;
            Outline.GetComponent<MeshRenderer>().materials[0].color = _allCards[OnCard - 1]._color2;
            Outline.GetComponent<MeshRenderer>().materials[1].color = _allCards[OnCard - 1]._color2;
       
        }
        _totalCardsText.text = numbers.Count.ToString() + "/" + "54";


        _mandalas[0].transform.Rotate(Vector3.forward * _mandalasSpeed * Time.deltaTime);
        _mandalas[1].transform.Rotate(Vector3.forward * _mandalasSpeed * Time.deltaTime);


        _cardParentUI.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(_cardParentUI.GetComponent<RectTransform>().anchoredPosition, new Vector2(_cardUiPos, _cardParentUI.GetComponent<RectTransform>().anchoredPosition.y), 5 * Time.deltaTime);
        _cardParentUIleft.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(_cardParentUIleft.GetComponent<RectTransform>().anchoredPosition, new Vector2(_cardLeftUiPos, _cardParentUIleft.GetComponent<RectTransform>().anchoredPosition.y), 5 * Time.deltaTime);

        if (_gameStarts)
        {
            if (numbers.Count <= 0)
            {       
                StartCoroutine(LastCardNumerator());
                _gameStarts = false;
            }
            else
            {
                if (_automaticOn && !_changingCard && !_topMenuDisplayed)
                {
                    _automaticTimer -= Time.deltaTime;
                    _timerText.text = _automaticTimer.ToString("f0");
                    _timerImage.fillAmount = _automaticTimer / _maxTimer;
                    if (_automaticTimer <= 0)
                    {
                        _timerText.text = "";
                        StartCoroutine(NewCardNumerator());
                    }
                }

            }

            if (_loteriaTimer <= 0)
            {
                _automaticOn = false;
                _loteriaCenterImage.sprite = _allCards[OnCard - 1]._cardSprite;
                _loteriaLastCardName.text = _allCards[OnCard - 1]._name;
                _loteriaParent.SetActive(true);
                MainCard.gameObject.SetActive(false);
                _topMenuAnimator.SetBool("TopIn", false);
                _mainMenuAnimator.SetBool("MenuIn", false);
                _cardParentUI.gameObject.SetActive(false);
                LeftCardsCreation();
                _loteriaAS.Play();
                _imageBlocker.gameObject.SetActive(false);
                _gameStarts = false;

            }
        }


        _loteriaFillImage.fillAmount = _loteriaTimer;
  

    

        _maxTimerButtonText.text = _maxTimer.ToString();

        switch (_automaticOn)
        {
            case true:
                _autoBackground.color = Color.Lerp(_autoBackground.color, _autoBackgroundColor[1], 2 * Time.deltaTime);
                break;
            case false:
                _autoBackground.color = Color.Lerp(_autoBackground.color, _autoBackgroundColor[0], 2 * Time.deltaTime);
                break;
        }
    
    }

    public IEnumerator LastCardNumerator()
    {
        _loteriaCenterImage.sprite = _allCards[OnCard - 1]._cardSprite;
        _loteriaLastCardName.text = _allCards[OnCard - 1]._name;
        Debug.Log("repite");
        LeftCardsCreation();
        yield return new WaitForSeconds(_maxTimer + 1);

        _loteriaParent.SetActive(true);
   
        MainCard.gameObject.SetActive(false);
        _topMenuAnimator.SetBool("TopIn", false);
        _mainMenuAnimator.SetBool("MenuIn", false);
        _cardParentUI.gameObject.SetActive(false);
   

    }

    void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public IEnumerator NewCardNumerator()
    {
        _gameStarts = true;
        _changingCard = true;
        _automaticTimer = _maxTimer;
        _flipAudio.Play();
        _loteriaIconAnimator.SetBool("MainIn", false);
        _cardParentUI.gameObject.SetActive(true);
        _shine.Play("ShineIn");
        if(_allCards[numbers[0] - 1]._soundEffect != null)
        {
            _audioS.clip = _allCards[numbers[0] - 1]._soundEffect;
        }
       
        switch (_cardOnGoing)
        {
            case 26:
                _mitadMazo.SetActive(true);
                CamaraParent.SetActive(false);
                _middleText.text = "Ya sólo queda la otra mitad del mazo";
                _mitadMazo.GetComponent<Animator>().Play("HalfTextIn");
                yield return new WaitForSeconds(2f);
                _mitadMazo.SetActive(false);
                CamaraParent.SetActive(true);
                break;
            case 44:
                _mitadMazo.SetActive(true);
                CamaraParent.SetActive(false);
                _middleText.text = "Ultimas 10 cartas";
                _mitadMazo.GetComponent<Animator>().Play("HalfTextIn");
                yield return new WaitForSeconds(2f);
                _mitadMazo.SetActive(false);
                CamaraParent.SetActive(true);
                break;
            default:
                break;
        }
        switch(_cardOnGoing >= 44)
        {
            case true:
                CamaraParent.GetComponent<Animator>().Play("CardFinal");
                OnCard = numbers[0];
                _cardOnGoing++;
                //CardAnimator.Play("TakeTop");
                //CameraAnimator.SetBool("Enter", true);
                yield return new WaitForSeconds(3.3f);
                _boomAudio.Play();
                yield return new WaitForSeconds(0.5f);
                _audioS.Play();
                lensDistortion.intensity.value = 20;
                NewBotonCreation();
                numbers.RemoveAt(0);
                _changingCard = false;
                break;
            case false:
                //CamaraParent.GetComponent<Animator>().Play("CardNormal");
                CamaraParent.GetComponent<Animator>().Play("CardNormal");
                OnCard = numbers[0];
                _cardOnGoing++;
                //CardAnimator.Play("TakeTop");
                CameraAnimator.SetBool("Enter", true);
                yield return new WaitForSeconds(1);
                _boomAudio.Play();
                yield return new WaitForSeconds(0.5f);
                _audioS.Play();
                lensDistortion.intensity.value = 20;
                NewBotonCreation();
                numbers.RemoveAt(0);
                _changingCard = false;
                break;
        }
        _mainMenuAnimator.SetBool("MenuIn", true);

    }

    public void NewTransformCreation()
    {
        _cardUIpos.Add(_firstTransform.transform);
        for (int i = 1; i < maxNumber; i++)
        {
            Image firstObj = Instantiate(_firstTransform, new Vector2(_firstTransform.transform.localPosition.x + 100 * 1, _firstTransform.transform.localPosition.y), Quaternion.Euler(0, 0, 0));
            firstObj.transform.parent = _cardParentUI.transform;
            firstObj.transform.localPosition = new Vector2(_firstTransform.transform.localPosition.x + 100 * i, _firstTransform.transform.localPosition.y);
            firstObj.transform.name = (i + 1).ToString();
            firstObj.transform.localScale = new Vector3(1, 1, 1);
            _cardUIpos.Add(firstObj.transform);

        }
    }



    public void NewBotonCreation()
    {


        GameObject UIprefab1 = Instantiate(_cardUIpanel, new Vector2(0, 0), Quaternion.Euler(0, 0, 0));
        UIprefab1.transform.parent = _cardParentUI.transform;
        UIprefab1.transform.localPosition = _cardUIpos[_cardOnGoing - 1].transform.localPosition;
        UIprefab1.transform.localScale = new Vector3(0.75f, 0.75f, 1);
        UIprefab1.GetComponent<Image>().color = _allCards[OnCard - 1]._color;
        UIprefab1.GetComponent<CardUiScript>()._cardSprite.sprite = _allCards[OnCard - 1]._cardSprite;
        UIprefab1.GetComponent<CardUiScript>()._numberText.text = OnCard.ToString();
        UIprefab1.GetComponent<CardUiScript>()._nameCard.text = _allCards[OnCard - 1]._name;
        UIprefab1.transform.name = "Card1";
        //UIprefab1.GetComponent<CardUiScript>()._cardID = numbers[OnCard - 1];
        _cardUIobject.Add(UIprefab1);

        if (_cardOnGoing > 1)
        {
            _objectsScript[0].GetComponent<ClickDragScroller>()._xLimit -= -100;        
            _cardUiPos -= 100f;
        }
    }

    public void LeftCardsCreation()
    {

      
        for(int i = 0; i < numbers.Count; i++)
        {
            GameObject UIprefab1 = Instantiate(_cardUIpanel, new Vector2(0, 0), Quaternion.Euler(0, 0, 0));
            UIprefab1.transform.parent = _cardsLeftParent.transform;
            UIprefab1.transform.localPosition = _cardUIpos[i].transform.localPosition;
            UIprefab1.transform.localScale = new Vector3(0.75f, 0.75f, 1);
            //UIprefab1.GetComponent<Image>().color = _allCards[OnCard - 1]._color;
            UIprefab1.GetComponent<CardUiScript>()._cardSprite.sprite = _allCards[numbers[i] - 1]._cardSprite;
            UIprefab1.GetComponent<CardUiScript>()._numberText.text = OnCard.ToString();
            UIprefab1.GetComponent<CardUiScript>()._nameCard.text = _allCards[numbers[i] - 1]._name;
            UIprefab1.transform.name = "CardLeft";
            _cardLeftUIobject.Add(UIprefab1);
            if(i > 1)
            {
                _objectsScript[1].GetComponent<ClickDragScroller>()._xLimit -= -100;
                _cardLeftUiPos -= 100f;
            }

        }
    }






    public void VibratePhone()
    {
        // Check if the device supports vibration
        if (SystemInfo.supportsVibration)
        {
            Handheld.Vibrate();
            //Debug.Log("Phone is vibrating!");
        }
        else
        {
            //Debug.Log("Vibration not supported on this device.");
        }
    }

    public void Camara30Void()
    {
        _camaraSize = 15;
    }

    public void Camara60Void()
    {
        _camaraSize = 35;
    }

    public void OpenCardButton()
    {
        switch (_leftOn)
        {
            case false:
                _leftPanelParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                _rotController.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 850);
                _leftOn = true;
                break;
            case true:
                _leftPanelParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-450, 0);
                _rotController.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1750);
                _leftOn = false;
                break;
        }

    }

    public void ActivateAutomatic()
    {
        _automaticOn = !_automaticOn;
        _timerImage.gameObject.SetActive(_automaticOn);
        _timerImage.fillAmount = 1;
        _automaticTimer = _maxTimer;
        switch (_automaticOn)
        {
            case true:
                _audioS.clip = _chooseSounds[0];
                _audioS.Play();
                _buttonImage.color = _buttonColor[0];
                break;
            case false:
                _audioS.clip = _chooseSounds[1];
                _audioS.Play();
                _buttonImage.color = _buttonColor[1];
                _timerText.text = "II";
                break;
        }
    }

    public void DectivateAutomatic()
    {
        _automaticOn = false;
        _timerImage.gameObject.SetActive(_automaticOn);
        _audioS.clip = _chooseSounds[1];
        _audioS.Play();
        _timerImage.fillAmount = 1;
        _automaticTimer = _maxTimer;
        _timerText.text = "II";
        _buttonImage.color = _buttonColor[1];
    }

    public void MenuButtonVoid()
    {

        _topMenuDisplayed = !_topMenuDisplayed;
        _topMenuAnimator.SetBool("TopIn", _topMenuDisplayed);
        _imageBlocker.gameObject.SetActive(_topMenuDisplayed);
        _automaticTimer = _maxTimer;

    }

    public void SetNewMaxTimer()
    {
        if(_maxTimer >= 5)
        {
            _maxTimer = 1;
        }
        else
        {
            _maxTimer++;
        }
   
    }

    public void RestartButton()
    {
        numbers.Clear();
        _cardOnGoing = 0;
        //_cardsLeftParent.gameObject.SetActive(false);
        _loteriaParent.gameObject.SetActive(false);
        for(int i = 0; i < _cardUIobject.Count; i++)
        {
            Destroy(_cardUIobject[i]);
        }
        _cardUIobject.Clear();
        for(int i = 0; i < _cardLeftUIobject.Count; i++)
        {
            Destroy(_cardLeftUIobject[i]);
        }
        _cardLeftUIobject.Clear();
    
        _cardUiPos = 0;
        _cardLeftUiPos = 0;
        for(int i = 0; i < _objectsScript.Length; i++)
        {
            _objectsScript[i].GetComponent<ClickDragScroller>()._xInitial = 0;
            _objectsScript[i].GetComponent<ClickDragScroller>()._xLimit = 0;
            _objectsScript[i].GetComponent<ClickDragScroller>().newX = 0;
            _objectsScript[i].GetComponent<ClickDragScroller>().lastMousePosition = new Vector2(0, 0);
        }
        _camaraSize = 60;
        CameraAnimator.SetBool("Enter", false);
        _loteriaIconAnimator.SetBool("MainIn", true);
    }


}
