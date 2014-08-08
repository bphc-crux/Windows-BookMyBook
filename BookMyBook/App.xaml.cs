using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Resources.Core;
using BookMyBook.Common;
using Windows.Networking.Connectivity;
// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace BookMyBook
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        //Popup _settingsPopup;
        Rect _windowBounds;
        public static bool IsInternetAvailable
        {
            get
            {
                var profiles = NetworkInformation.GetConnectionProfiles();
                var internetProfile = NetworkInformation.GetInternetConnectionProfile();
                return profiles.Any(s => s.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                    || (internetProfile != null
                            && internetProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            }
        }
        private async void OpenHelp(IUICommand command)
        {
            Uri uri = new Uri("mailto:srujanjha.jha@gmail.com");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }
        void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            _windowBounds = Window.Current.Bounds;
        }
        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                //_settingsPopup.IsOpen = false;
            }
        }
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            Window.Current.Activate();
            _windowBounds = Window.Current.Bounds;
            Window.Current.SizeChanged += OnWindowSizeChanged;
            
        }
        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            String t1 = "About", t2 = "Features", t3 = "Credits", t4 = "ISBN/EAN", t5 = "privacy", t6 = "Help";
            SettingsPane.GetForCurrentView().CommandsRequested += (s, e) =>
            {
                SettingsCommand defaultsCommand = new SettingsCommand("about", t1,
                    (handler) =>
                    {
                        SettingsFlyout1 sf = new SettingsFlyout1();
                        sf.Show();
                    });
                e.Request.ApplicationCommands.Add(defaultsCommand);
            };
            SettingsPane.GetForCurrentView().CommandsRequested += (s, e) =>
            {
                SettingsCommand defaultsCommand = new SettingsCommand("features", t2,
                    (handler) =>
                    {
                        SettingsFlyout2 sf = new SettingsFlyout2();
                        sf.Show();
                    });
                e.Request.ApplicationCommands.Add(defaultsCommand);
            };
            SettingsPane.GetForCurrentView().CommandsRequested += (s, e) =>
            {
                SettingsCommand defaultsCommand = new SettingsCommand("credits", t3,
                    (handler) =>
                    {
                        SettingsFlyout3 sf = new SettingsFlyout3();
                        sf.Show();
                    });
                e.Request.ApplicationCommands.Add(defaultsCommand);
            };
            SettingsPane.GetForCurrentView().CommandsRequested += (s, e) =>
            {
                SettingsCommand defaultsCommand = new SettingsCommand("isbn", t4,
                    (handler) =>
                    {
                        SettingsFlyout4 sf = new SettingsFlyout4();
                        sf.Show();
                    });
                e.Request.ApplicationCommands.Add(defaultsCommand);
            };
            SettingsPane.GetForCurrentView().CommandsRequested += (s, e) =>
            {
                SettingsCommand defaultsCommand = new SettingsCommand("privacy", t5,
                    (handler) =>
                    {
                        SettingsFlyout5 sf = new SettingsFlyout5();
                        sf.Show();
                    });
                e.Request.ApplicationCommands.Add(defaultsCommand);
            };
            SettingsPane.GetForCurrentView().CommandsRequested += (s, e) =>
            {
                SettingsCommand defaultsCommand = new SettingsCommand("help", t6, OpenHelp);
                e.Request.ApplicationCommands.Add(defaultsCommand);
            };
            base.OnWindowCreated(args);
        }
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
        
    }
}
