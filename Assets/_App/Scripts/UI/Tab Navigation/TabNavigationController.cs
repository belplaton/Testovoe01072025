using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace waterb.UI.TabNavigation
{
    public sealed class TabNavigationController : MonoBehaviour
    {
        [System.Serializable]
        private struct TabData
        {
            public TabType tabType;
            public InterfaceReference<ITabPresenter> tabPresenter;
        }

        [SerializeField] private List<TabData> _tabPresentersInit;
        private Dictionary<TabType, ITabPresenter> _tabPresenters;
        private IReadOnlyDictionary<TabType, ITabPresenter> TabPresenters => _tabPresenters ??= InitTabPresenters();
        private Dictionary<TabType, ITabPresenter> InitTabPresenters()
        {
            var dict = new Dictionary<TabType, ITabPresenter>();
            foreach (var tab in _tabPresentersInit)
            {
                dict[tab.tabType] = tab.tabPresenter.Value;
            }
            
            return dict;
        }

        [SerializeField] private TabType defaultTab = TabType.Clicker;
        private TabType? _currentTab;

        private void Start()
        {
            if (_currentTab == null)
            {
                SwitchTab(defaultTab);
            }
        }

        public void SwitchTab(TabType tabType)
        {
            _currentTab = tabType;
            foreach (var tab in TabPresenters)
            {
                tab.Value.HideView();
            }
            
            if (TabPresenters.TryGetValue(tabType, out var view))
            {
                view.ShowView();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _tabPresenters = null;
        }
#endif
    }
} 