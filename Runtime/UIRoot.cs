using System;
using UnityEngine;

namespace FluentUI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Skin _skin;

        public static Skin Skin => _instance._skin;
        
        private static UIRoot _instance;

        public static UIRoot Instance => GetInstance();

        private static UIRoot GetInstance()
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIRoot>();
            if (_instance == null)
                throw new Exception($"UIRoot does not exist. Add UIRoot gameObject to the first scene that is loaded or instantiate manually.");
            return _instance;
        }

        public static Canvas Canvas()
        {
            return FluentUI.Canvas.Create(Instance.transform);
        }
    }
}
