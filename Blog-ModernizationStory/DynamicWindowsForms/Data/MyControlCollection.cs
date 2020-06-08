// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWindowsForms.Data
{
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.microsoft.com", IsNullable = false)]
    internal class MyControlCollection
    {

        internal string FileName { get; set; }
        internal List<MyControl> Controls { get; set; }
    }
}
