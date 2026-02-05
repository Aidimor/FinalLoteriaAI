using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDragScroller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [System.Serializable]
    public enum Type
    {
        Normal,
        Left
    }
    public Type _type;

    [SerializeField] private ControladorCarta _scriptController;
    public RectTransform content;

    public Vector2 lastMousePosition;
    public bool isDragging = false;
    public bool hasMoved = false;  // 🔥 evita movimiento en el click

    public float _xInitial;
    public float _xLimit;

    public float newX;


    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        hasMoved = false;  // 🔥 Aún NO se ha movido
      
        lastMousePosition = new Vector2(newX, eventData.position.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        Vector2 currentMousePosition = eventData.position;
        Vector2 delta = currentMousePosition - lastMousePosition;


        newX = lastMousePosition.x;

        if (!hasMoved)
        {
            if (delta.magnitude < 0)   // 🔥 tolerancia para no mover en el click
            {
                return; // no mover aún
            }

            hasMoved = true; // ahora sí está moviendo
        }


        lastMousePosition = currentMousePosition;

    }

    void Update()
    {

        switch (_type)
        {
            case Type.Normal:
                if (_scriptController._cardOnGoing > 3 && hasMoved)
                {
                    if (isDragging)
                    {
                        content.anchoredPosition = Vector2.Lerp(
               content.anchoredPosition,
               new Vector2(lastMousePosition.x, content.anchoredPosition.y),
               2f * Time.deltaTime
           );
                    }
                    else
                    {
                        content.anchoredPosition = Vector2.Lerp(
                                       content.anchoredPosition,
                                       new Vector2(newX, content.anchoredPosition.y),
                                       2f * Time.deltaTime
                                   );
                    }
  
                }
                break;
            case Type.Left:
                if (_scriptController.numbers.Count > 3 && hasMoved)
                {
           
                    if (isDragging)
                    {
                        content.anchoredPosition = Vector2.Lerp(
                   content.anchoredPosition,
                   new Vector2(lastMousePosition.x, content.anchoredPosition.y),
                   2f * Time.deltaTime);
                    }
                    else
                    {
                        content.anchoredPosition = Vector2.Lerp(
                                        content.anchoredPosition,
                                        new Vector2(newX, content.anchoredPosition.y),
                                        2f * Time.deltaTime
                                    );
                    }
                }
                break;
        }

    }
}
