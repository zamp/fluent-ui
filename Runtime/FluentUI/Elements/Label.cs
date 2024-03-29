using TMPro;
using UnityEngine;

namespace FluentUI.Elements
{
	public class Label : Element<Label>
	{
		private TextMeshProUGUI _text;

		public Label Align(TextAlignmentOptions align)
		{
			_text.alignment = align;
			return this;
		}

		public Label Text(string text)
		{
			_text.text = text;
			return this;
		}

		#region Creation
		
		public static Label Create(Transform parent, UIBinding<string> binding)
		{
			var gameObject = new GameObject($"{nameof(Label)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var button = gameObject.AddComponent<Label>();
			button.CreateUnityComponents(binding);
			return button;
		}
		
		public static Label Create(Transform parent, string value)
		{
			var gameObject = new GameObject($"{nameof(Label)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var button = gameObject.AddComponent<Label>();
			button.CreateUnityComponents(value);
			return button;
		}

		private void CreateUnityComponents(string value)
		{
			_text = gameObject.AddComponent<TextMeshProUGUI>();
			_text.font = UIRoot.Skin.Font;
			_text.fontSize = UIRoot.Skin.FontSize;
			_text.text = value;

			PreferredHeight(UIRoot.Skin.DefaultLabelHeight);

			FitToParent();
		}
		
		private void CreateUnityComponents(UIBinding<string> binding)
		{
			_text = gameObject.AddComponent<TextMeshProUGUI>();
			_text.font = UIRoot.Skin.Font;
			_text.fontSize = UIRoot.Skin.FontSize;
			_text.alignment = TextAlignmentOptions.MidlineLeft;

			var valueUpdater = gameObject.AddComponent<UIBindingUpdater>();
			valueUpdater.Initialize(binding, value => _text.text = value);

			FitToParent();
		}

		#endregion
	}
}