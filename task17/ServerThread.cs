using System.Collections.Concurrent;

namespace task17
{
    public class ServerThread 
    {
        public bool IsRunning => _isRunning;

        private Thread _thread;
        private BlockingCollection<ICommand> _commandQueue = new();
        private bool _isRunning = true;
        private bool _softStopRequested = false;

        public ServerThread()
        {
            _thread = new Thread(Run);

            _thread.Start();
        }

        private void Run()
        {
            while (_isRunning)
            {
                if (_commandQueue.TryTake(out ICommand? command, Timeout.Infinite))
                {
                    command.Execute();
                }

                if (_softStopRequested && _commandQueue.Count == 0)
                {
                    _isRunning = false;
                }
            }

            _commandQueue.Dispose();
        }

        public void Post(ICommand command)
        {
            if (!_isRunning)
            {
                throw new InvalidOperationException("Thread is not running");
            }

            _commandQueue.Add(command);
        }

        public void HardStop()
        {
            if (Thread.CurrentThread != _thread)
            {
                throw new InvalidOperationException("Not target thread");
            }

            _isRunning = false;
        }

        public void SoftStop()
        {
            if (Thread.CurrentThread != _thread)
            {
                throw new InvalidOperationException("Not target thread");
            }

            _softStopRequested = true;
        }
    }
}
