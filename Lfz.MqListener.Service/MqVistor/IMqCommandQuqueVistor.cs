using Lfz.MqListener.Mq;
using Lfz.MqListener.Shared.Models;

namespace Lfz.MqListener.MqVistor
{
    /// <summary>
    /// ����ģʽ���������
    /// </summary>
    public interface IMqCommandQuqueVistor : ISingletonDependency
    {
        /// <summary>
        /// ���д������
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="commandInfo"></param>
        void Vistor(QuqueName queueName, MqCommandInfo commandInfo);
    }
}