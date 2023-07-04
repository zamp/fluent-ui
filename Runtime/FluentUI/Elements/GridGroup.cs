using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class GridGroup : Group<GridGroup>
	{
		public GridGroup Spacing(Vector2 spacing)
		{
			((GridLayoutGroup)_layoutGroup).spacing = spacing;
			return this;
		}

		public GridGroup CellSize(Vector2 cellSize)
		{
			((GridLayoutGroup)_layoutGroup).cellSize = cellSize;
			return this;
		}
		
		public GridGroup StartCorner(GridLayoutGroup.Corner startCorner)
		{
			((GridLayoutGroup)_layoutGroup).startCorner = startCorner;
			return this;
		}
		
		public GridGroup StartAxis(GridLayoutGroup.Axis axis)
		{
			((GridLayoutGroup)_layoutGroup).startAxis = axis;
			return this;
		}
		
		public GridGroup ChildAlignment(TextAnchor childAlignment)
		{
			((GridLayoutGroup)_layoutGroup).childAlignment = childAlignment;
			return this;
		}
		
		public GridGroup Constraint(GridLayoutGroup.Constraint constraint)
		{
			((GridLayoutGroup)_layoutGroup).constraint = constraint;
			return this;
		}

		#region Creation
		
		public static GridGroup Create(Transform parent)
		{
			var gameObject = new GameObject($"{nameof(GridGroup)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var verticalGroup = gameObject.AddComponent<GridGroup>();
			verticalGroup.CreateUnityComponents();
			return verticalGroup;
		}

		private void CreateUnityComponents()
		{
			var group = gameObject.AddComponent<GridLayoutGroup>();
			group.spacing = UIRoot.Skin.GridLayoutGroupSpacing;
			group.padding = UIRoot.Skin.GridLayoutGroupPadding;
			group.cellSize = UIRoot.Skin.GridLayoutGroupCellSize;
			_layoutGroup = group;
			
			FitToParent();
		}

		#endregion
	}
}