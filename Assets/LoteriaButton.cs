using UnityEngine;
using UnityEngine.EventSystems;

public class LoteriaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ControladorCarta _scriptController;

    private bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        _scriptController._loteriaTimer = 1; // Reinicias cuando suelta
        _scriptController._loteriaButton.transform.localScale = Vector3.MoveTowards(_scriptController._loteriaButton.transform.localScale, new Vector3(1.25f, 1.25f, 1), 2 * Time.deltaTime);
    }

    void Update()
    {
        if (isPressed)
        {
            _scriptController._loteriaTimer -= Time.deltaTime;
            _scriptController._loteriaButton.transform.localScale = Vector3.MoveTowards(_scriptController._loteriaButton.transform.localScale, new Vector3(1f, 1f, 1), 0.5f * Time.deltaTime);
        }
        else
        {
            _scriptController._loteriaButton.transform.localScale = Vector3.MoveTowards(_scriptController._loteriaButton.transform.localScale, new Vector3(0.7f, 0.7f, 1), 5 * Time.deltaTime);
        }
    }
}
