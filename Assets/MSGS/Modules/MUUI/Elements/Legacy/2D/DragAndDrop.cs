using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using MiniScript;
using MiniScript.MSGS;
using System;
using System.Data;
using MiniScript.MSGS.MUUI.Extensions;
using UnityEditor;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{

    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private UnityEngine.Canvas canvas;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            // Cache the RectTransform and Canvas components
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<UnityEngine.Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        void Update() { }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Make the object interactive during drag
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.8f; // Optional: make it semi-transparent
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (canvas == null)
                return;

            // Move the RectTransform with the drag
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //check position/placement, ensure its appropriate



            // Restore the object state
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1.0f; // Reset transparency
        }
    }
}
