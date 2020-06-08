// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using Microsoft.Toolkit.Forms.UI.XamlHost;
using System;
using System.Windows.Forms;
using WUX = Windows.UI.Xaml;
using Microsoft.Toolkit.Forms.UI.Controls;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using System.Drawing;

namespace DynamicWindowsForms.ControlHelpers
{
    /// <summary>
    /// Helper class for XAML Islands controls
    /// </summary>
    internal class ControlHelperXamlIslandControls
    {
        /// <summary>
        /// A new Windows.UI.Xaml.TextBox
        /// </summary>
        /// <returns>Windows.UI.Xaml.TextBox</returns>
        internal WUX.Controls.Control GetTextBox()
        {
            var textBox = new WUX.Controls.TextBox()
            {
                Width = 300,
                Margin = new WUX.Thickness(0, 10, 0, 0),
                HorizontalAlignment = WUX.HorizontalAlignment.Left,
            };
            return textBox;
        }

        /// <summary>
        /// Get a new WebViewCompatible. This works even pre-chromium.
        /// </summary>
        /// <returns>WebViewCompatible</returns>
        internal Control GetWebView()
        {
            var webViewCompatible = new Microsoft.Toolkit.Forms.UI.Controls.WebViewCompatible()
            {
                Width = 1000,
                Height = 400,
                Source = new Uri("http://www.bing.com")
            };
            return webViewCompatible;

        }

        /// <summary>
        /// Get an example MediaPlayerElement
        /// </summary>
        /// <returns>MediaPlayerElement</returns>
        internal Control GetMedia()
        {
            var media = new MediaPlayerElement()
            {
                AreTransportControlsEnabled = true,
                Source = "https://mediaplatstorage1.blob.core.windows.net/windows-universal-samples-media/e" +
    "lephantsdream-clip-h264_sd-aac_eng-aac_spa-aac_eng_commentary-srt_eng-srt_por-sr" +
    "t_swe.mkv",
                Width = 800,
                Height = 400
            };
            return media;
        }

        /// <summary>
        /// A demo map control
        /// </summary>
        /// <returns>MapControl</returns>
        internal Control GetMap()
        {
            var map = new MapControl()
            {
                Width = 800,
                Height = 400
            };
            return map;
        }

        /// <summary>
        /// This gets an example for inking
        /// </summary>
        /// <returns></returns>
        internal Control GetInk()
        {
            var panel = new Panel();
            panel.BackColor = Color.LightBlue;
            InkCanvas inkCanvas = new InkCanvas();
            inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Touch;
            inkCanvas.Width = 800;
            inkCanvas.Height = 300;
            inkCanvas.Top = 2;
            inkCanvas.Left = 2;
            inkCanvas.Margin = new Padding(4);

            InkToolbar toolbar = new InkToolbar();
            toolbar.ButtonFlyoutPlacement = Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.InkToolbarButtonFlyoutPlacement.Top;
            toolbar.Dock = System.Windows.Forms.DockStyle.Top;
            toolbar.Location = new System.Drawing.Point(2, 2);
            toolbar.MinimumSize = new System.Drawing.Size(13, 48);
            toolbar.TabIndex = 2;
            toolbar.TargetInkCanvas = inkCanvas;

            panel.Controls.Add(toolbar);
            panel.Controls.Add(inkCanvas);
            panel.Margin = new Padding(0, 10, 0, 0);
            panel.Size = new System.Drawing.Size(inkCanvas.Width + 4, inkCanvas.Height + 4);

            return panel;
        }

        /// <summary>
        /// A calendar example
        /// </summary>
        /// <returns>CalendarDatePicker</returns>
        internal WUX.Controls.Control GetCalendar()
        {

            var calendar = new WUX.Controls.CalendarDatePicker
            {
                Margin = new WUX.Thickness(0, 10, 0, 0),
                HorizontalAlignment = WUX.HorizontalAlignment.Left,
            };

            return calendar;
        }

