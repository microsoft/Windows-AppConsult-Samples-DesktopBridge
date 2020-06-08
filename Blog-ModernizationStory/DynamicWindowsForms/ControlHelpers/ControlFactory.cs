// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System.Windows.Forms;

namespace DynamicWindowsForms.ControlHelpers
{
    /// <summary>
    /// Abstract factory pattern to create the control instance.
    /// </summary>
    internal abstract class ControlFactory
    {
        internal abstract Control GetControl(ControlEnum controlType);
    }
}
