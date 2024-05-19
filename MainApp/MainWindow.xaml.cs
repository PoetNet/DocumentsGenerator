using Shared.Interfaces;
using System.IO;
using System.Reflection;
using System.Windows;

namespace MainApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<IModule> _modules = new();
    public MainWindow()
    {
        InitializeComponent();
        LoadModules();
    }

    private void LoadModules()
    {
        var modulePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Modules");
        _modules = ModuleLoader.LoadModules(modulePath);
    }


    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        foreach (var module in _modules)
        {
            module.Execute();
        }
    }
}

public static class ModuleLoader
{
    public static List<IModule> LoadModules(string path)
    {
        var modules = new List<IModule>();
        var dlls = Directory.GetFiles(path, "*.dll");

        foreach (var dll in dlls)
        {
            var assembly = Assembly.LoadFrom(dll);
            var types = assembly.GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var type in types)
            {
                var module = Activator.CreateInstance(type) as IModule;
                if (module != null)
                {
                    modules.Add(module);
                }
            }
        }

        return modules;
    }
}