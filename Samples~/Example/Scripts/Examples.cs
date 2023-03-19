using FluentUI;
using FluentUI.Elements;
using TMPro;
using UnityEngine;

namespace FluentUIExamples
{
	public class Examples : MonoBehaviour
	{
		[SerializeField] private Sprite[] _imageSprites;
		
		private readonly UIBinding<string> _labelBinding = new();
		private readonly UIBinding<Sprite> _imageBinding = new();
		
		private Window _windowReference;

		private float _imageTimer;
		private int _imageSpriteIndex;

		public void Start()
		{
			UIRoot.Canvas()
				.ScreenSpaceOverlay()
					.Children(
						x => x.Image(_imageBinding)
							.Size(100,100)
							.Position(100,100),
						x => x.Panel()
							.Size(new Vector2(400,200))
							.Center()
							.VerticalGroup()
								.Children(
									y => y.Label("This is a label"),
									y => y.Button()
										.Size(new Vector2(20,20))
										.OnClick(() => _windowReference.Open())
										.Label("Reopen draggable window").Align(TextAlignmentOptions.Center),
									y => y.Label(_labelBinding)
								),
						x => x.Window("Draggable window")
							.Position(400,400)
							.Size(200,100)
							.Out(out _windowReference)
					);
		}

		private void Update()
		{
			_labelBinding.Value = $"Label with binding: {Time.deltaTime}";
			_imageTimer -= Time.deltaTime;
			if (_imageTimer < 0)
			{
				_imageTimer = 0.1f;
				_imageBinding.Value = _imageSprites[_imageSpriteIndex++ % _imageSprites.Length];
			}
		}
	}
}