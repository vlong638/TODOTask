using System.Runtime.Serialization;

namespace TODOTask.Objects.Enums
{
    [DataContract]
    public enum EEventDealStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [EnumMember]
        None,
        /// <summary>
        /// 待机
        /// </summary>
        [EnumMember]
        Ready,
        /// <summary>
        /// 进行中
        /// </summary>
        [EnumMember]
        Processing,
        /// <summary>
        /// 处理完成
        /// </summary>
        [EnumMember]
        Settled,
        /// <summary>
        /// 处理失败
        /// </summary>
        [EnumMember]
        Unsettled,
    }
}
