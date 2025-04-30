using System.Reflection;
using Ecommerce.Commons.Abstraction.Modules;

namespace Ecommerce.Api.Modules;

internal static class ModuleLoader
{
    private const string ModuleName = "Ecommerce.Modules.";
    public static IList<IModule> LoadModules()
    {
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, ModuleName + "*.dll");
        foreach (var path in referencedPaths)
        {
            if (!loadedAssemblies.Any(a => a.Location == path))
            {
                Assembly.LoadFrom(path);
            }
        }
        
        var modules = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a => a.GetName().Name!.StartsWith(ModuleName))
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => (IModule)Activator.CreateInstance(t)!)
            .ToList();

        return modules;
    }
}