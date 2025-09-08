namespace task17
{
    public class HardStop : ICommand
    {
        private readonly ServerThread _targetThread;

        public HardStop(ServerThread targetThread)
        {
            _targetThread = targetThread;
        }

        public void Execute()
        {
            _targetThread.HardStop();
        }
    }
}
