using System;
using FluentUI.Components;
using FluentUI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Panel : Element<Panel>
	{
		private GameObject _content;
		private UnityEngine.UI.Image _image;

		public override Transform Content => _content.transform;

		public Panel Transparent(bool isTransparent)
		{
			_image.enabled = !isTransparent; 
			return this;
		}
		
		public Panel Color(Color color)
		{
			_image.color = color; 
			return this;
		}
		
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
			_content = CreateContent();
		}
		
		private GameObject CreateContent()
		{
			var obj = new GameObject("Content", typeof(RectTransform));
			obj.transform.SetParent(Transform);

			_image = obj.AddComponent<UnityEngine.UI.Image>();
			_image.sprite = UIRoot.Skin.PanelSprite;
			_image.color = UIRoot.Skin.PanelColor;
				
			var layoutElement = obj.AddComponent<LayoutElement>();
			layoutElement.flexibleHeight = 1;
			
			((RectTransform)obj.transform).FitToParent();

			return obj;
		}
		
		#endregion Creation
	}
}