
#pragma warning disable 1591

namespace FoodClient.localhost {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="DataSyncServiceHttpBinding", Namespace="http://face.webservice.fsweb.excellence.com")]
    public partial class DataSyncService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetPartDataDriverOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDataDriverOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckConnectionOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDataDriverBySignOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetDataDriverOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public DataSyncService() {
            this.Url = "http://192.168.100.108:8080/cfsweb/services/DataSyncService";
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
        public event GetPartDataDriverCompletedEventHandler GetPartDataDriverCompleted;
        
        /// <remarks/>
        public event GetDataDriverCompletedEventHandler GetDataDriverCompleted;
        
        /// <remarks/>
        public event CheckConnectionCompletedEventHandler CheckConnectionCompleted;
        
        /// <remarks/>
        public event GetDataDriverBySignCompletedEventHandler GetDataDriverBySignCompleted;
        
        /// <remarks/>
        public event SetDataDriverCompletedEventHandler SetDataDriverCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://face.webservice.fsweb.excellence.com", ResponseNamespace="http://face.webservice.fsweb.excellence.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("out", IsNullable=true)]
        public string GetPartDataDriver([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in0, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in1, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in2) {
            object[] results = this.Invoke("GetPartDataDriver", new object[] {
                        in0,
                        in1,
                        in2});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetPartDataDriver(string in0, string in1, string in2, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetPartDataDriver", new object[] {
                        in0,
                        in1,
                        in2}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetPartDataDriver(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetPartDataDriverAsync(string in0, string in1, string in2) {
            this.GetPartDataDriverAsync(in0, in1, in2, null);
        }
        
        /// <remarks/>
        public void GetPartDataDriverAsync(string in0, string in1, string in2, object userState) {
            if ((this.GetPartDataDriverOperationCompleted == null)) {
                this.GetPartDataDriverOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPartDataDriverOperationCompleted);
            }
            this.InvokeAsync("GetPartDataDriver", new object[] {
                        in0,
                        in1,
                        in2}, this.GetPartDataDriverOperationCompleted, userState);
        }
        
        private void OnGetPartDataDriverOperationCompleted(object arg) {
            if ((this.GetPartDataDriverCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPartDataDriverCompleted(this, new GetPartDataDriverCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://face.webservice.fsweb.excellence.com", ResponseNamespace="http://face.webservice.fsweb.excellence.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("out", IsNullable=true)]
        public string GetDataDriver([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in0, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in1, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in2, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in3, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in4) {
            object[] results = this.Invoke("GetDataDriver", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDataDriver(string in0, string in1, string in2, string in3, string in4, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDataDriver", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetDataDriver(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetDataDriverAsync(string in0, string in1, string in2, string in3, string in4) {
            this.GetDataDriverAsync(in0, in1, in2, in3, in4, null);
        }
        
        /// <remarks/>
        public void GetDataDriverAsync(string in0, string in1, string in2, string in3, string in4, object userState) {
            if ((this.GetDataDriverOperationCompleted == null)) {
                this.GetDataDriverOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDataDriverOperationCompleted);
            }
            this.InvokeAsync("GetDataDriver", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4}, this.GetDataDriverOperationCompleted, userState);
        }
        
        private void OnGetDataDriverOperationCompleted(object arg) {
            if ((this.GetDataDriverCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDataDriverCompleted(this, new GetDataDriverCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://face.webservice.fsweb.excellence.com", ResponseNamespace="http://face.webservice.fsweb.excellence.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("out", IsNullable=true)]
        public string CheckConnection([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in0, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in1) {
            object[] results = this.Invoke("CheckConnection", new object[] {
                        in0,
                        in1});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCheckConnection(string in0, string in1, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("CheckConnection", new object[] {
                        in0,
                        in1}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndCheckConnection(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CheckConnectionAsync(string in0, string in1) {
            this.CheckConnectionAsync(in0, in1, null);
        }
        
        /// <remarks/>
        public void CheckConnectionAsync(string in0, string in1, object userState) {
            if ((this.CheckConnectionOperationCompleted == null)) {
                this.CheckConnectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckConnectionOperationCompleted);
            }
            this.InvokeAsync("CheckConnection", new object[] {
                        in0,
                        in1}, this.CheckConnectionOperationCompleted, userState);
        }
        
        private void OnCheckConnectionOperationCompleted(object arg) {
            if ((this.CheckConnectionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckConnectionCompleted(this, new CheckConnectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://face.webservice.fsweb.excellence.com", ResponseNamespace="http://face.webservice.fsweb.excellence.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("out", IsNullable=true)]
        public string GetDataDriverBySign([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in0, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in1, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in2, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in3, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in4, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in5) {
            object[] results = this.Invoke("GetDataDriverBySign", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4,
                        in5});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDataDriverBySign(string in0, string in1, string in2, string in3, string in4, string in5, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDataDriverBySign", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4,
                        in5}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetDataDriverBySign(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetDataDriverBySignAsync(string in0, string in1, string in2, string in3, string in4, string in5) {
            this.GetDataDriverBySignAsync(in0, in1, in2, in3, in4, in5, null);
        }
        
        /// <remarks/>
        public void GetDataDriverBySignAsync(string in0, string in1, string in2, string in3, string in4, string in5, object userState) {
            if ((this.GetDataDriverBySignOperationCompleted == null)) {
                this.GetDataDriverBySignOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDataDriverBySignOperationCompleted);
            }
            this.InvokeAsync("GetDataDriverBySign", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4,
                        in5}, this.GetDataDriverBySignOperationCompleted, userState);
        }
        
        private void OnGetDataDriverBySignOperationCompleted(object arg) {
            if ((this.GetDataDriverBySignCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDataDriverBySignCompleted(this, new GetDataDriverBySignCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://face.webservice.fsweb.excellence.com", ResponseNamespace="http://face.webservice.fsweb.excellence.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("out", IsNullable=true)]
        public string SetDataDriver([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in0, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in1, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in2, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string in3, int in4) {
            object[] results = this.Invoke("SetDataDriver", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSetDataDriver(string in0, string in1, string in2, string in3, int in4, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SetDataDriver", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSetDataDriver(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SetDataDriverAsync(string in0, string in1, string in2, string in3, int in4) {
            this.SetDataDriverAsync(in0, in1, in2, in3, in4, null);
        }
        
        /// <remarks/>
        public void SetDataDriverAsync(string in0, string in1, string in2, string in3, int in4, object userState) {
            if ((this.SetDataDriverOperationCompleted == null)) {
                this.SetDataDriverOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetDataDriverOperationCompleted);
            }
            this.InvokeAsync("SetDataDriver", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4}, this.SetDataDriverOperationCompleted, userState);
        }
        
        private void OnSetDataDriverOperationCompleted(object arg) {
            if ((this.SetDataDriverCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetDataDriverCompleted(this, new SetDataDriverCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetPartDataDriverCompletedEventHandler(object sender, GetPartDataDriverCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPartDataDriverCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPartDataDriverCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetDataDriverCompletedEventHandler(object sender, GetDataDriverCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDataDriverCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDataDriverCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void CheckConnectionCompletedEventHandler(object sender, CheckConnectionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckConnectionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckConnectionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetDataDriverBySignCompletedEventHandler(object sender, GetDataDriverBySignCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDataDriverBySignCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDataDriverBySignCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void SetDataDriverCompletedEventHandler(object sender, SetDataDriverCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SetDataDriverCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SetDataDriverCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591