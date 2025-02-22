using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace MiniScript.MSGS.MUUI
{
    public class DraggableWindow : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IPointerEnterHandler
    {
        //reference for how-to from Code Monkey
        //https://www.youtube.com/watch?v=Mb2oua3FjZg

        public RectTransform draggedRectTransform;
        public UnityEngine.Canvas canvas;
        public Rect UpperLeftBound, LowerRightBound;
        bool _isactive = true;

        public DraggableWindowEndDrag EndDragEvent;

        void Awake()
        {
            EndDragEvent = new DraggableWindowEndDrag();
            draggedRectTransform = GetComponent<RectTransform>();
            //scaleFactor = canvas.scaleFactor;
            
        }

        void Update()
        {

        }

        public void Disable()
        {
            _isactive = false;
        }

        public void Enable()
        {
            _isactive = true;
        }

        public void SetCanvas(ref UnityEngine.Canvas vas)
        {
            canvas = vas;
        }

        [Button]
        public void DebugOutput()
        {
            Debug.Log(canvas.scaleFactor);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!_isactive) return;

            if (draggedRectTransform == null) { Debug.Log("rect null"); }
            if (canvas == null) { Debug.Log("canvas null"); }

            draggedRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

            //check window position to ensure it is within the bounds
            //y is permitted to be between -30 and 22
            //x is permitted to be between -228 and 33
            if (draggedRectTransform.localPosition.y > UpperLeftBound.y)
                draggedRectTransform.localPosition = new Vector3(draggedRectTransform.localPosition.x, UpperLeftBound.y, draggedRectTransform.localPosition.z);
            else if (draggedRectTransform.localPosition.y < LowerRightBound.y)
                draggedRectTransform.localPosition = new Vector3(draggedRectTransform.localPosition.x, LowerRightBound.y, draggedRectTransform.localPosition.z);

            if (draggedRectTransform.localPosition.x < UpperLeftBound.x)
                draggedRectTransform.localPosition = new Vector3(UpperLeftBound.x, draggedRectTransform.localPosition.y, draggedRectTransform.localPosition.z);
            else if (draggedRectTransform.localPosition.x > LowerRightBound.x)
                draggedRectTransform.localPosition = new Vector3(LowerRightBound.x, draggedRectTransform.localPosition.y, draggedRectTransform.localPosition.z);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!_isactive) return;

            GetComponent<RectTransform>().SetAsLastSibling();
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (!_isactive) return;

            EndDragEvent.Invoke(this.gameObject);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log(eventData.pointerEnter.tag);
        }
    }

    public class DraggableWindowEndDrag : UnityEngine.Events.UnityEvent<GameObject>
    {

    }
}

