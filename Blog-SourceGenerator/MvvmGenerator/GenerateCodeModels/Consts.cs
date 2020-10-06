using System;
using System.Collections.Generic;
using System.Text;

namespace MvvmGenerator.GenerateCodeModels
{
    static class Consts
    {
        public const string Namespace = "MvvmGenerator";
        public const string AutoNotifyName = "AutoNotify";
        public const string AutoNotifyAttributeName = "AutoNotifyAttribute";
        public static readonly string AutoNotifyAttributeFullName = $"{Namespace}.{AutoNotifyAttributeName}";
    }
}
