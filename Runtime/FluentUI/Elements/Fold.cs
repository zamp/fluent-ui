using FluentUI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Elements
{
	public class Fold : Element<Fold>
	{
		private UIBinding<bool> _isOpenBinding = new();
		private Image _foldImage;
		private Button _button;
		private GameObject _contentGameObject;
		private ContentSizeFitter _contentSizeFitter;

		public override Transform Content => _contentGameObject.transform; 

		public override Fold PreferredHeight(float height)
		{
			_button.PreferredHeight(height);
			return base.PreferredHeight(height);
		}

		public Fold Toggle(bool isOpen)
		{
			if (_isOpenBinding.Value != isOpen)
			{
				ToggleOpen();
			}

			return this;
		}

		#region Creation
		
		public static Fold Create(Transform parent, string label)
		{
			var gameObject = new GameObject($"{nameof(Elements.Fold)}", typeof(RectTransform));
			gameObject.transform.SetParent(parent, false);

			var toggle = gameObject.AddComponent<Fold>();
			toggle.CreateUnityComponents(label);
			return toggle;
		}

		private void CreateUnityComponents(string label)
		{
			gameObject.GetOrAddComponent<RectTransform>();

			var verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.childForceExpandHeight = false;
			verticalLayoutGroup.childControlHeight = false;

			_contentGameObject = gameObject;
			
			FitToParent();

			HorizontalGroup()
				.ContentSizeFit(ContentSizeFitter.FitMode.Unconstrained, ContentSizeFitter.FitMode.PreferredSize)
				.SetParent(transform)
				.Padding(0, 0, 0, 0)
				.Spacing(5)
				.Children(
					x => x.Button()
						.PreferredWidthFromHeight()
						.Out(out _button)
						.OnClick(ToggleOpen)
						.Image(UIRoot.Skin.FoldClosedSprite).Out(out _foldImage).FitToParent(),
					x => x.Label(label)
						.FlexibleWidth(1)
						.Align(TextAlignmentOptions.MidlineLeft));
			
			_contentGameObject = new GameObject($"{nameof(Fold)}_Content_{label}", typeof(RectTransform));
			verticalLayoutGroup = _contentGameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.childForceExpandHeight = false;
			var contentRectTransform = (RectTransform)_contentGameObject.transform;
			contentRectTransform.SetParent(transform, false);
			contentRectTransform.anchorMin = new Vector2(0, 1);
			contentRectTransform.anchorMax = new Vector2(1, 1);
			contentRectTransform.pivot = new Vector2(0.5f, 1f);
			contentRectTransform.anchoredPosition = new Vector2(0, 0);
			contentRectTransform.sizeDelta = new Vector2(0, 0);
			_contentGameObject.SetActive(_isOpenBinding.Value);
			
			_contentSizeFitter = Content.gameObject.GetOrAddComponent<ContentSizeFitter>();
			_contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			
			ChangeFoldImage(_isOpenBinding.Value);
			
			var valueUpdater = gameObject.AddComponent<UIBindingUpdater>();
			valueUpdater.Initialize(_isOpenBinding, value => _foldImage.gameObject.SetActive(value));
		}

		private void ToggleOpen()
		{
			_isOpenBinding.Value = !_isOpenBinding.Value;
			
			_contentGameObject.SetActive(_isOpenBinding.Value);
			_contentSizeFitter.enabled = _isOpenBinding.Value;
			ChangeFoldImage(_isOpenBinding.Value);
			
			// i've no idea why these both have to be called but just calling one won't work
			UnityEngine.Canvas.ForceUpdateCanvases();
			LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
		}

		private void ChangeFoldImage(bool isOpen)
		{
			_foldImage.Sprite(isOpen ? UIRoot.Skin.FoldOpenSprite : UIRoot.Skin.FoldClosedSprite);
		}

		#endregion
	}
}