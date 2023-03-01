using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace FluentUI
{
	public enum WindowCenter
	{
		Force,
		RememberPosition
	}
	
	public class Window : Element
	{
		private GameObject _titleBar;
		private GameObject _closeButton;
		private GameObject _content;
		private GameObject _titleText;

		private event Action _onClosed;
		private event Action _onOpened;

		public override Transform Content => _content.transform;

		public Window Close()
		{
			_onClosed?.Invoke();
			gameObject.SetActive(false);
			return this;
		}

		public Window OnClosed(Action callback)
		{
			_onClosed += callback;
			
			if (!gameObject.activeSelf)
			{
				callback?.Invoke();
			}
			
			return this;
		}
		
		public Window OnOpened(Action callback)
		{
			_onOpened += callback;
			
			if (gameObject.activeSelf)
			{
				callback?.Invoke();
			}
				
			return this;
		}

		public Window Size(Vector2 size)
		{
			rectTransform.sizeDelta = size;
			return this;
		}
		
		public Window Center()
		{
			rectTransform.pivot = Vector2.one / 2f;
			rectTransform.anchorMax = rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
			rectTransform.anchoredPosition = Vector2.zero;
			return this;
		}
		
		public Window RestorePosition()
		{
			rectTransform.anchoredPosition = Vector2.zero;
			return this;
		}
		
		public Window Fill()
		{
			rectTransform.pivot = Vector2.one / 2f;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.sizeDelta = Vector2.zero;
			return this;
		}
		
		#region Creation
		
		public static Window Create(Transform parent, string title)
		{
			var gameObject = new GameObject($"{nameof(FluentUI.Window)}_{title}");
			gameObject.transform.parent = parent;

			var window = gameObject.AddComponent<Window>();
			window.CreateUnityComponents(title);
			return window;
		}

		private void CreateUnityComponents(string title)
		{
			gameObject.AddComponent<RectTransform>();

			var verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.padding = new RectOffset(0, 0, 0, 0);
			verticalLayoutGroup.spacing = 0;
			verticalLayoutGroup.childForceExpandHeight = false;
			
			_titleBar = CreateTitleBar();
			_titleText = CreateTitleText(_titleBar, title);
			_closeButton = CreateCloseButton(_titleBar);
			_content = CreateContent();
		}
		
		private GameObject CreateTitleBar()
		{
			var obj = new GameObject("TitleBar");
			obj.transform.parent = Transform;

			var layoutGroup = obj.AddComponent<HorizontalLayoutGroup>();
			layoutGroup.childForceExpandWidth = false;
			layoutGroup.childForceExpandHeight = false;
			layoutGroup.padding = UIRoot.Skin.WindowTitleBarPadding; 

			var image = obj.AddComponent<Image>();
			image.sprite = UIRoot.Skin.WindowTitleBarSprite;
			image.color = UIRoot.Skin.WindowTitleBarColor;
			
			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.preferredHeight = UIRoot.Skin.WindowTitleBarHeight;
			layoutElement.minHeight = UIRoot.Skin.WindowTitleBarHeight;

			obj.AddComponent<DragElement>().OnDrag += OnDrag;

			return obj;
		}

		private void OnDrag(Vector2 delta)
		{
			rectTransform.anchoredPosition += delta;
			ClampToParent();
		}

		private GameObject CreateTitleText(GameObject container, string text)
		{
			var obj = new GameObject("TitleText");
			obj.transform.parent = container.transform;

			var textMesh = obj.AddComponent<TextMeshProUGUI>();
			textMesh.font = UIRoot.Skin.WindowTitleFont;
			textMesh.fontSize = UIRoot.Skin.WindowTitleFontSize;
			textMesh.text = text;
			textMesh.overflowMode = TextOverflowModes.Ellipsis;
			textMesh.alignment = TextAlignmentOptions.Center;
				
			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.flexibleWidth = 1;
			layoutElement.minHeight = UIRoot.Skin.WindowTitleBarHeight - (UIRoot.Skin.WindowTitleBarPadding.bottom + UIRoot.Skin.WindowTitleBarPadding.bottom); 

			return obj;
		}

		private GameObject CreateCloseButton(GameObject container)
		{
			var obj = new GameObject("CloseButton");
			obj.transform.parent = container.transform;

			var image = obj.AddComponent<Image>();
			image.sprite = UIRoot.Skin.WindowCloseButtonSprite;
			image.color = UIRoot.Skin.WindowCloseButtonColor;

			obj.AddComponent<Button>().onClick.AddListener(() => Close());

			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.minWidth = layoutElement.preferredWidth = UIRoot.Skin.WindowCloseButtonSize.x;
			layoutElement.minHeight = layoutElement.preferredHeight = UIRoot.Skin.WindowCloseButtonSize.y;
			
			return obj;
		}
		
		private GameObject CreateContent()
		{
			var obj = new GameObject("Content");
			obj.transform.parent = Transform;

			var image = obj.AddComponent<Image>();
			image.sprite = UIRoot.Skin.WindowContentSprite;
			image.color = UIRoot.Skin.WindowContentColor;
				
			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.flexibleHeight = 1;

			return obj;
		}
		
		#endregion Creation
	}
}