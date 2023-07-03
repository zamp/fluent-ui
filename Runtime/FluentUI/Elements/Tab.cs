using FluentUI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Tab : Element<Tab>
	{
		private GameObject _contentGameObject;
		private UnityEngine.UI.Image _backgroundImage;

		public override Transform Content => _contentGameObject.transform;

		public static Tab Create(Transform parent, Tabs tabs, string label)
		{
			var gameObject = new GameObject($"{nameof(Tab)}_{label}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var tab = gameObject.AddComponent<Tab>();
			tab.CreateUnityComponents(tabs, label);
			return tab;
		}

		private void CreateUnityComponents(Tabs tabs, string label)
		{
			_contentGameObject = new GameObject($"{nameof(Tab)}_Content_{label}", typeof(RectTransform));
			var contentRectTransform = (RectTransform)_contentGameObject.transform;
			contentRectTransform.SetParent(tabs.Content, false);
			contentRectTransform.FitToParent();
			_contentGameObject.SetActive(false);
			
			_backgroundImage = gameObject.AddComponent<UnityEngine.UI.Image>();
			_backgroundImage.sprite = UIRoot.Skin.TabInactiveBackground;
			_backgroundImage.color = UIRoot.Skin.TabBackgroundColor;
			_backgroundImage.type = UnityEngine.UI.Image.Type.Sliced;

			var textGameObject = new GameObject("Label", typeof(RectTransform));
			textGameObject.transform.SetParent(transform, false);
			var textRectTransform = textGameObject.GetOrAddComponent<RectTransform>();
			textRectTransform.FitToParent();

			var text = textGameObject.AddComponent<TextMeshProUGUI>();
			text.text = label;
			text.font = UIRoot.Skin.Font;
			text.fontSize = UIRoot.Skin.FontSize;
			text.alignment = TextAlignmentOptions.Center;
			
			var toggle = gameObject.AddComponent<UnityEngine.UI.Toggle>();
			toggle.group = tabs.ToggleGroup;
			toggle.onValueChanged.AddListener(value =>
			{
				_backgroundImage.sprite = value ? UIRoot.Skin.TabActiveBackground : UIRoot.Skin.TabInactiveBackground;
				_contentGameObject.SetActive(value);
			});
			toggle.toggleTransition = UnityEngine.UI.Toggle.ToggleTransition.None;
			toggle.transition = Selectable.Transition.None;
		}
	}
}