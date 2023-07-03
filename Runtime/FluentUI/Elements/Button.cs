using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Button : Element<Button>
	{
		private event Action _onClick;

		public Button OnClick(Action onClick)
		{
			_onClick += onClick;
			return this;
		}
		
		#region Creation
		
		public static Button Create(Transform parent)
		{
			var gameObject = new GameObject($"{nameof(Button)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var button = gameObject.AddComponent<Button>();
			button.CreateUnityComponents();
			return button;
		}

		private void CreateUnityComponents()
		{
			var button = gameObject.AddComponent<UnityEngine.UI.Button>();
			button.onClick.AddListener(() => _onClick?.Invoke());

			var image = gameObject.AddComponent<UnityEngine.UI.Image>();
			image.sprite = UIRoot.Skin.ButtonSprite;
			image.color = UIRoot.Skin.ButtonColor;
			
			PreferredHeight(UIRoot.Skin.DefaultButtonHeight);
		}

		#endregion
	}
}