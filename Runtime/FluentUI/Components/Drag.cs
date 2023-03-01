using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FluentUI.Components
{
	public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public float DragThreshold = 0;

		public event Action<Vector2> OnDrag; 

		private bool _isDragging;
		private Vector2 _preDrag;

		private bool DragThresholdPassed(PointerEventData eventData)
		{
			return (eventData.position - eventData.pressPosition).magnitude > DragThreshold;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_isDragging = true;
		}

		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!_isDragging)
				return;
			
			if (DragThresholdPassed(eventData))
			{
				OnDrag?.Invoke(eventData.delta + _preDrag);
				_preDrag = Vector2.zero;
			}
			else
			{
				_preDrag += eventData.delta;
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_isDragging = false;
		}
	}
}