using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TODOTask.Objects.Enums
{
    [DataContract]
    public enum EEventChange
    {
        [EnumMember]
        AddEvent,
        [EnumMember]
        RemoveEvent,
        [EnumMember]
        ChangeEventDealStatus,
    }
}
