using UnityEngine;

namespace FluentUI.Elements
{
	public class Image : Element<Image>
	{
		private UnityEngine.UI.Image _image;

		public Image Color(Color color)
		{
			_image.color = color;
			return this;
		}

		public Image Sprite(Sprite sprite)
		{
			_image.sprite = sprite;
			return this;
		}

		#region Creation
		
		public static Image Create(Transform parent, UIBinding<Sprite> binding)
		{
			var gameObject = new GameObject($"{nameof(Elements.Image)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var component = gameObject.AddComponent<Image>();
			component.CreateUnityComponents(binding);
			return component;
		}

		private void CreateUnityComponents(UIBinding<Sprite> binding)
		{
			_image = gameObject.AddComponent<UnityEngine.UI.Image>();
			var updater = gameObject.AddComponent<UIBindingUpdater>();
			updater.Initialize(binding, x => _image.sprite = x);
		}
		
		public static Image Create(Transform parent, Sprite sprite)
		{
			var gameObject = new GameObject($"{nameof(Elements.Image)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var component = gameObject.AddComponent<Image>();
			component.CreateUnityComponents(sprite);
			return component;
		}

		private void CreateUnityComponents(Sprite sprite)
		{
			_image = gameObject.AddComponent<UnityEngine.UI.Image>();
			_image.sprite = sprite;
		}
		
		#endregion Creation
	}
}