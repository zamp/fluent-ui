using System;
using UnityEngine;

namespace FluentUI
{
	public class UIBindingUpdater : MonoBehaviour
	{
		private UIBindingWrapper _wrapper;

		public void Initialize<T>(UIBinding<T> binding, Action<T> onValueChanged)
		{
			_wrapper = new UIBindingWrapper<T>(binding, onValueChanged);
		}
	}
}