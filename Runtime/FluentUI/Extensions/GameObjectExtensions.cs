using UnityEngine;

namespace FluentUI.Extensions
{
	public static class GameObjectExtensions
	{
		public static T GetOrAddComponent<T>(this GameObject component) where T : Component
		{
			if (!component.TryGetComponent<T>(out var comp))
				comp = (T)component.AddComponent(typeof(T));
			return comp;
		}
	}
}