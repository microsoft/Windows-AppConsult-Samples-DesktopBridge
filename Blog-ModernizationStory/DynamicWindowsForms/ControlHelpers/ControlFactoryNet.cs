// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System.Windows.Forms;

namespace DynamicWindowsForms.ControlHelpers
{
    /// <summary>
    /// The NET Framework concrete instance of the factory.
    /// </summary>
    internal class ControlFactoryNet : ControlFactory
    {
        /// <summary>
        /// The control instance factory returning a .NET Framework control instance.
        /// </summary>
        /// <param name="controlType">The type of control to return.</param>
        /// <returns>Instance of the control</returns>
        internal override Control GetControl(ControlEnum controlType)
        {
            var netControls = new ControlHelperStandardNetControls();
            switch (controlType)
            {
                case ControlEnum.Textbox:
                    return netControls.GetTextbox();
                case ControlEnum.Checkbox:
                    return netControls.GetCheckbox();
                case ControlEnum.RadioGroup:
                    return netControls.GetRadio();
                case ControlEnum.Combobox:
                    return netControls.GetCombobox();
                case ControlEnum.Button:
                    return netControls.GetButton();
                case ControlEnum.Notes:
                    return netControls.GetNotes();
                case ControlEnum.Calendar:
                    return netControls.GetCalendar();
                case ControlEnum.Browser:
                    return netControls.GetBrowser();
                default:
                    return null;
            }
        }
    }
}
