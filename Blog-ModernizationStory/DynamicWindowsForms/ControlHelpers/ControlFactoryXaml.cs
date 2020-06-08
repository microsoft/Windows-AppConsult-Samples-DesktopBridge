// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System.Windows.Forms;
using WUX = Windows.UI.Xaml;


namespace DynamicWindowsForms.ControlHelpers
{
    /// <summary>
    /// The XAML Islands concrete instance of the control factory.
    /// </summary>
    internal class ControlFactoryXaml : ControlFactory
    {
        /// <summary>
        /// The XAML Islands methods to return a type of control. 
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns>The type of control to return.</returns>
        internal override Control GetControl(ControlEnum controlType)
        {
            var xamlControls = new ControlHelperXamlIslandControls();
            var windowsXamlHost = new Microsoft.Toolkit.Forms.UI.XamlHost.WindowsXamlHost();

            switch (controlType)
            {
                case ControlEnum.UWPTextbox:
                    return xamlControls.WrapInXamlHost(windowsXamlHost, xamlControls.GetTextBox());
                case ControlEnum.UWPCheckbox:
                    return xamlControls.WrapInXamlHost(windowsXamlHost, xamlControls.GetCheckbox());
                case ControlEnum.UWPRadioGroup:
                    return xamlControls.WrapInXamlHost(windowsXamlHost, xamlControls.GetRadio());
                case ControlEnum.UWPCombobox:
                    return xamlControls.WrapInXamlHost(windowsXamlHost, xamlControls.GetCombobox());
                case ControlEnum.UWPButton:
                    return xamlControls.WrapInXamlHost(windowsXamlHost, xamlControls.GetButton());
                case ControlEnum.UWPSlider:
                    return xamlControls.WrapInXamlHost(windowsXamlHost, xamlControls.GetSlider());
                case ControlEnum.UWPNotes:
                    return xamlControls.WrapInXamlHost(windowsXamlHost, xamlControls.GetNotes());
                case ControlEnum.UWPCalendar:
                    return xamlControls.WrapInXamlHost(windowsXamlHost, xamlControls.GetCalendar());
                case ControlEnum.UWPInk:
                    return xamlControls.GetInk();
                case ControlEnum.UWPMap:
                    return xamlControls.GetMap();
                case ControlEnum.UWPWebView:
                    return xamlControls.GetWebView();
                case ControlEnum.UWPMediaPlayer:
                    return xamlControls.GetMedia();
                default:
                    return null;
            }

        }

        /// <summary>
        /// This method returns the Windows.UI control instance. It must be separate as in 
        /// some cases we need to append to an existing XAML Islands instance rather than creating 
        /// a new XAML host.
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns></returns>
        internal WUX.Controls.Control GetWUXControl(ControlEnum controlType)
        {
            var xamlControls = new ControlHelperXamlIslandControls();
            switch (controlType)
            {
                case ControlEnum.UWPTextbox:
                    return xamlControls.GetTextBox();
                case ControlEnum.UWPCheckbox:
                    return xamlControls.GetCheckbox();
                case ControlEnum.UWPRadioGroup:
                    return xamlControls.GetRadio();
                case ControlEnum.UWPCombobox:
                    return xamlControls.GetCombobox();
                case ControlEnum.UWPButton:
                    return xamlControls.GetButton();
                case ControlEnum.UWPSlider:
                    return xamlControls.GetSlider();
                case ControlEnum.UWPNotes:
                    return xamlControls.GetNotes();
                case ControlEnum.UWPCalendar:
                    return xamlControls.GetCalendar();
                default:
                    return null;
            }//switch
        }

    }
}
