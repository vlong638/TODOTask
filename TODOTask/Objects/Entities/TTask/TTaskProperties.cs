using VL.Common.ORM.Objects;

namespace TODOTask.Objects.Entities
{
    public class TTaskProperties
    {
        #region Properties
        public static PDMDbProperty TaskId { get; set; } = new PDMDbProperty(nameof(TaskId), "TaskId", "标识符", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty Topic { get; set; } = new PDMDbProperty(nameof(Topic), "Topic", "做什么(主题)", false, PDMDataType.nvarchar, 1000, 0, true, null);
        public static PDMDbProperty Tracing { get; set; } = new PDMDbProperty(nameof(Tracing), "Tracing", "执行记录(追踪)", false, PDMDataType.nvarchar, 1000, 0, true, null);
        public static PDMDbProperty DealStatus { get; set; } = new PDMDbProperty(nameof(DealStatus), "DealStatus", "执行情况", false, PDMDataType.numeric, 2, 0, true, null);
        public static PDMDbProperty Version { get; set; } = new PDMDbProperty(nameof(Version), "Version", "版本号", false, PDMDataType.numeric, 10, 0, true, null);
        #endregion
    }
}
