// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System;
using System.Windows.Forms;

namespace DynamicWindowsForms.ControlHelpers
{
    /// <summary>
    /// The NET Framework helper class
    /// </summary>
    internal class ControlHelperStandardNetControls
    {
        internal Control GetCombobox()
        {
            var comboBox = new ComboBox();
            comboBox.Items.Add("Apple");
            comboBox.Items.Add("Bananna");
            comboBox.Items.Add("Cantelope");
            return comboBox;
        }

        /// <summary>
        /// Return a notes
        /// </summary>
        /// <returns>TextBox</returns>
        internal Control GetNotes()
        {
            var textBox = new TextBox();
            textBox.Width = 500;
            textBox.Multiline = true;
            textBox.Height = 300;
            textBox.AcceptsReturn = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            return textBox;
        }

        /// <summary>
        /// Return a checkbox
        /// </summary>
        /// <returns>CheckBox</returns>
        internal Control GetCheckbox()
        {
            var checkBox = new CheckBox();
            checkBox.Text = "My .NET Checkbox";
            checkBox.Margin = new Padding(0, 10, 0, 0);
            checkBox.Height = 30;
            checkBox.Width = 300;
            return checkBox;
        }

        /// <summary>
        /// Return a browser
        /// </summary>
        /// <returns>WebBrowser</returns>
        internal Control GetBrowser()
        {
            var browser = new WebBrowser();
            browser.Url = new Uri("http://www.bing.com");
            browser.Width = 1000;
            browser.Height = 400;
            return browser;
        }

        /// <summary>
        /// Return a calendar
        /// </summary>
        /// <returns>MonthCalendar</returns>
        internal Control GetCalendar()
        {
            var monthCalendar = new MonthCalendar();
            monthCalendar.TodayDate = DateTime.Now;
            return monthCalendar;
        }

        /// <summary>
        /// A new textbox
        /// </summary>
        /// <returns>TextBox</returns>
        internal Control GetTextbox()
        {
            var textBox = new TextBox();
            textBox.Width = 500;
            textBox.Margin = new Padding(0, 10, 0, 0);
            return textBox;
        }

        /// <summary>
        /// A radio button
        /// </summary>
        /// <returns>RadioButton</returns>
        internal Control GetRadio()
        {
            var radioButton = new RadioButton();
            radioButton.Text = "my .NET Radio Button";
            radioButton.Margin = new Padding(0, 10, 0, 0);
            radioButton.Height = 30;
            radioButton.Width = 300;
            return radioButton;
        }

        /// <summary>
        /// Get a new button
        /// </summary>
        /// <returns>Button</returns>
        internal Control GetButton()
        {
            var button = new Button();
            button.Text = ".NET Button";
            button.Margin = new Padding(0, 10, 0, 0);
            button.Height = 40;
            button.Width = 200;
            return button;
        }
    }
}
