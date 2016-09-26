using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompilerLibrary;
using System.Threading;

namespace CompilerWpf
{
    [ImplementPropertyChanged]
    class WindowViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// Список задач отображаемый в GUI
        /// </summary>
        public ObservableCollection<TaskBuildModel> Tasks { get; set; }

        /// <summary>
        /// Команда "Добавить задачу"
        /// </summary>
        public RelayCommand<object> AddTaskCommand { get; set; }
        
        /// <summary>
        /// Путь к проекту для новой задачи
        /// </summary>
        public string ProjectPath { get; set; }

        /// <summary>
        /// Асинхронный, последовательный компилятор
        /// </summary>
        private QCompiler QComp;

        public WindowViewModel(QCompiler compiler)
        {
            ProjectPath = "TestProject";
            Tasks = new ObservableCollection<TaskBuildModel>();
            AddTaskCommand = new RelayCommand<object>(AddTaskExecuted);
            
            QComp = compiler;
        }

        /// <summary>
        /// Действие выполняемое при нажатии на кнопку "Добавить задачу".
        /// </summary>
        /// <param name="args"></param>
        private async void AddTaskExecuted(object args)
        {
            var path = (string)args;

            //Начинаем сборку проекта
            var buildEvent = new SemaphoreSlim(0, 1);
            var task = QComp.BuildProjectAsync(path, (x) => buildEvent.Release());

            //Создаём объект с информацией о задаче
            var taskModel = new TaskBuildModel();
            taskModel.StartTime = DateTime.Now;
            taskModel.Status = task.Status;
            taskModel.ProjectPath = path;
            Tasks.Add(taskModel);

            //Ожидаем когда очередь выполнения дойдёт до текущей задачи
            await buildEvent.WaitAsync();
            taskModel.Status = task.Status;

            //Ожидаем результат выполнения задачи
            var bytes = await task;
            taskModel.Status = task.Status;
            taskModel.EndTime = DateTime.Now;
            taskModel.ResultData = Encoding.UTF8.GetString(bytes);
        }
    }
}
