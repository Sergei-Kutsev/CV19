using CV19.Infrastructure.Commands.Base;
using System;

namespace CV19.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        //атрибут readonly, чтобы нельзя было изменить. Поля помеченные readonly работают быстрее
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        //в конструкторе надо получить два делегата (метода), один который будет выполняться методом CanExecute, а второй Execute
        public LambdaCommand(Action<object>//команды из разметки могут получать параметры, которые могут иметь разные типы данных, поэтому object
            Execute, Func<object, bool> CanExecute = null) //указываем два действия, которые команда будет выполнять
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute)); //выкидываем исключение, если сюда не передали аргумент
            _CanExecute = CanExecute;
        }
        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Execute(parameter);
    }
}
