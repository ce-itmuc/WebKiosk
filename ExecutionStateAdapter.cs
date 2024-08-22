namespace WebKiosk
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    internal partial class ExecutionStateAdapter
    {
        #region Kernel32 Reference

        [Flags]
        private enum ExecutionState : uint
        {
            EsContinuous = 0x80000000,
            EsDisplayRequired = 0x00000002,
            EsSystemRequired = 0x00000001
        }

        [LibraryImport("kernel32.dll", SetLastError = true)]
        private static partial uint SetThreadExecutionState(ExecutionState esFlags);

        #endregion

        private readonly AutoResetEvent _resetEvent = new(false);
        private readonly TaskFactory _taskFactory = new();

        public void RequireDisplayOn()
        {
            _taskFactory.StartNew(() =>
            {
                SetThreadExecutionState(
                    ExecutionState.EsContinuous
                    | ExecutionState.EsDisplayRequired
                    | ExecutionState.EsSystemRequired);
                _resetEvent.WaitOne();

            }, TaskCreationOptions.LongRunning);
        }

        public void Shutdown()
        {
            _resetEvent.Set();
        }
    }
}
