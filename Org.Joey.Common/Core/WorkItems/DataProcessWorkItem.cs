


namespace Org.Joey.Common
{
    using System;
    using System.Threading;
    using Microsoft.Extensions.Logging;
    public abstract class DataProcessWorkItem<T> : IWorkItem
        where T : IWorkItemState
    {
        protected CancellationTokenSource Cancellation;
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DataProcessWorkItem<T>));
        public DataProcessWorkItem(T state)
        {

            this.WorkItemState = state;
            this.Cancellation = new CancellationTokenSource();
        }
        public T WorkItemState { get; protected set; }
        object IWorkItem.WorkItemState { get { return this.WorkItemState; } }
        public void Abort()
        {
            this.Cancellation.Cancel();
            this.OnAbort(new JobTerminatedException());
        }
        public void Execute()
        {
            this.OnStart();
            try
            {
                this.Run(this.Cancellation.Token);
                this.OnComplete();
            }
            catch (Exception ex)
            {
                this.OnAbort(ex);
            }
        }
        protected abstract void Run(CancellationToken token);
        protected virtual void OnStart()
        {
            ////TODO: log work item started.
            Logger.InfoFormat("Work item start.({0})", this.WorkItemState.Name);
        }
        protected virtual void OnComplete()
        {
            ////TODO: log work item completed.
            Logger.InfoFormat("Work Item Completed.({0})",this.WorkItemState.Name);
        }
        protected virtual void OnAbort(Exception exception)
        {
            ////TODO: log work item aborted.
            Logger.Error(exception.Message, exception);
        }
    }
}
