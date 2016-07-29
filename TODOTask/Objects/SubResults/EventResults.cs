using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TODOTask.Objects.SubResults
{
    [DataContract]
    public enum CreateEventResult
    {
        [EnumMember]
        None,
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
        [EnumMember]
        FetchTaskFailed,
        [EnumMember]
        UpdateTaskResult_Failure,
        [EnumMember]
        UpdateTaskResult_NotReady,
        [EnumMember]
        UpdateTaskResult_NotProcessing,
    }
    [DataContract]
    public enum DeleteEventResult
    {
        [EnumMember]
        None,
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
        [EnumMember]
        FetchTaskFailed,
        [EnumMember]
        UpdateTaskResult_Failure,
        [EnumMember]
        UpdateTaskResult_NotReady,
        [EnumMember]
        UpdateTaskResult_NotProcessing,
    }
    [DataContract]
    public enum SettleEventResult
    {
        [EnumMember]
        None,
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
        [EnumMember]
        FetchTaskFailed,
        [EnumMember]
        AllreadySettled,
        [EnumMember]
        UpdateTaskResult_Failure,
        [EnumMember]
        UpdateTaskResult_NotReady,
        [EnumMember]
        UpdateTaskResult_NotProcessing,
    }
}
