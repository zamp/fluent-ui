using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Toggle : Element<Toggle>
	{
		private UIBinding<bool> _value;
		private Image _toggleCheckImage;
		private Button _button;

		private event Action<bool> _onValueChanged;

		public Toggle OnValueChanged(Action<bool> callback)
		{
			_onValueChanged += callback;
			return this;
		}

		public override Toggle PreferredHeight(int height)
		{
			_button.PreferredHeight(height);
			return base.PreferredHeight(height);
		}

		#region Creation
		
		public static Toggle Create(Transform parent, string label, UIBinding<bool> value)
		{
			var gameObject = new GameObject($"{nameof(Toggle)}");
			gameObject.transform.SetParent(parent, false);

			var toggle = gameObject.AddComponent<Toggle>();
			toggle._value = value;
			toggle.CreateUnityComponents(label);
			return toggle;
		}

		private void CreateUnityComponents(string label)
		{
			gameObject.AddComponent<RectTransform>();
			
			HorizontalGroup().
				Padding(0,0,0,0).
				Children(
					x => x.Button()
						.PreferredWidthFromHeight()
						.Out(out _button)
						.OnClick(ToggleValue)
						.Image(UIRoot.Skin.ToggleCheckSprite).Out(out _toggleCheckImage).Fill(),
					x => x.Label(label).FlexibleWidth(1));
			
			_toggleCheckImage.gameObject.SetActive(_value.Value);
			
			Fill();
		}

		private void ToggleValue()
		{
			_value.Value = !_value.Value;
			_toggleCheckImage.gameObject.SetActive(_value.Value);
			_onValueChanged?.Invoke(_value.Value);
		}

		#endregion
	}
}