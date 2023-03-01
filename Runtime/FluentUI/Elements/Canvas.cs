using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Canvas : Element<Canvas>
	{
		private UnityEngine.Canvas _canvas;
		private CanvasRenderer _renderer;
		private GraphicRaycaster _raycaster;

		public Canvas ScreenSpace(Camera camera)
		{
			_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			_canvas.worldCamera = camera;
			return this;
		}
		
		public Canvas WorldSpace(Camera camera)
		{
			_canvas.renderMode = RenderMode.WorldSpace;
			_canvas.worldCamera = camera;
			return this;
		}
		
		public Canvas ScreenSpaceOverlay()
		{
			_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			return this;
		}
		
		#region Creation
		
		internal static Canvas Create(Transform parent)
		{
			var gameObject = new GameObject($"{nameof(Canvas)}");
			gameObject.transform.parent = parent;

			var window = gameObject.AddComponent<Canvas>();
			window.CreateUnityComponents();
			return window;
		}

		private void CreateUnityComponents()
		{
			_canvas = gameObject.AddComponent<UnityEngine.Canvas>();
			_renderer = gameObject.AddComponent<CanvasRenderer>();
			_raycaster = gameObject.AddComponent<GraphicRaycaster>();
		}

		#endregion Creation
	}
}