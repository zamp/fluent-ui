using System;

namespace FluentUI
{
	public class UIBinding<T>
	{
		public event Action<T> OnValueChanged;
		
		private T _value;
		
		public T Value
		{
			get => _value;
			set
			{
				if (_value != null && _value.Equals(value))
					return;
				
				_value = value;
				OnValueChanged?.Invoke(_value);
			}
		}
	}
}