using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using nbot.contracts;
using Xunit;

namespace nbot.manager.test
{
    public class ManagerLoadBotTest
    {
        [Fact]
        public void Can_Load_Bot_From_Assembly()
        {

            // LoadFromAssemblyPath requires full path
            var curDir = Directory.GetCurrentDirectory();
            var path = Path.Combine(curDir, "../../../../nbot.samples/bin/Debug/netstandard2.0/nbot.samples.dll");

            var assemblyTask = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);

            var task = CreateTask(assemblyTask);

            task.Run();

        }

        static private INBot CreateTask(Assembly assembly)
        {
            int count = 0;
            IList<INBot> tasks = new List<INBot>();

            // Find all objects of type INBot
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(INBot).IsAssignableFrom(type))
                {
                    INBot result = Activator.CreateInstance(type) as INBot;
                    if (result != null)
                    {
                        count++;
                        tasks.Add(result);
                    }
                }
            }

            // Perform sanity check on assembly
            // ...
            
            // Can have only one INBot
            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements INBot in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
            else if (count > 1)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Too many types which implements INBot in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }

            return tasks.First();
        }
    }
}
