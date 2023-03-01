using UnityEngine;

namespace FluentUI
{
	public static class UIPersistence
	{
		public static Vector2 GetOrDefault(string key, Vector2 defaultValue)
		{
			var x = PlayerPrefs.GetFloat($"{key}_x", defaultValue.x);
			var y = PlayerPrefs.GetFloat($"{key}_y", defaultValue.y);
			return new Vector2(x, y);
		}
		
		public static void Set(string key, Vector2 value)
		{
			PlayerPrefs.SetFloat($"{key}_x", value.x);
			PlayerPrefs.SetFloat($"{key}_y", value.y);
		}
	}
}