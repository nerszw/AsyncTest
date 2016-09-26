using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompilerLibrary
{
    /// <summary>
    /// Обёртка над классом Compiler.
    /// Для асинхронного, последовательного выполнения компиляции проектов.
    /// </summary>
    public class QCompiler
    {
        private object LockBuild;
        private Task LastTask;
        private Compiler Compiler;

        public QCompiler(Compiler compiler)
        {
            LockBuild = new object();
            LastTask = Task.CompletedTask;
            Compiler = compiler;
        }
        
        public Task<byte[]> BuildProjectAsync(string projectPath, Action<Task> onBeforeBuild)
        {
            Task<byte[]> last = null;

            lock (LockBuild)
            {
                last = LastTask.ContinueWith(onBeforeBuild).ContinueWith((task) =>
                {
                    return Compiler.BuildProject(projectPath);
                });
                LastTask = last;
            }

            return last;
        }
    }
}
