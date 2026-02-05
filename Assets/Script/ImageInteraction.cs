using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Vector3 lastMousePosition;
    public Quaternion targetRotation;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float returnSpeed = 2f;
    public ControladorCarta _controlador;
    public bool _returning;
    public Vector3 mouseDelta;
    public Vector3 currentMousePosition;

    private float clickStartTime;
    public bool isHolding;



    void Start()
    {
        if (_controlador != null && _controlador.MainCard != null)
        {
            targetRotation = _controlador.MainCard.transform.localRotation;
        }
        else
        {
            Debug.LogError("Controlador or MainCard not assigned!");
        }
    }

    void Update()
    {
        if (isHolding)
        {
            if (Time.time - clickStartTime >= 0.1f)
            {
                RotateObject();
            }
        }
        else
        {
            _controlador.MainCard.transform.localRotation = Quaternion.RotateTowards(
                _controlador.MainCard.transform.localRotation,
                Quaternion.Euler(0, 180, 0),
                returnSpeed * Time.deltaTime
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_returning) return;

        Debug.Log("Pointer Down");
        lastMousePosition = Input.mousePosition;
        clickStartTime = Time.time; // Record the time of the pointer down
        isHolding = true;

        // Synchronize targetRotation with the current rotation
        targetRotation = _controlador.MainCard.transform.localRotation;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isHolding) return;

    
        isHolding = false;

        if (Time.time - clickStartTime <= 0.1f)
        {
            ShortClickAction();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
   
        isHolding = false;
    }

    private void RotateObject()
    {


        currentMousePosition = Input.mousePosition;
        mouseDelta = currentMousePosition - lastMousePosition;

        //Debug.Log($"Mouse Delta: {mouseDelta}");

        float rotationAmountX = mouseDelta.x * rotationSpeed * Time.deltaTime;
        float rotationAmountY = mouseDelta.y * (rotationSpeed / 2) * Time.deltaTime;

        targetRotation *= Quaternion.Euler(-rotationAmountY, -rotationAmountX, 0);
        lastMousePosition = currentMousePosition;

        _controlador.MainCard.transform.localRotation = Quaternion.Slerp(
            _controlador.MainCard.transform.localRotation,
            targetRotation,
            smoothSpeed * Time.deltaTime
        );
    }

    public void ShortClickAction()
    {
    
        if (!_controlador._changingCard)
        {
            _controlador.DectivateAutomatic();
            StartCoroutine(_controlador.NewCardNumerator());
          //  GetComponent<VibrationController>().SmallVibration();
        }
    }
}

