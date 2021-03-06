﻿using System;
using ScoreoidUI;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using Callisto.Controls;

namespace Scoreoid.Sample
{
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;

            ScoreoidManager.Initialize("e268410b95127e1a6d9ac447f8e2a29176ae3fb9", "lGKkihyFl");
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
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
                if (!rootFrame.Navigate(typeof (MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();

            SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SettingsPane.GetForCurrentView().CommandsRequested -= App_CommandsRequested;

            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        
        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            var cmd = new SettingsCommand("Scoreoid", "Scoreoid", (x) =>
            {
                var settings = new SettingsFlyout();
                settings.FlyoutWidth = Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth.Narrow;

                settings.Background = new SolidColorBrush(Colors.White);
                settings.HeaderBrush = new SolidColorBrush(Colors.Blue);
                settings.HeaderText = "Scoreoid";

                BitmapImage bmp = new BitmapImage(new Uri("ms-appx:///Assets/SmallLogo.png"));
                settings.SmallLogoImageSource = bmp;

                settings.Content = new ScoreoidUI.ScoreoidPane();
                settings.IsOpen = true;
            });

            args.Request.ApplicationCommands.Add(cmd);
        }
    }
}