using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_Manager : MonoBehaviour
{
    public Canvas canvas;

    bool CheckSide=true;

    public static int Slice_Count;

    public void DragHandler(BaseEventData data) 
    {
        PointerEventData pointerEventData = (PointerEventData)data;
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, pointerEventData.position, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Top"&&CheckSide)
        {
            CheckSide = false;
            Slice_Count++;
        }
        else if(other.tag=="Down"&&!CheckSide)
        {
            CheckSide = true;
            Slice_Count++;
        }
    }
}