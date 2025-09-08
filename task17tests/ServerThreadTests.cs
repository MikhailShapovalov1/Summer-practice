using task17;

namespace task17tests
{
    public class ActionCommand : ICommand
    {
        private readonly Action _action;

        public ActionCommand(Action action) => _action = action;

        public void Execute() => _action();
    }

    public class ServerThreadTests
    {
        [Fact]
        public void HardStop_StopsThreadImmediately()
        {
            var serverThread = new ServerThread();
            bool command1Executed = false;
            bool command2Executed = false;

            serverThread.Post(new ActionCommand(() => command1Executed = true));
            serverThread.Post(new HardStop(serverThread));
            serverThread.Post(new ActionCommand(() => command2Executed = true));

            Thread.Sleep(300);

            Assert.True(command1Executed);
            Assert.False(command2Executed);
            Assert.False(serverThread.IsRunning);
        }

        [Fact]
        public void SoftStop_StopsThreadAfterQueueIsEmpty()
        {
            var serverThread = new ServerThread();
            bool command1Executed = false;
            bool command2Executed = false;

            serverThread.Post(new ActionCommand(() => command1Executed = true));
            serverThread.Post(new SoftStop(serverThread));
            serverThread.Post(new ActionCommand(() => command2Executed = true));

            Thread.Sleep(100);

            Assert.True(command1Executed);
            Assert.True(command2Executed);
            Assert.False(serverThread.IsRunning);
        }

        [Fact]
        public void HardStop_ThrowsWhenExecutedInWrongThread()
        {
            var serverThread = new ServerThread();
            var hardStop = new HardStop(serverThread);

            Assert.Throws<InvalidOperationException>(() => hardStop.Execute());
        }

        [Fact]
        public void SoftStop_ThrowsWhenExecutedInWrongThread()
        {
            var serverThread = new ServerThread();
            var softStop = new SoftStop(serverThread);

            Assert.Throws<InvalidOperationException>(() => softStop.Execute());
        }
    }
}
