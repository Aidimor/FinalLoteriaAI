using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDragScroller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [System.Serializable]
    public enum Type
    {
        Normal,
        Down
    }

    public Type _type;

    [SerializeField] private ControladorCarta _scriptController;
    public RectTransform content;

    private Vector2 lastMousePosition;
    private bool isDragging = false;

    public float dragSensitivity = 1f;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        lastMousePosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        bool canMove = false;
        Vector2 currentMousePosition = eventData.position;
        switch (_type)
        {
            case Type.Normal:
                canMove = _scriptController._cardOnGoing > 3;
                float deltaX = currentMousePosition.x - lastMousePosition.x;
                if (!canMove) return;

                lastMousePosition = currentMousePosition;

                Vector2 pos = content.anchoredPosition;
                pos.x += deltaX * dragSensitivity;

                content.anchoredPosition = pos;
                break;

            case Type.Down:
                canMove = _scriptController.numbers.Count > 4;
                float deltaY = currentMousePosition.y - lastMousePosition.y;
                if (!canMove) return;





                lastMousePosition = currentMousePosition;

                Vector2 posy = content.anchoredPosition;
                posy.y += deltaY * dragSensitivity;

                content.anchoredPosition = posy;
                break;
        }

   
    }
}