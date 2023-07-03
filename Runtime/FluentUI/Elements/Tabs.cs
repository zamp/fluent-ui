using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Tabs : Element<Tabs>
	{
		private GameObject _tabsContainer;
		private GameObject _contentContainer;
		
		private ToggleGroup _toggleGroup;

		public override Transform Content => _contentContainer.transform;

		public ToggleGroup ToggleGroup => _toggleGroup;

		public static Tabs Create(Transform parent)
		{
			var gameObject = new GameObject($"{nameof(Tabs)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var tabs = gameObject.AddComponent<Tabs>();
			tabs.CreateUnityComponents();
			return tabs;
		}

		private void CreateUnityComponents()
		{
			_toggleGroup = gameObject.AddComponent<ToggleGroup>();
			_toggleGroup.allowSwitchOff = false;
			
			var verticalGroup = gameObject.AddComponent<VerticalLayoutGroup>();
			verticalGroup.childForceExpandWidth = true;
			verticalGroup.childForceExpandHeight = false;

			_tabsContainer = new GameObject("Tabs", typeof(RectTransform));
			_contentContainer = new GameObject("Content", typeof(RectTransform));
			
			_tabsContainer.transform.SetParent(transform, false);
			_contentContainer.transform.SetParent(transform, false);

			var tabsLayoutElement = _tabsContainer.AddComponent<LayoutElement>();
			tabsLayoutElement.preferredHeight = UIRoot.Skin.TabHeight;
			tabsLayoutElement.flexibleHeight = 0;
			
			var tabsLayoutGroup = _tabsContainer.AddComponent<HorizontalLayoutGroup>();
			tabsLayoutGroup.childForceExpandHeight = true;

			var contentLayoutElement = _contentContainer.AddComponent<LayoutElement>();
			contentLayoutElement.flexibleHeight = 1;
			
			FitToParent();
		}

		public Tab Tab(string label)
		{
			return Elements.Tab.Create(_tabsContainer.transform, this, label);
		}
	}
}