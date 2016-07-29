using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOTask.Objects.Entities;
using VL.Common.DAS.Objects;
using VL.Common.Protocol.IService;

namespace TODOTask.Objects.DomainFacades
{
    /// <summary>
    /// 浏览用户
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// 获取任务下未完成的事件计数
        /// </summary>
        public Result<int> GetUnsettledEventCount(DbSession session, Guid taskId)
        {
            Result<int> result = new Result<int>(nameof(GetUnsettledEventCount));
            var value = new TTask() { TaskId = taskId }.GetUnsettledEventCount(session);
            if (value.HasValue)
            {
                result.ResultCode = EResultCode.Success;
                result.Data = value.Value;
            }
            else
            {
                result.ResultCode = EResultCode.Failure;
            }
            return result;
        }
        /// <summary>
        /// 获取任务下未完成的事件计数
        /// </summary>
        public Result<List<TTask>> GetAllTasks(DbSession session)
        {
            Result<List<TTask>> result = new Result<List<TTask>>(nameof(GetAllTasks));
            result.Data= new TTask().GetAllTasks(session);
            result.ResultCode = EResultCode.Success;
            return result;
        }
        /// <summary>
        /// 获取任务下未完成的事件计数
        /// </summary>
        public Result<List<TTask>> GetAllTODOTasksWithEvents(DbSession session)
        {
            Result<List<TTask>> result = new Result<List<TTask>>(nameof(GetAllTasks));
            result.ResultCode = EResultCode.Success;
            result.Data = new TTask().GetAllTODOTasks(session);
            foreach (var task in result.Data)
            {
                if (!task.FetchEvents(session))
                {
                    result.ResultCode = EResultCode.Failure;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 获取任务下未完成的事件计数
        /// </summary>
        public Result<List<TTask>> GetAllTasksWithEvents(DbSession session)
        {
            Result<List<TTask>> result = new Result<List<TTask>>(nameof(GetAllTasks));
            result.ResultCode = EResultCode.Success;
            result.Data = new TTask().GetAllTasks(session);
            foreach (var task in result.Data)
            {
                if (!task.FetchEvents(session))
                {
                    result.ResultCode = EResultCode.Failure;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 获取任务下未完成的事件计数
        /// </summary>
        public Result<List<TEvent>> GetAllEvents(DbSession session)
        {
            Result<List<TEvent>> result = new Result<List<TEvent>>(nameof(GetAllEvents));
            result.Data = new TEvent().GetAllEvents(session);
            result.ResultCode = EResultCode.Success;
            return result;
        }
    }
}
