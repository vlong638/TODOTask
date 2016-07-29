using System.Runtime.Serialization;

namespace TODOTask.Objects.Enums
{
    [DataContract]
    public enum EDealStatus
    {
        /// <summary>
        /// 待机
        /// </summary>
        [EnumMember]
        Ready = 0,
        /// <summary>
        /// 进行中
        /// </summary>
        [EnumMember]
        Processing = 1,
        /// <summary>
        /// 处理完成
        /// </summary>
        [EnumMember]
        Settled = 2,
        /// <summary>
        /// 处理失败
        /// </summary>
        [EnumMember]
        Unsettled = 3,
    }
}
