using System;
using System.Windows;

namespace Biblioteka.Presentation.Utils
{
    public static class WPFWindowExtensions
    {
        public static void ShowNonBlockingModal(this Window window)
        {
            var parent = window.Owner;
            EventHandler parentDeactivate = (_, __) => { window.Activate(); };
            parent.Activated += parentDeactivate;
            EventHandler window_Closed = (_, __) => { parent.Activated -= parentDeactivate; };
            window.Show();
        }
    }
}