        /// <summary>
        /// The RichEditBox example
        /// </summary>
        /// <returns>RichEditBox</returns>
        internal WUX.Controls.Control GetNotes()
        {
            var richtext = new WUX.Controls.RichEditBox
            {
                Height = 100,
                Width = 400,
                HorizontalAlignment = WUX.HorizontalAlignment.Stretch,
                Margin = new WUX.Thickness(0, 10, 0, 0)
            };

            return richtext;
        }

        /// <summary>
        /// A cool slider control
        /// </summary>
        /// <returns>Slider</returns>
        internal WUX.Controls.Control GetSlider()
        {
            var slider = new WUX.Controls.Slider
            {
                HorizontalAlignment = WUX.HorizontalAlignment.Center,
                Margin = new WUX.Thickness(0, 10, 0, 0)
            };

            return slider;
        }

        /// <summary>
        /// A Windows UI Button
        /// </summary>
        /// <returns>Button</returns>
        internal WUX.Controls.Control GetButton()
        {
            var button = new WUX.Controls.Button
            {
                HorizontalAlignment = WUX.HorizontalAlignment.Center,
                Margin = new WUX.Thickness(0, 10, 0, 0),
                Content = "UWP Button",
            };

            return button;
        }

        /// <summary>
        /// A Windows UI ComboBox
        /// </summary>
        /// <returns>ComboBox</returns>
        internal WUX.Controls.Control GetCombobox()
        {
            var comboBox = new WUX.Controls.ComboBox()
            {
                HorizontalAlignment = WUX.HorizontalAlignment.Left,
                Margin = new WUX.Thickness(0, 10, 0, 0)
            };

            comboBox.Items.Add("Apple");
            comboBox.Items.Add("Bannana");
            comboBox.Items.Add("Cedar");
            return comboBox;
        }

        /// <summary>
        /// A radio button example
        /// </summary>
        /// <returns>Windows.UI.Xaml.RadioButton</returns>
        internal WUX.Controls.Control GetRadio()
        {
            var radioButton = new WUX.Controls.RadioButton()
            {
                Content = new WUX.Controls.TextBlock() { Text = "UWP Radio" },
                HorizontalAlignment = WUX.HorizontalAlignment.Left,
            };

            return radioButton;
        }

        /// <summary>
        /// A checkbox instance
        /// </summary>
        /// <returns>Windows.UI.Xaml.CheckBox</returns>
        internal WUX.Controls.Control GetCheckbox()
        {
            var checkBox = new WUX.Controls.CheckBox()
            {
                Content = new WUX.Controls.TextBlock() { Text = "UWP Checkbox" },
                HorizontalAlignment = WUX.HorizontalAlignment.Left,
            };

            return checkBox;
        }

        /// <summary>
        /// This method is used to wrap the Windows.UI.Xaml control in a new instance of a WindowsXamlHost.
        /// </summary>
        /// <param name="windowsXamlHost"></param>
        /// <param name="control"></param>
        /// <returns>Control</returns>
        internal Control WrapInXamlHost(WindowsXamlHost windowsXamlHost, WUX.Controls.Control control)
        {
            windowsXamlHost.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            windowsXamlHost.InitialTypeName = null;
            windowsXamlHost.Padding = new Padding(5);

            WUX.Controls.StackPanel stackPanel = new WUX.Controls.StackPanel()
            {
                HorizontalAlignment = WUX.HorizontalAlignment.Stretch
            };


            stackPanel.Children.Add(control);
            windowsXamlHost.Child = stackPanel;
            if (windowsXamlHost.Width == 0)
            {
                windowsXamlHost.Height = ((int)control.Height > 0) ? (int)control.Height + 100 : 80;
                windowsXamlHost.Width = ((int)control.Width > 0) ? (int)control.Width + 220 : 500;
            }
            return windowsXamlHost;
        }

    }

}