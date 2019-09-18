namespace DY.Process
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class PercentProcess
    {
        private BackgroundWorker _backgroundWorker;
        private BackgroundWorkerEventArgs _eventArgs = new BackgroundWorkerEventArgs();
        private string _inforMessage;
        private ProcessForm _processForm = new ProcessForm();
        private EventHandler<BackgroundWorkerEventArgs> BackgroundWorkerCompleted;

        public event EventHandler<BackgroundWorkerEventArgs> BackgroundWorkerCompleted
        {
            add
            {
                EventHandler<BackgroundWorkerEventArgs> handler2;
                EventHandler<BackgroundWorkerEventArgs> backgroundWorkerCompleted = this.BackgroundWorkerCompleted;
                do
                {
                    handler2 = backgroundWorkerCompleted;
                    EventHandler<BackgroundWorkerEventArgs> handler3 = (EventHandler<BackgroundWorkerEventArgs>) Delegate.Combine(handler2, value);
                    backgroundWorkerCompleted = Interlocked.CompareExchange<EventHandler<BackgroundWorkerEventArgs>>(ref this.BackgroundWorkerCompleted, handler3, handler2);
                }
                while (backgroundWorkerCompleted != handler2);
            }
            remove
            {
                EventHandler<BackgroundWorkerEventArgs> handler2;
                EventHandler<BackgroundWorkerEventArgs> backgroundWorkerCompleted = this.BackgroundWorkerCompleted;
                do
                {
                    handler2 = backgroundWorkerCompleted;
                    EventHandler<BackgroundWorkerEventArgs> handler3 = (EventHandler<BackgroundWorkerEventArgs>) Delegate.Remove(handler2, value);
                    backgroundWorkerCompleted = Interlocked.CompareExchange<EventHandler<BackgroundWorkerEventArgs>>(ref this.BackgroundWorkerCompleted, handler3, handler2);
                }
                while (backgroundWorkerCompleted != handler2);
            }
        }

        public PercentProcess()
        {
            this._processForm.ProcessStyle = ProgressBarStyle.Continuous;
            this._backgroundWorker = new BackgroundWorker();
            this._backgroundWorker.WorkerReportsProgress = true;
            this._backgroundWorker.DoWork += new DoWorkEventHandler(this._backgroundWorker_DoWork);
            this._backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
            this._backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this._backgroundWorker_ProgressChanged);
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.BackgroundWork != null)
            {
                try
                {
                    this.BackgroundWork(new Action<int>(this.ReportPercent));
                }
                catch (Exception ex)
                {
                    this._eventArgs.BackGroundException = ex;
                }
            }
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this._processForm.MessageInfo = this._inforMessage + ",已完成：" + e.ProgressPercentage.ToString() + "%";
            this._processForm.ProcessValue = e.ProgressPercentage;
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this._processForm.Visible)
            {
                this._processForm.Close();
            }
            if (this.BackgroundWorkerCompleted != null)
            {
                this.BackgroundWorkerCompleted(null, this._eventArgs);
            }
        }

        private void ReportPercent(int i)
        {
            if ((i >= 0) && (i <= 100))
            {
                this._backgroundWorker.ReportProgress(i);
            }
        }

        public void Start()
        {
            this._backgroundWorker.RunWorkerAsync();
            this._processForm.ShowDialog();
        }

        public Action<Action<int>> BackgroundWork { get; set; }

        public string MessageInfo
        {
            set
            {
                this._inforMessage = value;
            }
        }
    }
}

