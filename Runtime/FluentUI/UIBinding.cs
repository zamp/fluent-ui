using System;

namespace FluentUI
{
	public class UIBinding<T>
	{
		public event Action OnValueChanged;
		
		private T _value;
		
		public T Value
		{
			get => _value;
			set
			{
				_value = value;
				OnValueChanged?.Invoke();
			}
		}
	}
}