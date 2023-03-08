using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerDownEvent : MonoBehaviour, IPointerDownHandler
{
    public event Action<PointerEventData> onPointerDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke(eventData);
    }
}
