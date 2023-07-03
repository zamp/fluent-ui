using System;
using FluentUI.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Panel : Element<Panel>
	{
		private GameObject _content;

		public override Transform Content => _content.transform;
		
		#region Creation
		
		public static Panel Create(Transform parent)
		{
			var gameObject = new GameObject($"{nameof(Elements.Panel)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var panel = gameObject.AddComponent<Panel>();
			panel.CreateUnityComponents();
			return panel;
		}

		private void CreateUnityComponents()
		{
			var verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.padding = new RectOffset(0, 0, 0, 0);
			verticalLayoutGroup.spacing = 0;
			verticalLayoutGroup.childForceExpandHeight = false;
			
			_content = CreateContent();
		}
		
		private GameObject CreateContent()
		{
			var obj = new GameObject("Content", typeof(RectTransform));
			obj.transform.SetParent(Transform);

			var image = obj.AddComponent<UnityEngine.UI.Image>();
			image.sprite = UIRoot.Skin.PanelSprite;
			image.color = UIRoot.Skin.PanelColor;
				
			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.flexibleHeight = 1;

			return obj;
		}
		
		#endregion Creation
	}
}