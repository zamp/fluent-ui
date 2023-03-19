using System;
using FluentUI.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public abstract class Element : MonoBehaviour
	{
	}
	
	public abstract class Element<T> : Element where T : Element
	{
		private LayoutElement _layoutElement;
		public Transform Transform => transform;

		public virtual Transform Content => transform;

		protected RectTransform rectTransform => (RectTransform)transform;

		protected T ClampToParent()
		{
			var parentRectTransform = transform.parent as RectTransform;
			if (parentRectTransform == null)
				return this as T;

			var localPosition = rectTransform.localPosition;
			var rect = rectTransform.rect;
			var parentRect = parentRectTransform.rect;
			
			Vector3 minPosition = parentRect.min - rect.min;
			Vector3 maxPosition = parentRect.max - rect.max;
 
			localPosition.x = Mathf.Clamp(localPosition.x, minPosition.x, maxPosition.x);
			localPosition.y = Mathf.Clamp(localPosition.y, minPosition.y, maxPosition.y);
 
			rectTransform.localPosition = localPosition;
			return this as T;
		}
		
		public T Size(int width, int height)
		{
			return Size(new Vector2(width, height));
		}
		
		public T Size(Vector2 size)
		{
			rectTransform.sizeDelta = size;
			_layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
			_layoutElement.preferredHeight = size.y;
			_layoutElement.preferredWidth = size.x;
			return this as T;
		}
		
		public T Center()
		{
			rectTransform.pivot = Vector2.one / 2f;
			rectTransform.anchorMax = rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
			rectTransform.anchoredPosition = Vector2.zero;
			return this as T;
		}
		
		public T Position(int x, int y)
		{
			return Position(new Vector2(x, y));
		}

		public T Position(Vector2 position)
		{
			rectTransform.pivot = Vector2.zero;
			rectTransform.anchorMax = rectTransform.anchorMin = new Vector2(0f, 0f);
			rectTransform.anchoredPosition = position;
			return this as T;
		}
		
		public T RestorePosition()
		{
			rectTransform.anchoredPosition = Vector2.zero;
			return this as T;
		}
		
		public T Fill()
		{
			rectTransform.pivot = Vector2.one / 2f;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.sizeDelta = Vector2.zero;
			return this as T;
		}

		public T Out(out T value)
		{
			value = this as T;
			return this as T;
		}
		
		public Window Window(string title)
		{
			return Elements.Window.Create(Content, title);
		}
		
		public Button Button()
		{
			return Elements.Button.Create(Content);
		}

		public Label Label(string text)
		{
			return Elements.Label.Create(Content, text);
		}
		
		public Label Label(UIBinding<string> binding)
		{
			return Elements.Label.Create(Content, binding);
		}
		
		public VerticalGroup VerticalGroup()
		{
			return Elements.VerticalGroup.Create(Content);
		}
		
		public HorizontalGroup HorizontalGroup()
		{
			return Elements.HorizontalGroup.Create(Content);
		}
		
		public Panel Panel()
		{
			return Elements.Panel.Create(Content);
		}
		
		public Image Image(Sprite sprite)
		{
			return Elements.Image.Create(Content, sprite);
		}
		
		public Image Image(UIBinding<Sprite> binding)
		{
			return Elements.Image.Create(Content, binding);
		}
		
		public void Children(params Action<Element<T>>[] actions)
		{
			foreach (var action in actions)
			{
				action?.Invoke(this);
			}
		}
	}
}