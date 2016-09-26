using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerWpf
{
    /// <summary>
    /// Данные о выполняемой задаче (компиляции проекта).
    /// </summary>
    [ImplementPropertyChanged]
    class TaskBuildModel : NotifyPropertyChanged
    {
        /// <summary>
        /// Состояние задачи.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Путь к файлу проекта.
        /// </summary>
        public string ProjectPath { get; set; }

        /// <summary>
        /// Результат выполнения (преобразованный в строку).
        /// </summary>
        public string ResultData { get; set; }

        /// <summary>
        /// Время когда задача была добавлена в очередь на выполнение.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время когда задача была выполнена.
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
