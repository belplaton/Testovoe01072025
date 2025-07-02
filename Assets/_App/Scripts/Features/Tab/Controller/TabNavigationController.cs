using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace waterb.Features.Tab.Controller
{
    public sealed class TabNavigationController : MonoBehaviour
    {
        [System.Serializable]
        private struct TabData
        {
            public TabType tabType;
            public InterfaceReference<ITabController> tabController;
        }

        [SerializeField] private List<TabData> _tabControllersInit;
        private Dictionary<TabType, ITabController> _tabControllers;
        private IReadOnlyDictionary<TabType, ITabController> TabControllers => _tabControllers ??= InitTabControllers();
        private Dictionary<TabType, ITabController> InitTabControllers()
        {
            var dict = new Dictionary<TabType, ITabController>();
            foreach (var tab in _tabControllersInit)
            {
                dict[tab.tabType] = tab.tabController.Value;
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
            foreach (var tab in TabControllers)
            {
                tab.Value.HideView();
            }
            
            if (TabControllers.TryGetValue(tabType, out var view))
            {
                view.ShowView();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _tabControllers = null;
        }
#endif
    }
} 