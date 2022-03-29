using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        // в каждом поле должен быть вот такой вот код
        #region "Заголовок окна"
        private string _Title = "Анализ статистики CV19";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //    if (Equals(_Title, value)) return;
            //    _Title = value;
            //    OnPropertyChanged();
            //}
            set => Set(ref _Title, value);
        }
        #endregion

        #region Команды
        //команда которая закрывает нашу программу
        #region CloseApplicationCommand 
        public ICommand CloseApplicationCommand { get;  } //Свойство - команда, которое закрывает программу

        private bool CanCloseApplicationCommandExecute(object p) => true; //метод выполняется вместе с командой
                    
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        public MainWindowViewModel() //внутри конструктора создаем значение команды
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion
        }
    }
}
