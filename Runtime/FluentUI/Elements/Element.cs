using System;
using System.Collections.Generic;
using FluentUI.Components;
using FluentUI.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public abstract class Element : MonoBehaviour
	{
		public virtual Transform Content => transform;
		
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
			if (selection == null)
				throw new NullReferenceException($"Binding is null.");
			return Elements.Dropdown.Create(Content, label, selection, values);
		}
		
		public Toggle Toggle(string label, UIBinding<bool> value)
		{
			if (value == null)
				throw new NullReferenceException($"Binding is null.");
			return Elements.Toggle.Create(Content, label, value);
		}
		
		public Slider Slider(string label, UIBinding<float> value)
		{
			if (value == null)
				throw new NullReferenceException($"Binding is null.");
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
			if (sprite == null)
				throw new NullReferenceException($"Binding is null.");
			return Elements.Image.Create(Content, sprite);
		}
		
		public Canvas OverlayCanvas(int sortingOrder)
		{
			return Canvas.CreateOverlay(Content, sortingOrder);
		}

		public Tabs Tabs()
		{
			return Elements.Tabs.Create(Content);
		}

		public Fold Fold(string label)
		{
			return Elements.Fold.Create(Content, label);
		}
		
		public InputField InputField(string placeholder = "")
		{
			return Elements.InputField.Create(Content, placeholder);
		}
		
		public GridGroup GridGroup()
		{
			return Elements.GridGroup.Create(Content);
		}
		
		#endregion
	}
	
	public abstract class Element<T> : Element where T : Element
	{
		private LayoutElement _layoutElement;
		protected Transform Transform => transform;

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
		
		public virtual T Size(float width, float height, bool updateLayoutElementPreferredSize = true)
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
		
		public virtual T PreferredWidth(float width)
		{
			_layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
			_layoutElement.preferredWidth = width;
			rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
			return this as T;
		}
		
		public virtual T PreferredHeight(float height)
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
		
		public T FitToParent()
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

		public T Disable(UIBinding<bool> condition)
		{
			var canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
			condition.OnValueChanged += value => canvasGroup.interactable = value;
			return this as T;
		}

		public T ContentSizeFit(ContentSizeFitter.FitMode horizontalFit, ContentSizeFitter.FitMode verticalFit)
		{
			var contentSizeFitter = gameObject.GetOrAddComponent<ContentSizeFitter>();
			contentSizeFitter.horizontalFit = horizontalFit;
			contentSizeFitter.verticalFit = verticalFit;
			return this as T; 
		}
		
		#endregion
		
		#region Utilities
		
		public Element<T> Children(params Action<Element<T>>[] actions)
		{
			foreach (var action in actions)
			{
				action?.Invoke(this);
			}

			return this;
		}
		
		public Element<T> Children<TData>(UIBinding<IEnumerable<TData>> data, Action<T, TData> elementFactory)
		{
			data.OnValueChanged += value =>
			{
				Clear();
				foreach (var elementData in data.Value)
				{
					elementFactory(this as T, elementData);
				}
			};
			return this;
		}
		
		public Element<T> Children<TData>(IEnumerable<TData> data, Action<T, TData> elementFactory)
		{
			Clear();
			foreach (var elementData in data)
			{
				elementFactory(this as T, elementData);
			}
			return this;
		}

		private void Clear()
		{
			for (var i = transform.childCount - 1; i >= 0; --i)
			{
				DestroyImmediate(transform.GetChild(i).gameObject);
			}
		}

		public void Dispose()
		{
			DestroyImmediate(gameObject);
		}
		
		#endregion
	}
}