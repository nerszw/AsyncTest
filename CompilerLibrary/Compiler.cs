using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompilerLibrary
{
    /// <summary>
    /// Исходный класс компилятора.
    /// Не изменный, за исключением объявления переменной dllFilePath.
    /// </summary>
    public class Compiler
    {
        public byte[] BuildProject(string projectPath)
        {
            // Имитируем бурную деятельность.
            Thread.Sleep(500);

            // bug fixed, no comments... copyright© nerszw@gmail.com 2016
            var dllFilePath = projectPath + ".dll";

            // В реальности здесь будут байты собранной dll-ки.
            return Encoding.UTF8.GetBytes(dllFilePath);
        }
    }
}
