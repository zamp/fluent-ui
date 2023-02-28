using TMPro;
using UnityEngine;

namespace FluentUI
{
	[CreateAssetMenu(fileName = "Skin", menuName = "Fluent-UI/Create Skin")]
	public class Skin : ScriptableObject
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

		public TMP_FontAsset WindowTitleFont;
		public float WindowTitleFontSize = 16;
	}
}