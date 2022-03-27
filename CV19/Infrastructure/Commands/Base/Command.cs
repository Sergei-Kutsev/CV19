using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CV19.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged //событие гененрируется, когда CanExecute меняет состояние из одного в другое
        {
            add => CommandManager.RequerySuggested += value; //передаем управление событием
            remove => CommandManager.RequerySuggested += value;
        }
        public abstract bool CanExecute(object parameter); //функция которая возвращает истину/ложь. Если ложь, то команду выполнить нельзя,
                                                //если команду выполнить нельзя, то элемент к которому привязана команда отключчается
                                                //автоматически, т.е. когда мы описываем команды мы можем контролировать с их помощью
                                                //активность визуальных элементов на экране


        public abstract void Execute(object parameter); // метод который будет выполен самой командой
        
    }
}
