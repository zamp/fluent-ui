using System;
using System.Linq;
using FluentUI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Dropdown : Element<Dropdown>
	{
		private string[] _values;
		private GameObject _valuesContainer;
		private Label _selectionText;
		private RectTransform _valuesRectTransform;
		private Canvas _overlayCanvas;

		private event Action<int> _onSelectionChanged;

		public override Dropdown Size(float width, float height, bool updateLayoutPreferredSize = true)
		{
			_valuesRectTransform.sizeDelta = new Vector2(0, height * _values.Length);
			return base.Size(width, height, updateLayoutPreferredSize);
		}

		public override Dropdown PreferredHeight(float height)
		{
			_valuesRectTransform.sizeDelta = new Vector2(0, height * _values.Length);
			return base.PreferredHeight(height);
		}

		public Dropdown OnSelectionChanged(Action<int> callback)
		{
			_onSelectionChanged += callback;
			return this;
		}

		#region Creation
		
		public static Dropdown Create(Transform parent, string label, UIBinding<int> selection, string[] values)
		{
			var gameObject = new GameObject($"{nameof(Dropdown)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var dropdown = gameObject.AddComponent<Dropdown>();
			dropdown._values = values;
			dropdown.CreateUnityComponents(label, selection);
			return dropdown;
		}

		private void CreateUnityComponents(string label, UIBinding<int> selection)
		{
			gameObject.GetOrAddComponent<RectTransform>();
			
			Button buttonGameObject = null;
			
			HorizontalGroup().
				Padding(0,0,0,0).
				Children(
					x => x.Label(label)
						.Align(TextAlignmentOptions.MidlineLeft),
					x => x.Button()
						.FlexibleWidth(1)
						.Out(out buttonGameObject)
						.OnClick(OpenSelection)
						.Children(
							y => y.Label(_values.ElementAtOrDefault(selection.Value))
								.Align(TextAlignmentOptions.MidlineLeft)
								.Out(out _selectionText),
							y => y.OverlayCanvas(UIRoot.OVERLAY_SORTING_ORDER).FitToParent()
								.Out(out _overlayCanvas))
					);
			
			var buttonRt = (RectTransform)buttonGameObject.transform;
			var buttonSizeDelta = buttonRt.sizeDelta;

			_valuesContainer = new GameObject("Values", typeof(RectTransform));
			_valuesContainer.transform.SetParent(_overlayCanvas.transform, false);
			
			_valuesRectTransform = _valuesContainer.GetOrAddComponent<RectTransform>();
			_valuesRectTransform.anchorMin = new Vector2(0, 1);
			_valuesRectTransform.anchorMax = new Vector2(1, 1);
			_valuesRectTransform.pivot = new Vector2(0, 1);
			_valuesRectTransform.anchoredPosition = Vector2.zero;
			_valuesRectTransform.sizeDelta = new Vector2(0, _values.Length * buttonSizeDelta.y);

			var layoutElement = _valuesContainer.AddComponent<LayoutElement>();
			layoutElement.ignoreLayout = true;

			_valuesContainer.AddComponent<VerticalLayoutGroup>();

			for (var i = 0; i < _values.Length; ++i)
			{
				var capture = i;

				Button().SetParent(_valuesContainer.transform).OnClick(() =>
					{
						selection.Value = capture;
						_valuesContainer.SetActive(false);
						_selectionText.Text(_values[capture])
							.Align(TextAlignmentOptions.MidlineLeft);
						_onSelectionChanged?.Invoke(capture);
					})
					.Label(_values[i]);
			}
			
			_valuesContainer.SetActive(false);

			PreferredHeight(UIRoot.Skin.DefaultDropdownHeight);

			FitToParent();
			
			var valueUpdater = gameObject.AddComponent<UIBindingUpdater>();
			valueUpdater.Initialize(selection, value => _selectionText.Text(_values[value]));
		}

		private void OpenSelection()
		{
			_overlayCanvas.OverrideSortingOrder(UIRoot.OVERLAY_SORTING_ORDER);
			_valuesContainer.SetActive(true);
		}

		#endregion
	}
}