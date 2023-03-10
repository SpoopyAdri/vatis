using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ninject;
using Serilog;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Container;
using Vatsim.Vatis.Core;
using Vatsim.Vatis.Io;

namespace Vatsim.Vatis;

internal static class Program
{
    private static IKernel mContainer;
    private static IConfig mAppConfig;

    [STAThread]
    private static void Main(string[] args)
    {
        if (IsAlreadyRunning())
        {
            Application.Exit();
            return;
        }

        Application.CurrentCulture = new CultureInfo("en-US");
        Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

        var processPath = Environment.ProcessPath;
        if (processPath == null)
        {
            ShowError("Process path returned null.");
            Application.Exit();
            return;
        }

        var installPath = Path.GetDirectoryName(processPath);
        if (installPath == null)
        {
            ShowError("Install path returned null.");
            Application.Exit();
            return;
        }

        installPath = Directory.GetParent(installPath)?.FullName;
        if (Debugger.IsAttached)
        {
            installPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "org.vatsim.vatis");
        }

        PathProvider.SetInstallPath(installPath);

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter()
            }
        };

        SetupLogging();

        var informationalVersion = Assembly.GetEntryAssembly()
            ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        Log.Information($"vATIS version {informationalVersion} starting up");

        mContainer = new StandardKernel(new Bindings());

        Application.Run(mContainer.Get<VatisAppContext>());
    }

    private static bool IsAlreadyRunning()
    {
        FileSystemInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
        Mutex mutex = new(true, "Global\\" + fileInfo.Name, out var createdNew);
        if (createdNew)
        {
            mutex.ReleaseMutex();
        }

        return !createdNew;
    }

    private static void SetupLogging()
    {
        var logPath = Path.Combine(PathProvider.LogsFolderPath, "Log.txt");
        var config = new LoggerConfiguration().WriteTo.File(logPath);
        Log.Logger = config.CreateLogger();
    }

    private static void ShowError(string error)
    {
        MessageBox.Show("An error has occurred. Please refer to the log file for details.\n\n" + error,
            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            Log.Error(ex, "Unhandled exception");
            ShowError(ex.Message);
        }
    }

    private static void OnProcessExit(object sender, EventArgs e)
    {
        mAppConfig?.SaveConfig();
    }
}