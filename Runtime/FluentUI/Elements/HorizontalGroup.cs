using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class HorizontalGroup : Group<HorizontalGroup>
	{
		public HorizontalGroup Spacing(float spacing)
		{
			((HorizontalLayoutGroup)_layoutGroup).spacing = spacing;
			return this;
		}

		#region Creation
		
		public static HorizontalGroup Create(Transform parent, GroupForceExpand forceExpand = GroupForceExpand.Vertical)
		{
			var gameObject = new GameObject($"{nameof(HorizontalGroup)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var verticalGroup = gameObject.AddComponent<HorizontalGroup>();
			verticalGroup.CreateUnityComponents(forceExpand);
			return verticalGroup;
		}

		private void CreateUnityComponents(GroupForceExpand forceExpand)
		{
			var group = gameObject.AddComponent<HorizontalLayoutGroup>();
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