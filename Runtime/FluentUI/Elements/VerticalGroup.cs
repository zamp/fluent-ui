using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class VerticalGroup : Group<VerticalGroup>
	{
		public VerticalGroup Spacing(float spacing)
		{
			((HorizontalLayoutGroup)_layoutGroup).spacing = spacing;
			return this;
		}
		
		#region Creation
		
		public static VerticalGroup Create(Transform parent, GroupForceExpand forceExpand = GroupForceExpand.Horizontal)
		{
			var gameObject = new GameObject($"{nameof(VerticalGroup)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

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
			
			FitToParent();
		}

		#endregion
	}
}