using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoteriaCardScript : MonoBehaviour
{
    [SerializeField] private ControladorCarta _scriptMain;

    public List<GameObject> _allCards = new List<GameObject>();
    public List<int> _cardsSelected = new List<int>();

    public Transform _cardParent;
    public GameObject _subCardObject;
    public RectTransform[] _cardPoses;

 
    public Animator[] _allLines;



    void Start()
    {
        SaveSystem.Instance.Load();
        if (SaveSystem.Instance.data._cardSet == false)
        {
            GenerateCards();
        }
        else
        {
            Debug.Log("salvado");
        }
        CardCreator();
    }

    void GenerateCards()
    {
        List<int> numbers = new List<int>();

        for (int i = 1; i <= 54; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < numbers.Count; i++)
        {
            int randomIndex = Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        _cardsSelected.Clear();

        for (int i = 0; i < 16; i++)
        {
            _cardsSelected.Add(numbers[i]);
        }

        for(int i = 0; i < 16; i++)
        {
            SaveSystem.Instance.data._loteriaCardsID[i] = _cardsSelected[i];
        }
        SaveSystem.Instance.data._cardSet = true;
        SaveSystem.Instance.Save();
        
    }



    public void CardCreator()
    {
        var data = SaveSystem.Instance.data;
        for (int i = 0; i < 16; i++)
        {
            GameObject Subcard = Instantiate(_subCardObject, transform.position, transform.rotation);

            Subcard.transform.SetParent(_cardParent, false);

            Subcard.GetComponent<RectTransform>().anchoredPosition =
                _cardPoses[i].anchoredPosition;

            var cardScript = Subcard.GetComponent<CardUiScript>();

            cardScript._nameCard.text = _scriptMain._allCards[data._loteriaCardsID[i]]._name;
            cardScript._cardSprite.sprite = _scriptMain._allCards[data._loteriaCardsID[i]]._cardSprite;
            cardScript._Background.color = _scriptMain._allCards[data._loteriaCardsID[i]]._color;
            cardScript._numberText.text = data._loteriaCardsID[i].ToString();
            cardScript._cardID = data._loteriaCardsID[i];

            Subcard.GetComponent<RectTransform>().localScale = new Vector2(0.16f, 0.16f);

            _allCards.Add(Subcard);
        }

        SetCards();
    }

    public void CheckPatterns()
    {
        var data = SaveSystem.Instance.data;
        int[,] lines = new int[,]
        {
            {0,1,2,3},
            {4,5,6,7},
            {8,9,10,11},
            {12,13,14,15},

            {0,4,8,12},
            {1,5,9,13},
            {2,6,10,14},
            {3,7,11,15},

            {0,5,10,15},
            {3,6,9,12}
        };

        for (int i = 0; i < lines.GetLength(0); i++)
        {
            if (data._cardMarked[lines[i, 0]] &&
                data._cardMarked[lines[i, 1]] &&
                data._cardMarked[lines[i, 2]] &&
                data._cardMarked[lines[i, 3]])
            {
                if (!data._lineTriggered[i])
                {
                    data._lineTriggered[i] = true;

                    Debug.Log("Linea ganadora index: " + i);

                    if (_allLines[i] != null)
                        _allLines[i].SetTrigger("Line");
                    _scriptMain._chessAnimator.SetTrigger("Chest");
                    _scriptMain._scriptPeso.ChestGiftSet();
    
                }
            }
        }

        // CUADRO (centro)
        if (data._cardMarked[5] &&
            data._cardMarked[6] &&
            data._cardMarked[9] &&
            data._cardMarked[10])
        {
            if (!data._lineTriggered[10])
            {
                data._lineTriggered[10] = true;

                Debug.Log("Linea ganadora index: 10");

                if (_allLines[10] != null)
                    _allLines[10].SetTrigger("Line");
                _scriptMain._chessAnimator.SetTrigger("Chest");
                _scriptMain._scriptPeso.ChestGiftSet();
            }
        }

        // LOTERIA
        bool all = true;

        for (int i = 0; i < data._cardMarked.Length; i++)
        {
            if (!data._cardMarked[i])
            {
                all = false;
                break;
            }
        }

        if (all)
        {
            if (!data._lineTriggered[11])
            {
               data. _lineTriggered[11] = true;

                Debug.Log("Linea ganadora index: 11");

                if (_allLines[11] != null)
                    _allLines[11].SetTrigger("Square");
                _scriptMain._chessAnimator.SetTrigger("Chest");
                _scriptMain._scriptPeso.ChestGiftSet();
            }
        }
    }

    public void SpaceVoid()
    {
        var data = SaveSystem.Instance.data;
        int random;

        do
        {
            random = Random.Range(0, 16);
        }
        while (data._cardMarked[random]);

        _allCards[random]
            .GetComponent<CardUiScript>()
            ._parentAnimator
            .SetBool("CardOn", true);

        data._cardMarked[random] = true;

        CheckPatterns();
        SaveSystem.Instance.Save();
    }

    public void SetCards()
    {
        var data = SaveSystem.Instance.data;

        //while (data._cardMarked[random]);
        for (int i = 0; i < 16; i++){
            if (data._cardMarked[i])
            {
                _allCards[i]
                                .GetComponent<CardUiScript>()
                                ._parentAnimator
                                .SetBool("CardOn", true);
            }
        
        }
    


    }
}