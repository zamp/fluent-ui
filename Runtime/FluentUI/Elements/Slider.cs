using System;
using FluentUI.Components;
using FluentUI.Extensions;
using TMPro;
using UnityEngine;

namespace FluentUI.Elements
{
	public class Slider : Element<Slider>
	{
		private UIBinding<float> _valueBinding;
		private UnityEngine.UI.Slider _slider;
		private Image _handleImage;
		private VerticalGroup _sliderPadding;
		
		private event Action<float> _onValueChanged;

		public Slider OnValueChanged(Action<float> callback)
		{
			_onValueChanged += callback;
			return this;
		}

		public Slider Direction(UnityEngine.UI.Slider.Direction direction)
		{
			_slider.direction = direction;
			return this;
		}

		public Slider Range(float min, float max)
		{
			_slider.minValue = min;
			_slider.maxValue = max;
			return this;
		}

		public override Slider PreferredHeight(float height)
		{
			_handleImage.Size(height, 0, false);
			_sliderPadding.Padding(Mathf.FloorToInt(height / 2), Mathf.FloorToInt(height / 2), 0, 0);
			return base.PreferredHeight(height);
		}

		#region Creation
		
		public static Slider Create(Transform parent, string label, UIBinding<float> value)
		{
			var gameObject = new GameObject($"{nameof(Slider)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var toggle = gameObject.AddComponent<Slider>();
			toggle.CreateUnityComponents(label, value);
			return toggle;
		}

		private void CreateUnityComponents(string label, UIBinding<float> value)
		{
			Empty sliderEmpty = null;
			Image fillImage = null;
			
			_valueBinding = value;
			gameObject.GetOrAddComponent<RectTransform>();

			var half = Vector2.one / 2f;

			HorizontalGroup().Padding(0, 0, 0, 0)
				.Children(
					x => x.Label(label).Align(TextAlignmentOptions.MidlineLeft),
					x => x.VerticalGroup(GroupForceExpand.Both)
						.FlexibleWidth(1)
						.Out(out _sliderPadding)
						.Empty()
							.Out(out sliderEmpty)
							.Children(
									y => y.Image(UIRoot.Skin.SliderFillSprite)
										.Color(UIRoot.Skin.SliderFillColor)
										.Out(out fillImage)
										.FitToParent(),
									y => y.Image(UIRoot.Skin.SliderHandleSprite)
										.Color(UIRoot.Skin.SliderHandleColor)
										.AnchorMin(half)
										.AnchorMax(half)
										.Pivot(half)
										.AnchoredPosition(Vector2.zero)
										.IgnoreLayout()
										.Out(out _handleImage))
				);

			var sliderGameObject = sliderEmpty.gameObject;
			sliderGameObject.name = $"{nameof(Slider)}";

			sliderGameObject.AddComponent<EmptyRaycastTarget>();

			_slider = sliderGameObject.AddComponent<UnityEngine.UI.Slider>();

			_slider.fillRect = fillImage.gameObject.GetOrAddComponent<RectTransform>();
			_slider.handleRect = _handleImage.gameObject.GetOrAddComponent<RectTransform>();
			
			FitToParent();
			
			var valueUpdater = sliderGameObject.AddComponent<UIBindingUpdater>();
			valueUpdater.Initialize(_valueBinding, v => _slider.SetValueWithoutNotify(v));

			_slider.onValueChanged.AddListener(ChangeSliderValue);

			PreferredHeight(UIRoot.Skin.SliderDefaultHeight);
		}

		private void ChangeSliderValue(float value)
		{
			_valueBinding.Value = value;
			_onValueChanged?.Invoke(value);
		}

		#endregion
	}
}