// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using DynamicWindowsForms.ControlHelpers;
using System;

namespace DynamicWindowsForms
{
    /// <summary>
    /// A helper class for controls
    /// </summary>
    internal class ControlTypeHelper
    {
        /// <summary>
        /// Determines if the control is of a Windows UI (WUX) type or not.
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns>bool</returns>
        internal bool IsWuxControl(ControlEnum controlType)
        {
            switch (controlType)
            {
                case ControlEnum.UWPTextbox:
                    return true;
                case ControlEnum.UWPCheckbox:
                    return true;
                case ControlEnum.UWPRadioGroup:
                    return true;
                case ControlEnum.UWPCombobox:
                    return true;
                case ControlEnum.UWPButton:
                    return true;
                case ControlEnum.UWPSlider:
                    return true;
                case ControlEnum.UWPNotes:
                    return true;
                case ControlEnum.UWPCalendar:
                    return true;
                case ControlEnum.UWPInk:
                    return false;
                case ControlEnum.UWPMap:
                    return false;
                case ControlEnum.UWPWebView:
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Nullable returns the ControlEnum for the string value of the control name.
        /// </summary>
        /// <param name="controlTypeName"></param>
        /// <param name="isUWP"></param>
        /// <returns></returns>
        internal ControlEnum? GetControlEnum(string controlTypeName, bool isUWP)
        {
            controlTypeName = controlTypeName.Replace(" ", "");
            if (isUWP)
            {
                switch (controlTypeName)
                {
                    case ("Textbox"):
                        return ControlEnum.UWPTextbox;
                    case ("Checkbox"):
                        return ControlEnum.UWPCheckbox;
                    case ("RadioGroup"):
                        return ControlEnum.UWPRadioGroup;
                    case ("Combobox"):
                        return ControlEnum.UWPCombobox;
                    case ("Button"):
                        return ControlEnum.UWPButton;
                    case ("Slider"):
                        return ControlEnum.UWPSlider;
                    case ("Notes"):
                        return ControlEnum.UWPNotes;
                    case ("Calendar"):
                        return ControlEnum.UWPCalendar;
                    case ("Ink"):
                        return ControlEnum.UWPInk;
                    case ("Map"):
                        return ControlEnum.UWPMap;
                    case ("WebView"):
                        return ControlEnum.UWPWebView;
                    case ("MediaPlayer"):
                        return ControlEnum.UWPMediaPlayer;
                    default:
                        break;
                }

            }

            //-- otherwise it's standard
            ControlEnum? con = null;
            object obj;
            Enum.TryParse(typeof(ControlEnum), controlTypeName, out obj);
            if (obj != null)
            {
                return (ControlEnum)obj;
            }
            return null;
        }

    }
}