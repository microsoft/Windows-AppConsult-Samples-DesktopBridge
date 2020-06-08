// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using DynamicWindowsForms.ControlHelpers;
using DynamicWindowsForms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
    using WUX = Windows.UI.Xaml;
using windows = Windows;
using Microsoft.Toolkit.Forms.UI.XamlHost;
using Microsoft.Toolkit.Forms.UI.Controls;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;


namespace DynamicWindowsForms
{
    internal partial class FormDynamic : Form
    {
        private MyControlCollection thePage;
        private bool isXamlIslandsSupported = false;


        internal FormDynamic()
        {
            InitializeComponent();
            toolStripMenuItemSave.Click += ToolStripMenuItemSave_Click;
            toolStripMenuItemOpen.Click += ToolStripMenuItemOpen_Click;
            thePage = new MyControlCollection();
            thePage.Controls = new List<MyControl>();
            isXamlIslandsSupported = GetIsSupported();
        }

        private bool GetIsSupported()
        {
            if (Environment.OSVersion.Version.Major < 10)
            {
                return false;
            }

            if (Environment.OSVersion.Version.Build < 18226)
            {
                return false;
            }

            return true;
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DeserializeObject(openFileDialog1.FileName);
            }//if
        }

        private void DeserializeObject(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MyControlCollection));

            using (Stream reader = new FileStream(fileName, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                thePage = (MyControlCollection)serializer.Deserialize(reader);
                Populate(thePage);
            }
        }

        private void Populate(MyControlCollection thePage)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var control in thePage.Controls)
            {
                AddControl(control.ControlTypeName,control.IsXamlControl);
            }
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "dxml files |*.dxml";

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            };

            // Create a new Serializer
            XmlSerializer serializer = new XmlSerializer(typeof(MyControlCollection));
            // Create a new StreamWriter
            using (TextWriter writer = new StreamWriter(dlg.FileName))
            {
                serializer.Serialize(writer, thePage);
            }
        }

        private void buttonAddStandard_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxStandard.Text))
            {
                return;
            }
            AddControl(comboBoxStandard.Text, false);
            thePage.Controls.Add(new MyControl() { ControlTypeName = comboBoxStandard.Text.Replace(" ", "") });
        }

        private void AddControl(string text, bool isXaml)
        {
            if (!isXamlIslandsSupported)
            {
                isXaml = false;
            }

            var controlTypeHelper = new ControlTypeHelper();
            ControlEnum? controlType = controlTypeHelper.GetControlEnum(text,isXaml);
            if (controlType == null)
            {
                return;
            }

            //-- factory pattern
            ControlFactory factory = (isXaml) ?
                (ControlFactory)(new ControlFactoryXaml()) :
                (ControlFactory)(new ControlFactoryNet());


            var control = factory.GetControl(controlType.Value);
            if (control == null)
            {
                return;
            }

            flowLayoutPanel1.Controls.Add(control);
            flowLayoutPanel1.ScrollControlIntoView(control);



        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls != null)
            {
                flowLayoutPanel1.Controls.Clear();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonAddUWP_Click(object sender, EventArgs e)
        {
            AddUWPControl(comboBoxUWP.Text);
            thePage.Controls.Add(new MyControl() { ControlTypeName = comboBoxUWP.Text, IsXamlControl = true });
        }

        private void AddUWPControl(string controlName)
        {
            var controlTypeHelper = new ControlTypeHelper();
            var controlType = controlTypeHelper.GetControlEnum(controlName, true);
            if (!controlType.HasValue)
            {
                return;
            }

            //-- add as a new control
            if (!IsXamlAppendAction() || !controlTypeHelper.IsWuxControl(controlType.Value))
            {
                AddControl(controlName, true);
                return;
            }

            AppendUWPControl(controlType);
        }

        private void AppendUWPControl(ControlEnum? controlType)
        {
            //-- now add to existing XamlHost instead
            var lastControl = (Microsoft.Toolkit.Forms.UI.XamlHost.WindowsXamlHost)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1];
            var stackPanel = (WUX.Controls.StackPanel)lastControl.Child;
            var factory = new ControlFactoryXaml();
            var wuxControl = factory.GetWUXControl(controlType.Value);

            stackPanel.Children.Add(wuxControl);
            lastControl.Height = lastControl.Height + (int)stackPanel.ActualHeight;

            //-- now scroll it
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ScrollControlIntoView(lastControl);
            flowLayoutPanel1.VerticalScroll.Value = flowLayoutPanel1.VerticalScroll.Maximum;
        }

        private bool IsXamlAppendAction()
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                return false;
            }

            var control = flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1];
            return (control is Microsoft.Toolkit.Forms.UI.XamlHost.WindowsXamlHost);
            //return;
        }//if

    }
}
