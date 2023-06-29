using NUnit.Framework;

namespace ProcessesMonitor.UnitTests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void ProcessIsOpened()
        {
            string processName = "notepad";
            var result = Program.GetAllProcesses(processName);

            Assert.IsNotNull(result);
        }

        [Test]
        public void ProcessIsNotOpened()
        {
            string processName = "NotAnApplicationInTheList";
            var result = Program.GetAllProcesses(processName);

            Assert.IsNull(result);
        }

        [Test]
        public void ProcessIsFound()
        {
            string processName = "notepad";
            var result = Program.GetAllProcesses(processName);

            Assert.AreEqual(processName.ToLower(), result.ProcessName.ToLower());
        }
    }
}
