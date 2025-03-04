using System.Management;

namespace FindPrimes;

public static class CPUInfo
{
    public static void Get()
    {
        var mc = new ManagementClass("Win32_Processor");
        var moc = mc.GetInstances();
        foreach (var m in moc)
        {
            foreach ( var prop in m.Properties)
            {
                if(prop.Value != null)
                {
                    Console.WriteLine($"{prop.Name} = {prop.Value};");
                }
            }
        }
    }
}
