using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotator : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] Transform viewPort;
    Vector3 lastPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = Input.mousePosition - lastPosition;
        lastPosition = Input.mousePosition;

        var axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
        viewPort.rotation = Quaternion.AngleAxis(delta.magnitude * 0.5f, axis) * viewPort.rotation;
    }

}
