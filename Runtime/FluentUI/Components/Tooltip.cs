using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FluentUI.Components
{
	public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public string Text;
		
		private void OnEnable()
		{
			if (GetComponent<Graphic>() == null)
			{
				gameObject.AddComponent<EmptyRaycastTarget>();
			}
		}
		
		#region IPointerHandlers

		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			// TODO: show tooltip
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			// TODO: hide tooltip
		}
		
		#endregion IPointerHandlers
	}
}