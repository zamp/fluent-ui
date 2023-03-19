using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class VerticalGroup : Group
	{
		#region Creation
		
		public static VerticalGroup Create(Transform parent, GroupForceExpand forceExpand = GroupForceExpand.Horizontal)
		{
			var gameObject = new GameObject($"{nameof(VerticalGroup)}");
			gameObject.transform.parent = parent;

			var verticalGroup = gameObject.AddComponent<VerticalGroup>();
			verticalGroup.CreateUnityComponents(forceExpand);
			return verticalGroup;
		}

		private void CreateUnityComponents(GroupForceExpand forceExpand)
		{
			var group = gameObject.AddComponent<VerticalLayoutGroup>();
			group.spacing = UIRoot.Skin.LayoutGroupSpacing;
			group.padding = UIRoot.Skin.LayoutGroupPadding;
			group.childForceExpandWidth = forceExpand.HasFlag(GroupForceExpand.Horizontal);
			group.childForceExpandHeight = forceExpand.HasFlag(GroupForceExpand.Vertical);
			_layoutGroup = group;
			
			Fill();
		}

		#endregion
	}
}