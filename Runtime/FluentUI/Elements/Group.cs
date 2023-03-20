using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	[Flags]
	public enum GroupForceExpand
	{
		None = 0,
		Vertical = 1 << 0,
		Horizontal = 1 << 1,
		Both = Vertical | Horizontal
	}
	
	public class Group<T> : Element<T> where T : Element
	{
		protected LayoutGroup _layoutGroup;
		
		public T Padding(RectOffset padding)
		{
			_layoutGroup.padding = padding;
			return this as T;
		}
		
		public T Padding(int left, int right, int top, int bottom)
		{
			return Padding(new RectOffset(left, right, top, bottom));
		}
	}
}