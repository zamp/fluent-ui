using System;

namespace FluentUI
{
	public class UIBindingWrapper
	{
	}
	
	public class UIBindingWrapper<T> : UIBindingWrapper
	{
		private readonly UIBinding<T> _binding;
		private readonly Action<T> _onValueChanged;

		public UIBindingWrapper(UIBinding<T> binding, Action<T> onValueChanged)
		{
			_binding = binding;
			_binding.OnValueChanged += OnValueChanged;
			
			_onValueChanged = onValueChanged;
			OnValueChanged();
		}

		private void OnValueChanged()
		{
			_onValueChanged?.Invoke(_binding.Value);
		}
	}
}