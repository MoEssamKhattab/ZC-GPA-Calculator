namespace ZC_GPA_Calculator;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

		// to ensure consistent rendering across different windows versions
		Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		Application.Run(new MainForm());
    }
}