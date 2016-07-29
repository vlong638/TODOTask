using VL.Common.ORM.Objects;

namespace TODOTask.Objects.Entities
{
    public class TEventProperties
    {
        #region Properties
        public static PDMDbProperty TaskId { get; set; } = new PDMDbProperty(nameof(TaskId), "TaskId", "关联标识符", false, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty EventId { get; set; } = new PDMDbProperty(nameof(EventId), "EventId", "标识符", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty Topic { get; set; } = new PDMDbProperty(nameof(Topic), "Topic", "做什么(主题)", false, PDMDataType.nvarchar, 1000, 0, true, null);
        public static PDMDbProperty Participant { get; set; } = new PDMDbProperty(nameof(Participant), "Participant", "参与者(人)", false, PDMDataType.nvarchar, 1000, 0, true, null);
        public static PDMDbProperty WhenToStart { get; set; } = new PDMDbProperty(nameof(WhenToStart), "WhenToStart", "预计开始时间", false, PDMDataType.datetime, 0, 0, false, null);
        public static PDMDbProperty WhenToEnd { get; set; } = new PDMDbProperty(nameof(WhenToEnd), "WhenToEnd", "预计结束时间", false, PDMDataType.datetime, 0, 0, false, null);
        public static PDMDbProperty WhenStart { get; set; } = new PDMDbProperty(nameof(WhenStart), "WhenStart", "实际开始时间", false, PDMDataType.datetime, 0, 0, false, null);
        public static PDMDbProperty WhenEnd { get; set; } = new PDMDbProperty(nameof(WhenEnd), "WhenEnd", "实际结束时间", false, PDMDataType.datetime, 0, 0, false, null);
        public static PDMDbProperty DealStatus { get; set; } = new PDMDbProperty(nameof(DealStatus), "DealStatus", "执行情况", false, PDMDataType.numeric, 2, 0, true, null);
        public static PDMDbProperty Version { get; set; } = new PDMDbProperty(nameof(Version), "Version", "版本号", false, PDMDataType.numeric, 10, 0, true, null);
        #endregion
    }
}
