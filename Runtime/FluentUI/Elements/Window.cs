using System;
using FluentUI.Components;
using FluentUI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public enum WindowCenter
	{
		Force,
		RememberPosition
	}
	
	public class Window : Element<Window>
	{
		private GameObject _titleBar;
		private GameObject _closeButton;
		private GameObject _content;
		private GameObject _titleText;
		private string _title;

		private event Action _onClosed;
		private event Action _onOpened;

		public override Transform Content => _content.transform;
		
		private string PrefsY => $"{_title}_y";
		private string PrefsX => $"{_title}_x";

		public Window Close()
		{
			gameObject.SetActive(false);
			_onClosed?.Invoke();
			return this;
		}

		public Window Open()
		{
			gameObject.SetActive(true);
			_onOpened?.Invoke();
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
		
		public Window RestorePositionOr(Action<Window> callback)
		{
			if (PlayerPrefs.HasKey(PrefsX) && PlayerPrefs.HasKey(PrefsY))
			{
				var v = new Vector2(PlayerPrefs.GetFloat(PrefsX, 0f), PlayerPrefs.GetFloat(PrefsY, 0));
				rectTransform.anchoredPosition = v;
			}
			else
			{
				callback?.Invoke(this);
			}

			return this;
		}
		
		private void OnDrag(Vector2 delta)
		{
			var anchoredPosition = rectTransform.anchoredPosition;
			anchoredPosition += delta;
			rectTransform.anchoredPosition = anchoredPosition;
			PlayerPrefs.SetFloat(PrefsX, anchoredPosition.x);
			PlayerPrefs.SetFloat(PrefsY, anchoredPosition.y);
			ClampToParent();
		}
		
		#region Creation
		
		public static Window Create(Transform parent, string title)
		{
			var gameObject = new GameObject($"{nameof(Elements.Window)}_{title}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var window = gameObject.AddComponent<Window>();
			window._title = title;
			window.CreateUnityComponents(title);
			return window;
		}

		private void CreateUnityComponents(string title)
		{
			gameObject.GetOrAddComponent<RectTransform>();

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
			var obj = new GameObject("TitleBar", typeof(RectTransform));
			obj.transform.SetParent(Transform, false);

			var layoutGroup = obj.AddComponent<HorizontalLayoutGroup>();
			layoutGroup.childForceExpandWidth = false;
			layoutGroup.childForceExpandHeight = false;
			layoutGroup.padding = UIRoot.Skin.WindowTitleBarPadding; 

			var image = obj.AddComponent<UnityEngine.UI.Image>();
			image.sprite = UIRoot.Skin.WindowTitleBarSprite;
			image.color = UIRoot.Skin.WindowTitleBarColor;
			
			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.preferredHeight = UIRoot.Skin.WindowTitleBarHeight;
			layoutElement.minHeight = UIRoot.Skin.WindowTitleBarHeight;

			obj.AddComponent<Drag>().OnDrag += OnDrag;

			return obj;
		}

		private GameObject CreateTitleText(GameObject container, string text)
		{
			var obj = new GameObject("TitleText", typeof(RectTransform));
			obj.transform.SetParent(container.transform);

			var textMesh = obj.AddComponent<TextMeshProUGUI>();
			textMesh.font = UIRoot.Skin.Font;
			textMesh.fontSize = UIRoot.Skin.FontSize;
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
			var obj = new GameObject("CloseButton", typeof(RectTransform));
			obj.transform.SetParent(container.transform);

			var image = obj.AddComponent<UnityEngine.UI.Image>();
			image.sprite = UIRoot.Skin.WindowCloseButtonSprite;
			image.color = UIRoot.Skin.WindowCloseButtonColor;

			obj.AddComponent<UnityEngine.UI.Button>().onClick.AddListener(() => Close());

			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.minWidth = layoutElement.preferredWidth = UIRoot.Skin.WindowCloseButtonSize.x;
			layoutElement.minHeight = layoutElement.preferredHeight = UIRoot.Skin.WindowCloseButtonSize.y;
			
			return obj;
		}
		
		private GameObject CreateContent()
		{
			var obj = new GameObject("Content", typeof(RectTransform));
			obj.transform.SetParent(Transform, false);

			var image = obj.AddComponent<UnityEngine.UI.Image>();
			image.sprite = UIRoot.Skin.WindowContentSprite;
			image.color = UIRoot.Skin.WindowContentColor;
				
			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.flexibleHeight = 1;

			return obj;
		}
		
		#endregion Creation
	}
}