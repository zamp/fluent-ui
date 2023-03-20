using System;
using FluentUI.Components;
using FluentUI.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace FluentUI.Elements
{
	public abstract class Element : MonoBehaviour
	{
	}
	
	public abstract class Element<T> : Element where T : Element
	{
		private LayoutElement _layoutElement;
		protected Transform Transform => transform;

		protected virtual Transform Content => transform;

		protected RectTransform rectTransform => (RectTransform)transform;
		
		#region Settings & Properties

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
		
		public T AnchorMin(Vector2 anchorMin)
		{
			rectTransform.anchorMin = anchorMin;
			return this as T;
		}
		
		public T AnchorMax(Vector2 anchorMax)
		{
			rectTransform.anchorMax = anchorMax;
			return this as T;
		}
		
		public T Pivot(Vector2 pivot)
		{
			rectTransform.pivot = pivot;
			return this as T;
		}
		
		public T AnchoredPosition(Vector2 anchoredPosition)
		{
			rectTransform.anchoredPosition = anchoredPosition;
			return this as T;
		}
		
		public T IgnoreLayout()
		{
			var layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
			layoutElement.ignoreLayout = true;
			return this as T;
		}
		
		public virtual T Size(int width, int height, bool updateLayoutElementPreferredSize = true)
		{
			return Size(new Vector2(width, height));
		}
		
		public T Size(Vector2 size, bool updateLayoutElementPreferredSize = true)
		{
			rectTransform.sizeDelta = size;
			
			if (updateLayoutElementPreferredSize)
			{
				_layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
				_layoutElement.preferredHeight = size.y;
				_layoutElement.preferredWidth = size.x;
			}
			
			return this as T;
		}
		
		public virtual T PreferredWidth(int width)
		{
			_layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
			_layoutElement.preferredWidth = width;
			rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
			return this as T;
		}
		
		public virtual T PreferredHeight(int height)
		{
			_layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
			_layoutElement.preferredHeight = height;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
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

		public T SetParent(Transform parent)
		{
			rectTransform.SetParent(parent, false);
			return this as T;
		}
		
		public T SetParent(Element parent)
		{
			rectTransform.SetParent(parent.transform, false);
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

		public T FlexibleWidth(float width)
		{
			var layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
			layoutElement.flexibleWidth = width;
			return this as T;
		}
		
		public T FlexibleHeight(float height)
		{
			var layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
			layoutElement.flexibleHeight = height;
			return this as T;
		}

		public T PreferredWidthFromHeight()
		{
			gameObject.GetOrAddComponent<UpdatePreferredWidthFromHeight>();
			return this as T;
		}
		
		#endregion
		
		#region Elements
		
		public Empty Empty()
		{
			return Elements.Empty.Create(Content);
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
		
		public Label Label(UIBinding<string> text)
		{
			return Elements.Label.Create(Content, text);
		}
		
		public Dropdown Dropdown(string label, UIBinding<int> selection, string[] values)
		{
			return Elements.Dropdown.Create(Content, label, selection, values);
		}
		
		public Toggle Toggle(string label, UIBinding<bool> value)
		{
			return Elements.Toggle.Create(Content, label, value);
		}
		
		public Slider Slider(string label, UIBinding<float> value)
		{
			return Elements.Slider.Create(Content, label, value);
		}
		
		public VerticalGroup VerticalGroup(GroupForceExpand forceExpand = GroupForceExpand.Horizontal)
		{
			return Elements.VerticalGroup.Create(Content, forceExpand);
		}
		
		public HorizontalGroup HorizontalGroup(GroupForceExpand forceExpand = GroupForceExpand.Vertical)
		{
			return Elements.HorizontalGroup.Create(Content, forceExpand);
		}
		
		public Panel Panel()
		{
			return Elements.Panel.Create(Content);
		}
		
		public Image Image(Sprite sprite)
		{
			return Elements.Image.Create(Content, sprite);
		}
		
		public Image Image(UIBinding<Sprite> sprite)
		{
			return Elements.Image.Create(Content, sprite);
		}
		
		public Canvas OverlayCanvas(int sortingOrder)
		{
			return Canvas.CreateOverlay(Content, sortingOrder);
		}
		
		#endregion
		
		#region Utilities
		
		public void Children(params Action<Element<T>>[] actions)
		{
			foreach (var action in actions)
			{
				action?.Invoke(this);
			}
		}

		public void DestroySelf()
		{
			DestroyImmediate(gameObject);
		}
		
		#endregion
	}
}