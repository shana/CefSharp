using CefSharp.Internals;
using System;
using System.Windows;
using System.Windows.Input;

namespace CefSharp.Wpf.Internals
{
    public class DefaultFocusHandler : IFocusHandler
    {
        private readonly ChromiumWebBrowser browser;

        public DefaultFocusHandler(ChromiumWebBrowser browser)
        {
            this.browser = browser;
        }

        public virtual void OnGotFocus()
        {
        }

		// Do not let the browser take focus when a Load method has been called
		public virtual bool OnSetFocus(CefFocusSource source)
        {
            return source == CefFocusSource.FocusSourceNavigation;
        }

		// NOTE: OnTakeFocus means leaving focus / not taking focus
		public virtual void OnTakeFocus(bool next)
        {
			// Creating a FocusNavigationDirection object and setting it to a 
			// local field that contains the direction selected.
			var focusDirection = next ? FocusNavigationDirection.Next : FocusNavigationDirection.Previous;

			// MoveFocus takes a TraveralReqest as its argument.
			var request = new TraversalRequest(focusDirection);
			browser.Dispatcher.Invoke(new Action(() => browser.MoveFocus(request)));
        }
    }
}
