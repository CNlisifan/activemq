using System;

namespace Lfz.Network
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataEventArgs<T> : EventArgs
    {
        /// <summary>
        /// �¼�Я��������
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Obj { get; set; }
    }
}