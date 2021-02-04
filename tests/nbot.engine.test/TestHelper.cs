using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using nbot.actions;
using nbot.contract;

namespace nbot.engine.test
{
    public class TestHelper
    {
        static public string GetCurrentFullPath()
        {
            // LoadFromAssemblyPath requires full path
            var curDir = Directory.GetCurrentDirectory();
            var path = Path.Combine(curDir, "../../../../../nbot.samples/bin/Debug/net5.0/nbot.samples.dll");

            return path;
        }

        static public Assembly GetAssembly()
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyPath(GetCurrentFullPath());
        }

        static public Bot CreateBot(Assembly assembly)
        {
            var tasks = LoadBots(assembly);

            // Perform sanity check on assembly
            // ...

            // Can have only one IBot
            if (tasks.Count() == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements IBot in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
            else if (tasks.Count() > 1)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Too many types which implements IBot in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }

            return tasks.First();
        }

        static public IEnumerable<Bot> LoadBots(Assembly assembly)
        {
            IList<Bot> tasks = new List<Bot>();

            // Find all objects of type IBot
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IBot).IsAssignableFrom(type))
                {
                    Bot result = Activator.CreateInstance(type) as Bot;
                    if (result != null)
                    {
                        tasks.Add(result);
                    }
                }
            }

            return tasks;
        }
    }

    public class BaseBot : IBotController
    {
        public BaseBot(string name)
        {
            Name = name;
        }
        public bool IsRunning => throw new NotImplementedException();
        public bool IsWaiting => throw new NotImplementedException();
        public bool IsAlive => throw new NotImplementedException();

        public string Name { get; }

        public Point CalculateNextPosition()
        {
            throw new NotImplementedException();
        }

        public void Turn()
        {
            throw new NotImplementedException();
        }

        public void Wakeup()
        {
            throw new NotImplementedException();
        }
    }

    public class RandomBotsProviderMock : IRandomBots
    {
        public IEnumerable<IBotController> RandomizeList(IList<IBotController> items)
        {
            throw new System.NotImplementedException();
        }
    }
}
