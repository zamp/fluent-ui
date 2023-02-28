using UnityEngine;
using UnityEngine.UI;

namespace FluentUI
{
	public class Canvas : Element
	{
		private readonly UnityEngine.Canvas _canvas;
		private readonly CanvasRenderer _renderer;
		private readonly GraphicRaycaster _raycaster;

		private Canvas(Transform parent) : base(parent)
		{
			_canvas = GameObject.AddComponent<UnityEngine.Canvas>();
			_renderer = GameObject.AddComponent<CanvasRenderer>();
			_raycaster = GameObject.AddComponent<GraphicRaycaster>();
		}
		
		internal static Canvas Create(Transform parent)
		{
			return new Canvas(parent);
		}

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
	}
}