using System.ComponentModel;
using System.Diagnostics;

class Program
{
    public static System.Timers.Timer checkTimer;
    public static System.Timers.Timer maxTimer;

    public static void CloseAppStatement()
    {
        Console.WriteLine("Press ESC to stop this exe.");
        while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
    }
    public static Process? GetAllProcesses(string processName)
    {
        Process[] processes = Process.GetProcesses();

        for (int i = 0; i < processes.Length; i++)
        {
            Process process = processes[i];
            try
            {
                if (process.ProcessName.ToLower().Contains(processName.ToLower()))
                {
                    Console.WriteLine("The app " + process.ProcessName + " is running.");
                    return process;
                }
            }
            catch (Win32Exception)
            {
                Console.WriteLine("{0} : could not parse", i);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("{0} : has exited", i);
            }
        }
        Console.WriteLine("The App " + processName + " is not running.");
        return null;
    }
    public static void HandleCheckTimer(string processName)
    {
        Console.WriteLine("Check if the App " + processName + " is still open.");
        GetAllProcesses(processName);
    }

    public static void HandleMaxTimer(string processName)
    {
        var process = GetAllProcesses(processName);

        if (process != null)
        {
            process.Kill();
        }
        maxTimer.Dispose();
        checkTimer.Dispose();
        Console.WriteLine("The App " + processName + " was closed.");
        CloseAppStatement();
    }

    public static void StartProcess(string processName, int maxTimerRun, int checkTimerRun)
    {
        Console.WriteLine("Search for the App " + processName + " if is running.");
        var process = GetAllProcesses(processName);

        if (process == null)
        {
            maxTimer.Dispose();
            CloseAppStatement();
        }

        checkTimer = new System.Timers.Timer(1000 * 60 * checkTimerRun);
        checkTimer.Elapsed += (sender, e) => HandleCheckTimer(processName);
        checkTimer.Start();
    }

    public static void Main(string[] args)
    {
        if (args.Length == 3)
        {
            try
            {
                Convert.ToInt32(args[1]);
                Convert.ToInt32(args[2]);
            }
            catch (FormatException)
            {
                Console.WriteLine("The Second and the Third Arguments need to be int format.");
            }

            var maxTimerRun = Convert.ToInt32(args[1]);
            maxTimer = new System.Timers.Timer(1000 * 60 * maxTimerRun);

            maxTimer.Elapsed += (sender, e) => HandleMaxTimer(args[0]);
            maxTimer.Start();

            StartProcess(args[0].ToLower(), maxTimerRun, Convert.ToInt32(args[2]));
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("You need to add only 3 arguments to this utility program.");
        }
    }
}