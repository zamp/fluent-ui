using UnityEngine;

namespace FluentUI
{
	public abstract class Element
	{
		internal GameObject GameObject { get; }

		public Transform Transform => GameObject.transform;

		public Element Parent { get; }

		public virtual Transform Content => GameObject.transform;

		protected Element()
		{
			GameObject = new GameObject($"{GetType().Name}");
		}

		protected Element(Transform parent)
		{
			GameObject = new GameObject($"{GetType().Name}");
			Transform.parent = parent;
		}
		
		protected Element(Element parent)
		{
			GameObject = new GameObject($"{GetType().Name}");
			Parent = parent;
			Transform.parent = parent.Transform;
		}
		
		public Window Window(string title)
		{
			return FluentUI.Window.Create(Content, title);
		}
	}
}