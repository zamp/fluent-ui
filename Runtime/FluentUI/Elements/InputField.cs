using System;
using FluentUI.Extensions;
using TMPro;
using UnityEngine;

namespace FluentUI.Elements
{
	public class InputField : Element<InputField>
	{
		private TextMeshProUGUI _text;
		private TMP_InputField _input;

		public InputField Align(TextAlignmentOptions align)
		{
			_text.alignment = align;
			return this;
		}

		public InputField Text(string text)
		{
			_input.text = text;
			return this;
		}

		public InputField OnSubmit(Action<string> onSubmit)
		{
			_input.onSubmit.AddListener(value => onSubmit?.Invoke(value));
			return this;
		}
		
		public InputField OnValueChanged(Action<string> onValueChanged)
		{
			_input.onValueChanged.AddListener(value => onValueChanged?.Invoke(value));
			return this;
		}
		
		public InputField ContentType(TMP_InputField.ContentType contentType)
		{
			_input.contentType = contentType;
			return this;
		}

		#region Creation
		
		public static InputField Create(Transform parent, string placeholder = null)
		{
			var gameObject = new GameObject($"{nameof(InputField)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var button = gameObject.AddComponent<InputField>();
			button.CreateUnityComponents(placeholder);
			return button;
		}
		
		private void CreateUnityComponents(string placeholder)
		{
			var image = gameObject.AddComponent<UnityEngine.UI.Image>();
			image.sprite = UIRoot.Skin.InputFieldSprite;
			
			var textGameObject = new GameObject("Text", typeof(RectTransform));
			textGameObject.transform.SetParent(transform, false);
			((RectTransform)textGameObject.transform).FitToParent();
			
			_text = textGameObject.AddComponent<TextMeshProUGUI>();
			_text.font = UIRoot.Skin.Font;
			_text.fontSize = UIRoot.Skin.FontSize;
			_text.alignment = TextAlignmentOptions.MidlineLeft;
			
			_input = gameObject.AddComponent<TMP_InputField>();
			_input.textComponent = _text;
			_input.textViewport = _text.rectTransform;

			if (placeholder != null)
			{
				var component = new GameObject("Placeholder", typeof(RectTransform));
				component.transform.SetParent(transform, false);
				((RectTransform)component.transform).FitToParent();
				var text = component.AddComponent<TextMeshProUGUI>();
				
				text.text = placeholder;
				text.font = UIRoot.Skin.Font;
				text.fontSize = UIRoot.Skin.FontSize;
				text.alignment = TextAlignmentOptions.MidlineLeft;
				
				_input.placeholder = text;
			}
			
			PreferredHeight(UIRoot.Skin.DefaultInputFieldHeight);

			FitToParent();
		}

		#endregion
	}
}