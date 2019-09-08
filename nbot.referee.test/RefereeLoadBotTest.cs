using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using nbot.contracts;
using Xunit;

namespace nbot.referee.test
{
    public class RefereeLoadBotTest
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

        static private IBot CreateTask(Assembly assembly)
        {
            int count = 0;
            IList<IBot> tasks = new List<IBot>();

            // Find all objects of type IBot
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IBot).IsAssignableFrom(type))
                {
                    IBot result = Activator.CreateInstance(type) as IBot;
                    if (result != null)
                    {
                        count++;
                        tasks.Add(result);
                    }
                }
            }

            // Perform sanity check on assembly
            // ...

            // Can have only one IBot
            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements IBot in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
            else if (count > 1)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Too many types which implements IBot in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }

            return tasks.First();
        }
    }
}
