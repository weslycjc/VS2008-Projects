﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5485
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 2.0.50727.5485 版自动生成。
// 
#pragma warning disable 1591

namespace shaiXuan.ZEMISMARA_01 {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    // CODEGEN: 未处理命名空间“http://schemas.xmlsoap.org/ws/2004/09/policy”中的可选 WSDL 扩展元素“Policy”。
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Z_EMIS_MARA_01", Namespace="urn:sap-com:document:sap:soap:functions:mc-style")]
    public partial class Z_EMIS_MARA_01 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ZEmisMara01OperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Z_EMIS_MARA_01() {
            this.Url = global::shaiXuan.Properties.Settings.Default.shaiXuan_Z_EMIS_MARA_01_Z_EMIS_MARA_01;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ZEmisMara01CompletedEventHandler ZEmisMara01Completed;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="urn:sap-com:document:sap:soap:functions:mc-style", ResponseNamespace="urn:sap-com:document:sap:soap:functions:mc-style", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ZEmisMara01([System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref Zemismara[] Itab, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Lgordesc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Maktdesc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Maradesc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Matkdesc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Normdesc, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string Posidesc) {
            object[] results = this.Invoke("ZEmisMara01", new object[] {
                        Itab,
                        Lgordesc,
                        Maktdesc,
                        Maradesc,
                        Matkdesc,
                        Normdesc,
                        Posidesc});
            Itab = ((Zemismara[])(results[0]));
        }
        
        /// <remarks/>
        public void ZEmisMara01Async(Zemismara[] Itab, string Lgordesc, string Maktdesc, string Maradesc, string Matkdesc, string Normdesc, string Posidesc) {
            this.ZEmisMara01Async(Itab, Lgordesc, Maktdesc, Maradesc, Matkdesc, Normdesc, Posidesc, null);
        }
        
        /// <remarks/>
        public void ZEmisMara01Async(Zemismara[] Itab, string Lgordesc, string Maktdesc, string Maradesc, string Matkdesc, string Normdesc, string Posidesc, object userState) {
            if ((this.ZEmisMara01OperationCompleted == null)) {
                this.ZEmisMara01OperationCompleted = new System.Threading.SendOrPostCallback(this.OnZEmisMara01OperationCompleted);
            }
            this.InvokeAsync("ZEmisMara01", new object[] {
                        Itab,
                        Lgordesc,
                        Maktdesc,
                        Maradesc,
                        Matkdesc,
                        Normdesc,
                        Posidesc}, this.ZEmisMara01OperationCompleted, userState);
        }
        
        private void OnZEmisMara01OperationCompleted(object arg) {
            if ((this.ZEmisMara01Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ZEmisMara01Completed(this, new ZEmisMara01CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5485")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style")]
    public partial class Zemismara {
        
        private string matnrField;
        
        private string maktxField;
        
        private string matklField;
        
        private string werksField;
        
        private string lgortField;
        
        private string sobkzField;
        
        private string lifnrField;
        
        private decimal labstField;
        
        private string meinsField;
        
        private string normtField;
        
        private string zeinrField;
        
        private string aetxtField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Matnr {
            get {
                return this.matnrField;
            }
            set {
                this.matnrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Maktx {
            get {
                return this.maktxField;
            }
            set {
                this.maktxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Matkl {
            get {
                return this.matklField;
            }
            set {
                this.matklField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Werks {
            get {
                return this.werksField;
            }
            set {
                this.werksField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Lgort {
            get {
                return this.lgortField;
            }
            set {
                this.lgortField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Sobkz {
            get {
                return this.sobkzField;
            }
            set {
                this.sobkzField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Lifnr {
            get {
                return this.lifnrField;
            }
            set {
                this.lifnrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Labst {
            get {
                return this.labstField;
            }
            set {
                this.labstField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Meins {
            get {
                return this.meinsField;
            }
            set {
                this.meinsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Normt {
            get {
                return this.normtField;
            }
            set {
                this.normtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Zeinr {
            get {
                return this.zeinrField;
            }
            set {
                this.zeinrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Aetxt {
            get {
                return this.aetxtField;
            }
            set {
                this.aetxtField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    public delegate void ZEmisMara01CompletedEventHandler(object sender, ZEmisMara01CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5483")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ZEmisMara01CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ZEmisMara01CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Zemismara[] Itab {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Zemismara[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591