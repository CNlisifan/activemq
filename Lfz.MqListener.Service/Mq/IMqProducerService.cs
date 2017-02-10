using System;
using System.Collections.Generic;
using Lfz.MqListener.Shared.Models;

namespace Lfz.MqListener.Mq
{
    /// <summary>
    /// ��Ϣ������
    /// </summary>
    public interface IMqProducerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId"></param> 
        /// <param name="ququqName"></param>
        /// <param name="content"></param>
        /// <param name="timeSpan"></param>
        /// <param name="propertydic"></param>
        bool SendQueueMessage(Guid storeId, QuqueName ququqName, object content, TimeSpan timeSpan,NMSMessageType? messageType=null, Dictionary<string, string> propertydic = null);

        /// <summary>
        /// ĳ����Ϣ������Ч��1��
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="ququqName"></param>
        /// <param name="content"></param> 
        bool SendQueueMessage(Guid storeId, QuqueName ququqName, object content, NMSMessageType? messageType = null);

        /// <summary>
        /// ĳ����Ϣ������Ч��7��
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="topicName"></param>
        /// <param name="content"></param> 
        bool SendTopicMessage(Guid storeId, TopicName topicName, object content, NMSMessageType? messageType = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="topicName"></param>
        /// <param name="content"></param>
        /// <param name="timeSpan"></param>
        /// <param name="propertydic"></param>
        bool SendTopicMessage(Guid storeId, TopicName topicName, object content, TimeSpan timeSpan, NMSMessageType? messageType = null, Dictionary<string, string> propertydic = null);
    }
}