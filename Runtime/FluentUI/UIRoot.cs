using System;
using System.Collections.Generic;
using UnityEngine;
using Canvas = FluentUI.Elements.Canvas;

namespace FluentUI
{
    public class UIRoot : MonoBehaviour
    {
        public const int OVERLAY_SORTING_ORDER = 100;
        
        [SerializeField] private UISkin _skin;

        public static UISkin Skin => _instance._skin;
        
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
            return Elements.Canvas.Create(Instance.transform);
        }
    }
}
