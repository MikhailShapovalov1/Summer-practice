namespace task17
{
    public class SoftStop : ICommand
    {
        private readonly ServerThread _targetThread;

        public SoftStop(ServerThread targetThread)
        {
            _targetThread = targetThread;
        }

        public void Execute()
        {
            _targetThread.SoftStop();
        }
    }
}
