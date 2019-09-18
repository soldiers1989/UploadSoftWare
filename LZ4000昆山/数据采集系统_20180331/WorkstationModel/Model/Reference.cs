﻿
#pragma warning disable 1591

namespace FoodClient.ForNet
{
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Data;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "DataDriverSoap", Namespace = "http://localhost/FSWeb/DataDriver/")]
    public partial class DataDriver : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback GetDataDriverOperationCompleted;

        private System.Threading.SendOrPostCallback SetDataDriverOperationCompleted;

        private System.Threading.SendOrPostCallback GetPartDataDriverOperationCompleted;

        private System.Threading.SendOrPostCallback CheckConnectionOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public DataDriver()
        {
            this.Url = "http://localhost/DataDriver/DataDriver.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event GetDataDriverCompletedEventHandler GetDataDriverCompleted;

        /// <remarks/>
        public event SetDataDriverCompletedEventHandler SetDataDriverCompleted;

        /// <remarks/>
        public event GetPartDataDriverCompletedEventHandler GetPartDataDriverCompleted;

        /// <remarks/>
        public event CheckConnectionCompletedEventHandler CheckConnectionCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/FSWeb/DataDriver/GetDataDriver", RequestNamespace = "http://localhost/FSWeb/DataDriver/", ResponseNamespace = "http://localhost/FSWeb/DataDriver/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetDataDriver(string ver, string sDirstrictCode, string sStdCode, string username, string pwd, out string sErr)
        {
            object[] results = this.Invoke("GetDataDriver", new object[] {
                        ver,
                        sDirstrictCode,
                        sStdCode,
                        username,
                        pwd});
            sErr = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetDataDriver(string ver, string sDirstrictCode, string sStdCode, string username, string pwd, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetDataDriver", new object[] {
                        ver,
                        sDirstrictCode,
                        sStdCode,
                        username,
                        pwd}, callback, asyncState);
        }

        /// <remarks/>
        public System.Data.DataSet EndGetDataDriver(System.IAsyncResult asyncResult, out string sErr)
        {
            object[] results = this.EndInvoke(asyncResult);
            sErr = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }

        /// <remarks/>
        public void GetDataDriverAsync(string ver, string sDirstrictCode, string sStdCode, string username, string pwd)
        {
            this.GetDataDriverAsync(ver, sDirstrictCode, sStdCode, username, pwd, null);
        }

        /// <remarks/>
        public void GetDataDriverAsync(string ver, string sDirstrictCode, string sStdCode, string username, string pwd, object userState)
        {
            if ((this.GetDataDriverOperationCompleted == null))
            {
                this.GetDataDriverOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDataDriverOperationCompleted);
            }
            this.InvokeAsync("GetDataDriver", new object[] {
                        ver,
                        sDirstrictCode,
                        sStdCode,
                        username,
                        pwd}, this.GetDataDriverOperationCompleted, userState);
        }

        private void OnGetDataDriverOperationCompleted(object arg)
        {
            if ((this.GetDataDriverCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDataDriverCompleted(this, new GetDataDriverCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/FSWeb/DataDriver/SetDataDriver", RequestNamespace = "http://localhost/FSWeb/DataDriver/", ResponseNamespace = "http://localhost/FSWeb/DataDriver/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SetDataDriver(System.Data.DataSet dtTable, string username, string pwd, string companyname, int sType)
        {
            object[] results = this.Invoke("SetDataDriver", new object[] {
                        dtTable,
                        username,
                        pwd,
                        companyname,
                        sType});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginSetDataDriver(System.Data.DataSet dtTable, string username, string pwd, string companyname, int sType, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("SetDataDriver", new object[] {
                        dtTable,
                        username,
                        pwd,
                        companyname,
                        sType}, callback, asyncState);
        }

        /// <remarks/>
        public int EndSetDataDriver(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void SetDataDriverAsync(System.Data.DataSet dtTable, string username, string pwd, string companyname, int sType)
        {
            this.SetDataDriverAsync(dtTable, username, pwd, companyname, sType, null);
        }

        /// <remarks/>
        public void SetDataDriverAsync(System.Data.DataSet dtTable, string username, string pwd, string companyname, int sType, object userState)
        {
            if ((this.SetDataDriverOperationCompleted == null))
            {
                this.SetDataDriverOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetDataDriverOperationCompleted);
            }
            this.InvokeAsync("SetDataDriver", new object[] {
                        dtTable,
                        username,
                        pwd,
                        companyname,
                        sType}, this.SetDataDriverOperationCompleted, userState);
        }

        private void OnSetDataDriverOperationCompleted(object arg)
        {
            if ((this.SetDataDriverCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetDataDriverCompleted(this, new SetDataDriverCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/FSWeb/DataDriver/GetPartDataDriver", RequestNamespace = "http://localhost/FSWeb/DataDriver/", ResponseNamespace = "http://localhost/FSWeb/DataDriver/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetPartDataDriver(string ver, string username, string pwd, out string sErr)
        {
            object[] results = this.Invoke("GetPartDataDriver", new object[] {
                        ver,
                        username,
                        pwd});
            sErr = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetPartDataDriver(string ver, string username, string pwd, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetPartDataDriver", new object[] {
                        ver,
                        username,
                        pwd}, callback, asyncState);
        }

        /// <remarks/>
        public System.Data.DataSet EndGetPartDataDriver(System.IAsyncResult asyncResult, out string sErr)
        {
            object[] results = this.EndInvoke(asyncResult);
            sErr = ((string)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }

        /// <remarks/>
        public void GetPartDataDriverAsync(string ver, string username, string pwd)
        {
            this.GetPartDataDriverAsync(ver, username, pwd, null);
        }

        /// <remarks/>
        public void GetPartDataDriverAsync(string ver, string username, string pwd, object userState)
        {
            if ((this.GetPartDataDriverOperationCompleted == null))
            {
                this.GetPartDataDriverOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPartDataDriverOperationCompleted);
            }
            this.InvokeAsync("GetPartDataDriver", new object[] {
                        ver,
                        username,
                        pwd}, this.GetPartDataDriverOperationCompleted, userState);
        }

        private void OnGetPartDataDriverOperationCompleted(object arg)
        {
            if ((this.GetPartDataDriverCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPartDataDriverCompleted(this, new GetPartDataDriverCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/FSWeb/DataDriver/CheckConnection", RequestNamespace = "http://localhost/FSWeb/DataDriver/", ResponseNamespace = "http://localhost/FSWeb/DataDriver/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckConnection(string username, string pwd, out string sErr)
        {
            object[] results = this.Invoke("CheckConnection", new object[] {
                        username,
                        pwd});
            sErr = ((string)(results[1]));
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCheckConnection(string username, string pwd, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CheckConnection", new object[] {
                        username,
                        pwd}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndCheckConnection(System.IAsyncResult asyncResult, out string sErr)
        {
            object[] results = this.EndInvoke(asyncResult);
            sErr = ((string)(results[1]));
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void CheckConnectionAsync(string username, string pwd)
        {
            this.CheckConnectionAsync(username, pwd, null);
        }

        /// <remarks/>
        public void CheckConnectionAsync(string username, string pwd, object userState)
        {
            if ((this.CheckConnectionOperationCompleted == null))
            {
                this.CheckConnectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckConnectionOperationCompleted);
            }
            this.InvokeAsync("CheckConnection", new object[] {
                        username,
                        pwd}, this.CheckConnectionOperationCompleted, userState);
        }

        private void OnCheckConnectionOperationCompleted(object arg)
        {
            if ((this.CheckConnectionCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckConnectionCompleted(this, new CheckConnectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetDataDriverCompletedEventHandler(object sender, GetDataDriverCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDataDriverCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetDataDriverCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public System.Data.DataSet Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }

        /// <remarks/>
        public string sErr
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
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
    public partial class SetDataDriverCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SetDataDriverCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetPartDataDriverCompletedEventHandler(object sender, GetPartDataDriverCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPartDataDriverCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetPartDataDriverCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public System.Data.DataSet Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }

        /// <remarks/>
        public string sErr
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
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
    public partial class CheckConnectionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CheckConnectionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }

        /// <remarks/>
        public string sErr
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591