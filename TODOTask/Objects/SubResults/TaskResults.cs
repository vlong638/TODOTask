using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TODOTask.Objects.SubResults
{
    [DataContract]
    public enum CreateTaskResult
    {
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
    }
    [DataContract]
    public enum UpdateTaskResult
    {
        [EnumMember]
        None,
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
        [EnumMember]
        NotReady,
        [EnumMember]
        ReadyForStart,
    }
    [DataContract]
    public enum DeleteTaskResult
    {
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
        [EnumMember]
        NotReady,
        [EnumMember]
        DeleteEventFailed,
    }
    [DataContract]
    public enum StartTaskResult
    {
        [EnumMember]
        None,
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
        [EnumMember]
        NotReady,
        /// <summary>
        /// 缺少有效的事件,缺失事件或者获取事件计数时失败.
        /// </summary>
        [EnumMember]
        LackOfEvent,
        [EnumMember]
        UpdateTaskResult_IsSettled,
        [EnumMember]
        UpdateTaskResult_Failure,
    }
    [DataContract]
    public enum SettleTaskResult
    {
        [EnumMember]
        None,
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
    }
}
