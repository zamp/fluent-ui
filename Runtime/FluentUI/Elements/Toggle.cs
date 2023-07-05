using System;
using FluentUI.Extensions;
using TMPro;
using UnityEngine;

namespace FluentUI.Elements
{
	public class Toggle : Element<Toggle>
	{
		private UIBinding<bool> _valueBinding;
		private Image _toggleCheckImage;
		private Button _button;

		private event Action<bool> _onValueChanged;

		public Toggle OnValueChanged(Action<bool> callback)
		{
			_onValueChanged += callback;
			return this;
		}

		public override Toggle PreferredHeight(float height)
		{
			_button.PreferredHeight(height);
			return base.PreferredHeight(height);
		}

		#region Creation
		
		public static Toggle Create(Transform parent, string label, UIBinding<bool> value)
		{
			var gameObject = new GameObject($"{nameof(Toggle)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var toggle = gameObject.AddComponent<Toggle>();
			toggle._valueBinding = value;
			toggle.CreateUnityComponents(label);
			return toggle;
		}

		private void CreateUnityComponents(string label)
		{
			gameObject.GetOrAddComponent<RectTransform>();
			
			FitToParent();

			HorizontalGroup()
				.Padding(0, 0, 0, 0)
				.Spacing(5)
				.Children(
					x => x.Button()
						.PreferredWidthFromHeight()
						.Out(out _button)
						.OnClick(ToggleValue)
						.Image(UIRoot.Skin.ToggleCheckSprite).Out(out _toggleCheckImage).FitToParent(),
					x => x.Label(label)
						.FlexibleWidth(1)
						.Align(TextAlignmentOptions.MidlineLeft));
			
			PreferredHeight(UIRoot.Skin.DefaultToggleHeight);
			
			_toggleCheckImage.gameObject.SetActive(_valueBinding.Value);
			
			var valueUpdater = gameObject.AddComponent<UIBindingUpdater>();
			valueUpdater.Initialize(_valueBinding, value => _toggleCheckImage.gameObject.SetActive(value));
		}

		private void ToggleValue()
		{
			_valueBinding.Value = !_valueBinding.Value;
			_toggleCheckImage.gameObject.SetActive(_valueBinding.Value);
			_onValueChanged?.Invoke(_valueBinding.Value);
		}

		#endregion
	}
}