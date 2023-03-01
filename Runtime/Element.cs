using UnityEngine;

namespace FluentUI
{
	public abstract class Element : MonoBehaviour
	{
		public Transform Transform => transform;

		public virtual Transform Content => transform;

		protected RectTransform rectTransform => (RectTransform)transform;
		
		public Window Window(string title)
		{
			return FluentUI.Window.Create(Content, title);
		}

		protected Element ClampToParent()
		{
			var parentRectTransform = transform.parent as RectTransform;
			if (parentRectTransform == null)
				return this;

			var localPosition = rectTransform.localPosition;
			var rect = rectTransform.rect;
			var parentRect = parentRectTransform.rect;
			
			Vector3 minPosition = parentRect.min - rect.min;
			Vector3 maxPosition = parentRect.max - rect.max;
 
			localPosition.x = Mathf.Clamp(localPosition.x, minPosition.x, maxPosition.x);
			localPosition.y = Mathf.Clamp(localPosition.y, minPosition.y, maxPosition.y);
 
			rectTransform.localPosition = localPosition;
			return this;
		}
	}
}