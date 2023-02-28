using System;
using FluentUI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace FluentUI
{
	public class Window : Element
	{
		private readonly GameObject _titleBar;
		private readonly GameObject _closeButton;
		private readonly GameObject _content;
		private readonly GameObject _titleText;
		
		private bool _isOpen;
		private bool _autoDestroyOnClose = true;

		private event Action _onClosed;
		private event Action _onOpened;

		public override Transform Content => _content.transform;

		private Window(Transform parent, string title)
		{
			GameObject.name = $"{nameof(FluentUI.Window)}_{title}";
			Transform.parent = parent;

			GameObject.GetOrAddComponent<RectTransform>();

			var verticalLayoutGroup = GameObject.GetOrAddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.padding = new RectOffset(0, 0, 0, 0);
			verticalLayoutGroup.spacing = 0;
			verticalLayoutGroup.childForceExpandHeight = false;

			_titleBar = CreateTitleBar();
			_titleText = CreateTitleText(_titleBar, title);
			_closeButton = CreateCloseButton(_titleBar);
			_content = CreateContent();
			
			_isOpen = true;
		}

		private GameObject CreateTitleBar()
		{
			var gameObject = new GameObject("TitleBar");
			gameObject.transform.parent = Transform;

			var layoutGroup = gameObject.AddComponent<HorizontalLayoutGroup>();
			layoutGroup.childForceExpandWidth = false;
			layoutGroup.childForceExpandHeight = false;
			layoutGroup.padding = UIRoot.Skin.WindowTitleBarPadding; 

			var image = gameObject.AddComponent<Image>();
			image.sprite = UIRoot.Skin.WindowTitleBarSprite;
			image.color = UIRoot.Skin.WindowTitleBarColor;
			
			var layoutElement = gameObject.AddComponent<LayoutElement>();
			layoutElement.preferredHeight = UIRoot.Skin.WindowTitleBarHeight;
			layoutElement.minHeight = UIRoot.Skin.WindowTitleBarHeight;

			return gameObject;
		}

		private GameObject CreateTitleText(GameObject container, string text)
		{
			var gameObject = new GameObject("TitleText");
			gameObject.transform.parent = container.transform;

			var textMesh = gameObject.AddComponent<TextMeshProUGUI>();
			textMesh.font = UIRoot.Skin.WindowTitleFont;
			textMesh.fontSize = UIRoot.Skin.WindowTitleFontSize;
			textMesh.text = text;
			textMesh.overflowMode = TextOverflowModes.Ellipsis;
			textMesh.alignment = TextAlignmentOptions.Center;
				
			var layoutElement = gameObject.AddComponent<LayoutElement>();
			layoutElement.flexibleWidth = 1;
			layoutElement.minHeight = UIRoot.Skin.WindowTitleBarHeight - (UIRoot.Skin.WindowTitleBarPadding.bottom + UIRoot.Skin.WindowTitleBarPadding.bottom); 

			return gameObject;
		}

		private GameObject CreateCloseButton(GameObject container)
		{
			var gameObject = new GameObject("CloseButton");
			gameObject.transform.parent = container.transform;

			var image = gameObject.AddComponent<Image>();
			image.sprite = UIRoot.Skin.WindowCloseButtonSprite;
			image.color = UIRoot.Skin.WindowCloseButtonColor;

			gameObject.AddComponent<Button>().onClick.AddListener(() => Close());

			var layoutElement = gameObject.AddComponent<LayoutElement>();
			layoutElement.minWidth = layoutElement.preferredWidth = UIRoot.Skin.WindowCloseButtonSize.x;
			layoutElement.minHeight = layoutElement.preferredHeight = UIRoot.Skin.WindowCloseButtonSize.y;
			
			return gameObject;
		}
		
		private GameObject CreateContent()
		{
			var content = new GameObject("Content");
			content.transform.parent = Transform;

			var image = content.AddComponent<Image>();
			image.sprite = UIRoot.Skin.WindowContentSprite;
			image.color = UIRoot.Skin.WindowContentColor;
				
			var layoutElement = content.AddComponent<LayoutElement>();
			layoutElement.flexibleHeight = 1;

			return content;
		}

		public static Window Create(Transform parent, string title)
		{
			return new Window(parent, title);
		}
		
		public Window Close()
		{
			_isOpen = false;
			_onClosed?.Invoke();

			if (_autoDestroyOnClose)
			{
				Object.DestroyImmediate(GameObject);
			}
			
			return this;
		}

		public Window OnClosed(Action callback)
		{
			_onClosed += callback;
			
			if (!_isOpen)
			{
				callback?.Invoke();
			}
			
			return this;
		}
		
		public Window OnOpened(Action callback)
		{
			_onOpened += callback;
			
			if (_isOpen)
			{
				callback?.Invoke();
			}
				
			return this;
		}
	}
}