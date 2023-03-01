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

		#region Creation
		
		public static Label Create(Transform parent, string value)
		{
			var gameObject = new GameObject($"{nameof(Label)}");
			gameObject.transform.parent = parent;

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

			Fill();
		}

		#endregion
	}
}