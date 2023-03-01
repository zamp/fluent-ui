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
	
	public class Group : Element<Group>
	{
		protected LayoutGroup _layoutGroup;
		
		public void Padding(RectOffset padding)
		{
			_layoutGroup.padding = padding;
		}
	}
}