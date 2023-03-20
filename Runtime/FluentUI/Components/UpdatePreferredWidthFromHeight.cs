using FluentUI.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace FluentUI.Components
{
	public class UpdatePreferredWidthFromHeight : MonoBehaviour
	{
		private LayoutElement _layoutElement;

		private void OnEnable()
		{
			_layoutElement = gameObject.GetOrAddComponent<LayoutElement>();
		}

		private void Update()
		{
			_layoutElement.preferredWidth = _layoutElement.preferredHeight;
		}
	}
}