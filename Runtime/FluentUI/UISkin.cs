using TMPro;
using UnityEngine;

namespace FluentUI
{
	[CreateAssetMenu(fileName = "Skin", menuName = "Fluent-UI/Create Skin")]
	public class UISkin : ScriptableObject
	{
		[Header("General")]
		public float DefaultSpacing = 5;
		
		[Header("Window")]
		public Sprite WindowTitleBarSprite;
		public float WindowTitleBarHeight = 20;
		public Color WindowTitleBarColor;
		public RectOffset WindowTitleBarPadding;
		
		public Sprite WindowContentSprite;
		public Color WindowContentColor;
		
		public Sprite WindowCloseButtonSprite;
		public Color WindowCloseButtonColor;
		
		public Vector2 WindowCloseButtonSize;
		
		[Header("Label")]
		public float DefaultLabelHeight = 20;
		
		[Header("InputField")]
		public float DefaultInputFieldHeight = 20;
		public Sprite InputFieldSprite;

		[Header("Button")]
		public Sprite ButtonSprite;
		public Color ButtonColor;
		public float DefaultButtonHeight = 20;
		
		[Header("Fonts")]
		public TMP_FontAsset Font;
		public float FontSize = 13;

		[Header("Layout Groups")]
		public RectOffset LayoutGroupPadding;
		public float LayoutGroupSpacing;

		[Header("Panel")]
		public Sprite PanelSprite;
		public Color PanelColor;

		[Header("Toggle")]
		public Sprite ToggleCheckSprite;
		public float DefaultToggleHeight = 20;

		[Header("Slider")]
		public Sprite SliderFillSprite;
		public Color SliderFillColor;
		public Sprite SliderHandleSprite;
		public Color SliderHandleColor;

		[Header("Tabs")]
		public float TabHeight;
		public Sprite TabInactiveBackground;
		public Sprite TabActiveBackground;
		public Color TabBackgroundColor;

		[Header("Fold")]
		public Sprite FoldClosedSprite;
		public Sprite FoldOpenSprite;
		
		[Header("Dropdown")]
		public float DefaultDropdownHeight = 20;
	}
}