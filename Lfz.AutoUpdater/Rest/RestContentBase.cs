using System.ComponentModel;

namespace Lfz.AutoUpdater.Rest
{
    /// <summary>
    /// ֧��Rest���������������ࣨ��JSON���л���
    /// </summary> 
    internal class RestContentBase : IJsonContent, INotifyPropertyChanged, INotifyPropertyChanging
    {  
        /// <summary>
        /// �����޸���ɴ����¼�
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// ���������޸���ɴ����¼�
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// ���������޸���ɴ����¼�
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        protected virtual void OnPropertyChanging(string propertyName, string newValue)
        {
            PropertyChangingEventHandler handler = PropertyChanging;
            if (handler != null) handler(this, new CustomPropertyChangingEventArgs<string>(propertyName, newValue));
        }
    }

    /// <summary>
    /// �����޸��¼��Զ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class CustomPropertyChangingEventArgs<T> : PropertyChangingEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newValue"></param>
        public CustomPropertyChangingEventArgs(string propertyName, T newValue)
            : base(propertyName)
        {
            NewValue = newValue;
        }

        /// <summary>
        /// ������ֵ
        /// </summary>
        public T NewValue { get; set; }
    }
}