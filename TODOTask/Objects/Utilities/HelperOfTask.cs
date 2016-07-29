using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOTask.Objects.Utilities
{
    public static class HelperOfTask
    {
        public enum TracingType
        {
            CreateTask,
            StartTask,
            SettleTask,
            AddEvent,
            RemoveEvent,
            ChangeEventDealStatus,
            RestartTask,
        }
        public static string GetTracingMessage(TracingType type, params string[] args)
        {
            //TEST 移除版本化的任务变更记录
            return "";

            string message = DateTime.Now + ":";
            switch (type)
            {
                case TracingType.CreateTask:
                    message += "创建了任务";
                    break;
                case TracingType.StartTask:
                    message += "任务变更为开始状态";
                    break;
                case TracingType.RestartTask:
                    message += "任务变更为未完成状态";
                    break;
                case TracingType.SettleTask:
                    message += "任务变更为已完成状态";
                    break;
                case TracingType.AddEvent:
                    message += string.Format("增加了子任务事件:{0}" , args);
                    break;
                case TracingType.RemoveEvent:
                    message += string.Format("移除了子任务事件:{0}", args);
                    break;
                case TracingType.ChangeEventDealStatus:
                    message += string.Format("完成了子任务事件:{0}", args);
                    break;
                default:
                    break;
            }
            message += Environment.NewLine;
            return message;
        }
    }
}
