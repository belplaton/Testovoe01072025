using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace waterb.Features.Tab.View
{
    public sealed class TabNavigationView : MonoBehaviour
    {
        [Serializable]
        private struct TabData
        {
            public TabType tabType;
            public Button openButton;
        }

        [SerializeField] private Transform _tabPlaceholder;
        [SerializeField] private Transform _linksList;
        [SerializeField] private TabData[] _tabDataInit = { };
        [SerializeField] private Button _returnButton;
        
        private Dictionary<TabType, Button> _tabButtons;
        public IReadOnlyDictionary<TabType, Button> TabButtons
        {
            get
            {
                if (_tabButtons == null)
                {
                    _tabButtons = new Dictionary<TabType, Button>();
                    for (var i = 0; i < _tabDataInit.Length; i++)
                    {
                        _tabButtons[_tabDataInit[i].tabType] = _tabDataInit[i].openButton;
                    }
                }

                return _tabButtons;
            }
        }

        public Transform TabPlaceholder => _tabPlaceholder;
        public Button ReturnButton => _returnButton;

        public void SwitchViewState(bool isAnyTabActive)
        {
            if (_linksList.gameObject.activeSelf == isAnyTabActive) _linksList.gameObject.SetActive(!isAnyTabActive);
        }
        
        private void OnValidate()
        {
            _tabButtons = null;
        }
    }
}