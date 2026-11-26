using Bai6_6_BankAtmMdiSystem.GUI;

namespace Bai6_6_BankAtmMdiSystem;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new frmBankAtmMain());
    }
}
