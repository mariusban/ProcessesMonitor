# ProcessesMonitor
tiny C# utility executable to monitor Windows processes and kill the processes that work longer than the threshold specified

Executable path: "YourPath"\ProcessesMonitor\ProcessesMonitor\bin\Release\net6.0\publish\ProcessesMonitor.exe

ProcessesMonitor.exe [nameApp] [maxTimeToCloseApp] [timeToCheckAppIfOpen]

Example:
ProcessesMonitor.exe notepad 3 1
