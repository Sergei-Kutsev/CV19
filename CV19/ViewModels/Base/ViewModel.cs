using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CV19.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged //интерфейс способный уведомлять, что внутри нашего объекта изменилось какое-то свойство
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string PropertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>( //метод Set обновляет значение свойства, для которого определено поле в котором это поле хранит свои данные. А затем автоматически
                                       //обновлять второе свойство, а второе свойство порождает обновление третьего свойства, а третье - первое (КОЛЬЦЕВЫЕ ОБНОВЛЕНИЯ)
            ref T field, //сюда попадает ссылка на поле свойства
            T value,  //сюда передаем новое значение, которое хотим установить
            [CallerMemberName] string PropertyName = null) // имя нашего свойства, которое мы передаем в метод OnPropertyChanged 
        {
            if (Equals(field, //если значение поля, которое мы хотим обновить
                value)) //уже соответствует тому значению, которое мы передали, то
                return false; // возвращаем ложь

            //если же значение изменилось, то
            field = value; // обновляем поле
            OnPropertyChanged(PropertyName); //и генерируем OnPropertyChanged событие
            return true; // и возвращаем истину
        }
    }
}
