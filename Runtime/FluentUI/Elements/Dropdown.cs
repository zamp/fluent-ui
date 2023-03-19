using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Dropdown : Element<Dropdown>
	{
		private TextMeshProUGUI _text;
		private UnityEngine.UI.Button _button;
		private string[] _values;
		private GameObject _valuesContainer;
		private Label _selectionText;
		private RectTransform _valuesRectTransform;
		
		private event Action<int> _onSelectionChanged;

		public override Dropdown Size(int width, int height)
		{
			_valuesRectTransform.sizeDelta = new Vector2(0, height * _values.Length);
			return base.Size(width, height);
		}

		public Dropdown OnSelectionChanged(Action<int> callback)
		{
			_onSelectionChanged += callback;
			return this;
		}

		#region Creation
		
		public static Dropdown Create(Transform parent, string label, UIBinding<int> selection, string[] values)
		{
			var gameObject = new GameObject($"{nameof(Dropdown)}");
			gameObject.transform.SetParent(parent, false);

			var dropdown = gameObject.AddComponent<Dropdown>();
			dropdown._values = values;
			dropdown.CreateUnityComponents(label, selection);
			return dropdown;
		}

		private void CreateUnityComponents(string label, UIBinding<int> selection)
		{
			gameObject.AddComponent<RectTransform>();
			
			Button buttonGameObject = null;
			
			HorizontalGroup().
				Padding(0,0,0,0).
				Children(
					x => x.Label(label),
					x => x.Button()
						.FlexibleWidth(1)
						.Out(out buttonGameObject)
						.OnClick(OpenSelection)
						.Label(_values.ElementAtOrDefault(selection.Value)).Out(out _selectionText)
					);
			
			var buttonRt = (RectTransform)buttonGameObject.transform;
			var buttonSizeDelta = buttonRt.sizeDelta;

			_valuesContainer = new GameObject("Values");
			_valuesContainer.transform.SetParent(buttonRt, false);

			_valuesRectTransform = _valuesContainer.AddComponent<RectTransform>();
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
						_selectionText.SetText(_values[capture]);
						_onSelectionChanged?.Invoke(capture);
					})
					.Label(_values[i]);
			}
			
			_valuesContainer.SetActive(false);

			Fill();
		}

		private void OpenSelection()
		{
			_valuesContainer.SetActive(true);
		}

		private void CreateUnityComponents(UIBinding<string> binding)
		{
			_text = gameObject.AddComponent<TextMeshProUGUI>();
			_text.font = UIRoot.Skin.Font;
			_text.fontSize = UIRoot.Skin.FontSize;

			var valueUpdater = gameObject.AddComponent<UIBindingUpdater>();
			valueUpdater.Initialize(binding, value => _text.text = value);
		}

		#endregion
	}
}