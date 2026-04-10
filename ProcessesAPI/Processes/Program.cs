using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("psapi.dll", SetLastError = true)]
    public static extern bool EnumProcesses(
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)][In][Out] uint[] processIds,
        uint arraySizeBytes,
        out uint bytesReturned);

    [DllImport("psapi.dll", SetLastError = true)]
    public static extern bool GetProcessMemoryInfo(
        IntPtr hProcess,
        out PROCESS_MEMORY_COUNTERS counters,
        uint size);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool CloseHandle(IntPtr hObject);

    // ✅ Fixed: SIZE_T fields must be UIntPtr (8 bytes on x64, 4 bytes on x86)
    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_MEMORY_COUNTERS
    {
        public uint cb;
        public uint PageFaultCount;
        public UIntPtr PeakWorkingSetSize;   // SIZE_T
        public UIntPtr WorkingSetSize;        // SIZE_T
        public UIntPtr QuotaPeakPagedPoolUsage;
        public UIntPtr QuotaPagedPoolUsage;
        public UIntPtr QuotaPeakNonPagedPoolUsage;
        public UIntPtr QuotaNonPagedPoolUsage;
        public UIntPtr PagefileUsage;
        public UIntPtr PeakPagefileUsage;
    }

    const uint PROCESS_QUERY_INFORMATION = 0x0400;
    const uint PROCESS_VM_READ = 0x0010;

    static void Main()
    {
        uint[] processIds = new uint[1024];

        if (!EnumProcesses(processIds, (uint)processIds.Length * sizeof(uint), out uint bytesReturned))
        {
            Console.WriteLine("Failed to enumerate processes.");
            Console.ReadKey();
            return;
        }

        int count = (int)(bytesReturned / sizeof(uint));
        Console.WriteLine($"Number of processes: {count}\n");

        for (int i = 0; i < count; i++)
        {
            uint pid = processIds[i];
            IntPtr handle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, pid);
            if (handle == IntPtr.Zero) continue;

            try
            {
                var counters = new PROCESS_MEMORY_COUNTERS();
                counters.cb = (uint)Marshal.SizeOf<PROCESS_MEMORY_COUNTERS>(); // ✅ Set cb before calling

                if (GetProcessMemoryInfo(handle, out counters, counters.cb))
                {
                    string name = "Unknown";
                    try { name = Process.GetProcessById((int)pid).ProcessName; }
                    catch { }

                    long memKb = (long)counters.WorkingSetSize / 1024;
                    Console.WriteLine($"PID: {pid,-6} | {name,-30} | Memory: {memKb,8} KB");
                }
            }
            finally
            {
                CloseHandle(handle);
            }
        }

        Console.ReadKey();
    }
}