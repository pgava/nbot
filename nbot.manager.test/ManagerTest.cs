using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using nbot.contracts;
using Xunit;

namespace nbot.manager.test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var assemblyTask = AssemblyLoadContext.Default.LoadFromAssemblyPath(@"/home/paolo/Dev/robo/nbot/nbot.samples/bin/Debug/netstandard2.0\nbot.samples.dll");

            var task = CreateTask(assemblyTask);

            task.Run();
            //Parallel.For(0, 10, (i) => { task.Run(null); });
            //ThreadPool.QueueUserWorkItem(task.Run);

        }

        static INBot CreateTask(Assembly assembly)
        {
            int count = 0;
            IList<INBot> tasks = new List<INBot>();
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


            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements INBot in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }


            if (count > 1)
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
