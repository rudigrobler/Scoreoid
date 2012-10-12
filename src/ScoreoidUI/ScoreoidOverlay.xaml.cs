﻿using System;
using System.Collections.Generic;
using System.Text;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ScoreoidUI
{
    public sealed partial class ScoreoidOverlay : UserControl
    {
        public ScoreoidOverlay()
        {
            InitializeComponent();

            Loaded += ScoreoidOverlay_Loaded;
        }

        public void Refresh()
        {
            username.Text = string.Empty;
            password.Password = string.Empty;

            if (string.IsNullOrEmpty(ScoreoidManager.username))
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }

        private void ScoreoidOverlay_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Refresh();
            }
            catch(Exception ex)
            {
                MessageDialog dlg = new MessageDialog(ex.Message);
                dlg.ShowAsync();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(username.Text))
            {
                ScoreoidManager.username = username.Text;
                if (!string.IsNullOrEmpty(password.Password))
                {
                    ScoreoidManager.password = password.Password;
                }
                Visibility = Visibility.Collapsed;
            }
            else
            {
                var dlg = new MessageDialog("Please enter a valid scoreoid username and/or password") {Title = "ERROR"};
                dlg.Title = "ERROR";
                dlg.ShowAsync();
            }
        }

        private void password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                SaveButton_Click(sender, new RoutedEventArgs());
            }
        }

        private void createPlayerHyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (Window.Current != null)
            {
                var frame = Window.Current.Content as Frame;
                if (frame != null)
                {
                    frame.Navigate(typeof(ScoreoidCreatePlayerPage));
                }
            }
        }
    }
}