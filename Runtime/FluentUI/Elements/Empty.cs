using UnityEngine;

namespace FluentUI.Elements
{
	public class Empty : Element<Empty>
	{
		public static Empty Create(Transform parent)
		{
			var gameObject = new GameObject($"{nameof(Empty)}");
			gameObject.transform.SetParent(parent, false);

			var empty = gameObject.AddComponent<Empty>();
			return empty;
		}
	}
}