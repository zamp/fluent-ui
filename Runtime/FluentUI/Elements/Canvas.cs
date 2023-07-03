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

		public Canvas OverrideSortingOrder(int sortingOrder)
		{
			_canvas.overrideSorting = true;
			_canvas.sortingOrder = sortingOrder;
			return this;
		}
		
		#region Creation
		
		internal static Canvas Create(Transform parent)
		{
			var gameObject = new GameObject($"{nameof(Canvas)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var canvas = gameObject.AddComponent<Canvas>();
			canvas._canvas = gameObject.AddComponent<UnityEngine.Canvas>();
			canvas._renderer = gameObject.AddComponent<CanvasRenderer>();
			canvas._raycaster = gameObject.AddComponent<GraphicRaycaster>();
			return canvas;
		}
		
		internal static Canvas CreateOverlay(Transform parent, int sortingOrder)
		{
			var gameObject = new GameObject($"{nameof(Canvas)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var canvas = gameObject.AddComponent<Canvas>();
			canvas._canvas = gameObject.AddComponent<UnityEngine.Canvas>();
			canvas._raycaster = gameObject.AddComponent<GraphicRaycaster>();
			canvas.OverrideSortingOrder(sortingOrder);
			return canvas;
		}

		#endregion Creation
	}
}