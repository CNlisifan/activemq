using Newtonsoft.Json;

namespace Lfz.AutoUpdater.Rest
{
    /// <summary>
    /// Rest������Ӧ��Ϣ(�����ʹ�ã���Ҫ���л���λ��)
    /// </summary>
    internal sealed class ResponseContent : ResponseContent<IJsonContent>
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ResponseContent()
            : this(new EmptyContent() { })
        {
        }

        /// <summary>
        /// ����Rest���ݹ�����Ӧ��Ϣ
        /// </summary>
        /// <param name="content"></param>
        public ResponseContent(IJsonContent content)
        {
            this.Content = content;
        }

        /// <summary>
        ///  Rest������Ӧ����
        /// </summary>
        [JsonProperty("Result")]
        [RestResolverFilter(JsonPropertyFilterEnum.RestResponseResult)]
        public override IJsonContent Content { get; set; } 
    }
    /// <summary>
    /// CQRSģʽ��Command�ķ��ؽ��
    /// </summary>
    internal interface ICommandResult
    {


    }
    /// <summary>
    /// Rest������Ӧ��Ϣ(�ͻ���ʹ�ã���Ҫ�����л���λ��)
    /// </summary>
    internal class ResponseContent<TContent> : RestContentBase, ICommandResult
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ResponseContent()
        {
        }

        /// <summary>
        ///  Rest������Ӧ����
        /// </summary>
        [JsonProperty("Result")]
        public virtual TContent Content { get; set; }

        /// <summary>
        /// Rest������Ӧ״̬
        /// </summary> 
        [JsonProperty("StatusCode")]
        public RestStatus StatusCode { get; set; }
    }
}