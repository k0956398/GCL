using System;

namespace EplusE
{
    /// <summary>
    /// Provides asynchronous helper methods.
    /// <locDE><para />Stellt asynchrone Hilfsmethoden bereit.</locDE>
    /// </summary>
    public class AsyncHelper
    {
        #region RunAsync

        /// <summary>
        /// Runs the worker action asynchronously.
        /// <locDE><para />Führt die Worker-Action asynchron (im Hintergrund) aus.</locDE>
        /// </summary>
        /// <param name="worker">The worker action.<locDE><para />Die Worker-Action.</locDE></param>
        /// <param name="callback">The optional callback action, called after completion of the worker action.
        /// Exception parameter is not null if error/exception ocurred.
        /// <locDE><para />Die optionale Callback-Action, wird nach dem Ende der Worker-Action aufgerufen.
        /// Parameter Exception ist ungleich Null falls ein Fehler/eine Exception aufgetreten ist.</locDE></param>
        /// <example>
        /// <code>
        /// EplusE.AsyncHelper.RunAsynch( () =&gt; { pick file from FTP server and parse it },
        /// (ex) =&gt; { Console.WriteLine("Parsing is done"); } );
        /// </code>
        /// </example>
        public static void RunAsync(Action worker, Action<Exception> callback = null)
        {
            // http://stackoverflow.com/questions/1159214/how-to-create-an-asynchronous-method

            System.Threading.ThreadPool.QueueUserWorkItem(_ =>
            {
                Exception exception = null;
                try
                {
                    worker();
                }
                //catch (System.Threading.ThreadAbortException taex)
                //{
                //	exception = taex;
                //}
                catch (Exception ex)
                {
                    exception = ex;
                }

                if (null != callback)
                    callback(exception);
            });
        }

        #endregion RunAsync

        #region Run an async Task<T> method synchronously

        //// http://stackoverflow.com/a/5097066
        //// https://social.msdn.microsoft.com/Forums/en-US/163ef755-ff7b-4ea5-b226-bbe8ef5f4796/is-there-a-pattern-for-calling-an-async-method-synchronously?forum=async

        ///// <summary>
        ///// Execute's an async Task<T> method which has a void return value synchronously.
        ///// I.e. customerList = AsyncHelper.RunSync<List<Customer>>(() => GetCustomers());
        ///// </summary>
        ///// <param name="task">Task<T> method to execute</param>
        //public static void RunSync(Func<Task> task)
        //{
        //    var oldContext = SynchronizationContext.Current;
        //    var synch = new ExclusiveSynchronizationContext();
        //    SynchronizationContext.SetSynchronizationContext(synch);
        //    synch.Post(async _ =>
        //    {
        //        try
        //        {
        //            await task();
        //        }
        //        catch (Exception e)
        //        {
        //            synch.InnerException = e;
        //            throw;
        //        }
        //        finally
        //        {
        //            synch.EndMessageLoop();
        //        }
        //    }, null);
        //    synch.BeginMessageLoop();

        //    SynchronizationContext.SetSynchronizationContext(oldContext);
        //}

        ///// <summary>
        ///// Execute's an async Task<T> method which has a T return type synchronously.
        ///// I.e. customerList = AsyncHelper.RunSync<List<Customer>>(() => GetCustomers());
        ///// </summary>
        ///// <typeparam name="T">Return Type</typeparam>
        ///// <param name="task">Task<T> method to execute</param>
        ///// <returns></returns>
        //public static T RunSync<T>(Func<Task<T>> task)
        //{
        //    var oldContext = SynchronizationContext.Current;
        //    var synch = new ExclusiveSynchronizationContext();
        //    SynchronizationContext.SetSynchronizationContext(synch);
        //    T ret = default(T);
        //    synch.Post(async _ =>
        //    {
        //        try
        //        {
        //            ret = await task();
        //        }
        //        catch (Exception e)
        //        {
        //            synch.InnerException = e;
        //            throw;
        //        }
        //        finally
        //        {
        //            synch.EndMessageLoop();
        //        }
        //    }, null);
        //    synch.BeginMessageLoop();
        //    SynchronizationContext.SetSynchronizationContext(oldContext);
        //    return ret;
        //}

        //private class ExclusiveSynchronizationContext : SynchronizationContext
        //{
        //    private bool done;
        //    public Exception InnerException { get; set; }
        //    readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);
        //    readonly Queue<Tuple<SendOrPostCallback, object>> items =
        //        new Queue<Tuple<SendOrPostCallback, object>>();

        //    public override void Send(SendOrPostCallback d, object state)
        //    {
        //        throw new NotSupportedException("We cannot send to our same thread");
        //    }

        //    public override void Post(SendOrPostCallback d, object state)
        //    {
        //        lock (items)
        //        {
        //            items.Enqueue(Tuple.Create(d, state));
        //        }
        //        workItemsWaiting.Set();
        //    }

        //    public void EndMessageLoop()
        //    {
        //        Post(_ => done = true, null);
        //    }

        //    public void BeginMessageLoop()
        //    {
        //        while (!done)
        //        {
        //            Tuple<SendOrPostCallback, object> task = null;
        //            lock (items)
        //            {
        //                if (items.Count > 0)
        //                {
        //                    task = items.Dequeue();
        //                }
        //            }
        //            if (task != null)
        //            {
        //                task.Item1(task.Item2);
        //                if (InnerException != null) // the method threw an exeption
        //                {
        //                    throw new AggregateException("AsyncHelpers.Run method threw an exception.", InnerException);
        //                }
        //            }
        //            else
        //            {
        //                workItemsWaiting.WaitOne();
        //            }
        //        }
        //    }

        //    public override SynchronizationContext CreateCopy()
        //    {
        //        return this;
        //    }
        //}

        #endregion Run an async Task<T> method synchronously

        #region Maybe useful?

        //private async System.Threading.Tasks.Task DoSomethingDelayed(int msec = 5000)
        //{
        //	await System.Threading.Tasks.Task.Delay(msec);
        //
        //	try
        //	{
        //		// do something
        //	}
        //	catch (Exception ex)
        //	{
        //	}
        //}

        #endregion Maybe useful?
    }
}