using UnityEngine;
using waterb.Features.Tab.Clicker.Controller;
using waterb.Features.Tab.View;
using waterb.Features.Tab.Weather.Controller;
using Zenject;

namespace waterb.Features.Tab.Controller
{
    public sealed class TabNavigationController : MonoBehaviour
    { 
        private TabNavigationView _tabNavigationView;
        private ClickerTabController _clickerTabController;
        private WeatherTabController _weatherTabController;
        
        [Inject]
        private void Initialize(TabNavigationView tabNavigationView,
            ClickerTabControllerCreator clickerTabControllerCreator,
            WeatherTabControllerCreator weatherTabControllerCreator)
        {
            _tabNavigationView = tabNavigationView;
            _clickerTabController = clickerTabControllerCreator.Create();
            _weatherTabController = weatherTabControllerCreator.Create();
            
            _tabNavigationView.transform.SetParent(transform);
            _clickerTabController.transform.SetParent(transform);
            _weatherTabController.transform.SetParent(transform);
            foreach (var pair in _tabNavigationView.TabButtons)
            {
                pair.Value.onClick.AddListener(() => SwitchTab(pair.Key));
            }
            
            _tabNavigationView.ReturnButton.onClick.AddListener(() => SwitchTab(null));
        }

        public void SwitchTab(TabType? tabType)
        {
            _tabNavigationView.SwitchViewState(tabType != null);
            if (tabType is null)
            {
                _clickerTabController.HideView();
                _weatherTabController.HideView();
                _weatherTabController.OnTabDeactivated();
            }

            if (tabType is TabType.Clicker)
            {
                _clickerTabController.ShowView(_tabNavigationView.TabPlaceholder);
                _weatherTabController.HideView();
                _weatherTabController.OnTabDeactivated();
            }

            if (tabType is TabType.Weather)
            {
                _clickerTabController.HideView();
                _weatherTabController.ShowView(_tabNavigationView.TabPlaceholder);
                _weatherTabController.OnTabActivated();
            }

            if (tabType is TabType.Breeds)
            {
                _clickerTabController.HideView();
                _weatherTabController.HideView();
                _weatherTabController.OnTabDeactivated();
            }
        }
    }
} 