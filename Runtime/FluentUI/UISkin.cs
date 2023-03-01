using TMPro;
using UnityEngine;

namespace FluentUI
{
	[CreateAssetMenu(fileName = "Skin", menuName = "Fluent-UI/Create Skin")]
	public class UISkin : ScriptableObject
	{
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

		[Header("Button")]
		public Sprite ButtonSprite;
		public Color ButtonColor;
		
		[Header("Fonts")]
		public TMP_FontAsset Font;
		public float FontSize = 13;

		[Header("Layout Groups")]
		public RectOffset LayoutGroupPadding;
		public float LayoutGroupSpacing;
	}
}