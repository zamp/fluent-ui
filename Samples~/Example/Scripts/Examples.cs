using System;
using System.Collections.Generic;
using FluentUI;
using FluentUI.Elements;
using TMPro;
using UnityEngine;

namespace FluentUIExamples
{
	public class Examples : MonoBehaviour
	{
		private readonly UIBinding<string> _sliderLabel = new();
		private readonly UIBinding<Sprite> _imageBinding = new();
		private readonly UIBinding<int> _dropdownSelectionBinding = new();
		private readonly UIBinding<bool> _toggle = new();
		private readonly UIBinding<float> _sliderValue = new();
		private readonly UIBinding<IEnumerable<(string, Action)>> _dynamicContent = new();
		
		private Window _windowReference;

		public void Start()
		{
			UIRoot.Canvas().ScreenSpaceOverlay()
				.Children(
					x => x.Panel()
						.Size(new Vector2(400,200))
						.Center()
						.VerticalGroup()
							.Children(
								y => y.Label("This is a label"),
								y => y.Button()
									.OnClick(() => _windowReference.Open())
									.Label("Reopen draggable window").Align(TextAlignmentOptions.Center),
								y => y.Dropdown("Dropdown:", _dropdownSelectionBinding, new []{"First example", "Second example", "Third example"})
									.OnSelectionChanged(i => Debug.Log($"Selection changed: {i}")),
								y => y.Toggle("Toggle", _toggle),
								y => y.Label(_sliderLabel),
								y => y.Slider("Slider: ", _sliderValue)
									.Range(1,10)
									.WholeNumbers(true)
									.OnValueChanged(UpdateDraggableWindowContents)
							),
					x => x.Window("Draggable Window")
						.Position(400,400)
						.Size(200,200)
						.Out(out _windowReference)
						.VerticalGroup(GroupForceExpand.Both)
							.Children(_dynamicContent, DynamicContentFactory)
				);
			
			UpdateDraggableWindowContents(_sliderValue.Value);
		}

		private void DynamicContentFactory(VerticalGroup group, (string label, Action callback) item)
		{
			group.Button()
				.OnClick(item.callback)
				.Label(item.label);
		}

		private void UpdateDraggableWindowContents(float value)
		{
			// Callback value is ignored since value can be read from binding. I recommend not doing it like this and instead use the callback value. It's faster and less messy. This is only an example.
			_sliderLabel.Value = $"Slider value: {_sliderValue.Value}, Toggle: {(_toggle.Value ? "on" : "off")}";
			// Use this code instead if you want to use the callback value:
			// _sliderLabel.Value = $"Slider value: {value}, Toggle: {(_toggle.Value ? "on" : "off")}";
			var sliderValue = Mathf.FloorToInt(_sliderValue.Value);
			_dynamicContent.Value = ExampleContents(sliderValue);
		}

		private IEnumerable<(string, Action)> ExampleContents(int sliderValue)
		{
			for (var i = 1; i <= sliderValue; ++i)
			{
				var capture = i;
				yield return (i.ToString(), () => Debug.Log($"Clicked {capture}"));
			}
		}
	}
}