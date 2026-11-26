using Bai6_7_PowerGridMonitor.GUI;

namespace Bai6_7_PowerGridMonitor;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new frmPowerGridMonitor());
    }
}
