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
            public InterfaceReference<ITabView> view;
        }

        [SerializeField] private List<TabData> _tabViewsInit;
        private Dictionary<TabType, ITabView> _tabViews;
        private IReadOnlyDictionary<TabType, ITabView> TabViews => _tabViews ??= InitTabViews();
        private Dictionary<TabType, ITabView> InitTabViews()
        {
            var dict = new Dictionary<TabType, ITabView>();
            foreach (var tab in _tabViewsInit)
            {
                dict[tab.tabType] = tab.view.Value;
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
            foreach (var tab in TabViews)
            {
                tab.Value.Hide();
            }
            
            if (TabViews.TryGetValue(tabType, out var view))
            {
                view.Show();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _tabViews = null;
        }
#endif
    }
} 