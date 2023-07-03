using UnityEngine;

namespace FluentUI.Extensions
{
	public static class RectTransformExtensions
	{
		public static void FitToParent(this RectTransform rectTransform)
		{
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.anchoredPosition = Vector2.zero;
		}
	}
}