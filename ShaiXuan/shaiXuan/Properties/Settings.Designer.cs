﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5485
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace shaiXuan.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://vicosrv5:8000/sap/bc/srt/rfc/sap/z_emis_mara_01/800/z_emis_mara_01/z_emis_" +
            "mara_01")]
        public string shaiXuan_Z_EMIS_MARA_01_Z_EMIS_MARA_01 {
            get {
                return ((string)(this["shaiXuan_Z_EMIS_MARA_01_Z_EMIS_MARA_01"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://vicosrv5:8000/sap/bc/srt/rfc/sap/z_rfc_po_01/800/z_rfc_po_01/z_rfc_po_01")]
        public string shaiXuan_vicosrv5_Z_RFC_PO_01 {
            get {
                return ((string)(this["shaiXuan_vicosrv5_Z_RFC_PO_01"]));
            }
        }
    }
}