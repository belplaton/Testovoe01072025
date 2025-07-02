using System;

namespace waterb.UI.Window
{
    public interface IWindow
    {
        public event Action<IWindow> OnDestroyEvent;
    }
} 