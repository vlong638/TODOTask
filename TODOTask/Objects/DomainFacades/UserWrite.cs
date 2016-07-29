using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOTask.Objects.SubResults;
using VL.Common.DAS.Objects;
using VL.Common.Protocol.IService;
using TODOTask.Objects.Entities;
using TODOTask.Objects.Enums;

namespace TODOTask.Objects.DomainFacades
{
    /// <summary>
    /// 用户操作
    /// "A对B操作,A主导了B的哪些信息":User了解TaskId,知道对哪一个Task进行操作
    /// "A对B操作,B应该知道自己的哪些信息":Task知道自己的这一个操作需要自身的Tracing和Version信息
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// 创建任务
        /// </summary>
        public Result CreateTask(DbSession session, string topic)
        {
            Result result = new Result(nameof(CreateTask));
            result.ResultCode = new TTask().BLCreate(session, topic) ? EResultCode.Success : EResultCode.Failure;
            return result;
        }
        /// <summary>
        /// 启动任务
        /// </summary>
        public Result<StartTaskResult> StartTask(DbSession session, Guid taskId)
        {
            Result<StartTaskResult> result = new Result<StartTaskResult>(nameof(CreateTask));
            ///这一层模拟User对Task进行操作
            ///User了解TaskId,知道对哪一个Task进行操作
            ///Task知道自己的这一个操作需要自身的Tracing和Version信息
            result.Data = new TTask() { TaskId = taskId }.BLStart(session);
            result.ResultCode = result.Data == StartTaskResult.Success ? EResultCode.Success : EResultCode.Failure;
            return result;
        }
        /// <summary>
        /// 更新任务描述性内容
        /// </summary>
        public Result<UpdateTaskResult> UpdateTaskDescriptionalContent(DbSession session, Guid taskId, string topic)
        {
            Result<UpdateTaskResult> result = new Result<UpdateTaskResult>(nameof(CreateTask));
            result.Data = new TTask() { TaskId = taskId, Topic = topic }.BLUpdateDescriptionalContent(session, nameof(TTask.Topic));
            result.ResultCode = result.Data == UpdateTaskResult.Success ? EResultCode.Success : EResultCode.Failure;
            return result;
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        public Result<DeleteTaskResult> DeleteTask(DbSession session, Guid taskId)
        {
            Result<DeleteTaskResult> result = new Result<DeleteTaskResult>(nameof(CreateTask));
            result.Data = new TTask() { TaskId = taskId }.BLDelete(session);
            result.ResultCode = result.Data == DeleteTaskResult.Success ? EResultCode.Success : EResultCode.Failure;
            return result;
        }
        /// <summary>
        /// 创建事项
        /// </summary>
        public Result<CreateEventResult> CreateEvent(DbSession session, Guid taskId, string topic)
        {
            Result<CreateEventResult> result = new Result<CreateEventResult>(nameof(CreateEvent));
            result.Data = new TEvent().BLCreate(session, taskId, Name, topic);
            result.ResultCode = result.Data == CreateEventResult.Success ? EResultCode.Success : EResultCode.Failure;
            return result;
        }
        /// <summary>
        /// 删除事项
        /// </summary>
        public Result<DeleteEventResult> DeleteEvent(DbSession session, Guid eventId)
        {
            Result<DeleteEventResult> result = new Result<DeleteEventResult>(nameof(DeleteEvent));
            result.Data = new TEvent() { EventId = eventId }.BLDelete(session);
            result.ResultCode = result.Data == DeleteEventResult.Success ? EResultCode.Success : EResultCode.Failure;
            return result;
        }
        /// <summary>
        /// 处理事项
        /// </summary>
        public Result<SettleTaskResult> SettleTask(DbSession session, Guid taskId, ETaskDealStatus dealStatus)
        {
            Result<SettleTaskResult> result = new Result<SettleTaskResult>(nameof(SettleTask));
            //TODO 
            return result;
        }
        /// <summary>
        /// 处理事项
        /// </summary>
        public Result<SettleEventResult, ETaskDealStatus> SettleEvent(DbSession session, Guid eventId, EEventDealStatus dealStatus)
        {
            Result<SettleEventResult, ETaskDealStatus> result = new Result<SettleEventResult, ETaskDealStatus>(nameof(SettleEvent));
            var @event = new TEvent() { EventId = eventId };
            result.Data1 = @event.BLSettle(session, dealStatus);
            result.ResultCode = result.Data1 == SettleEventResult.Success ? EResultCode.Success : EResultCode.Failure;
            result.Data2 = result.Data1 == SettleEventResult.Success ? @event.Task.DealStatus : ETaskDealStatus.None;
            return result;
        }
        //---------------------------------分割线,未完善的功能---------------------------------

        ///// <summary>
        ///// 删除任务相关的子任务
        ///// </summary>
        ///// <param name="session"></param>
        ///// <param name="task"></param>
        ///// <returns></returns>
        //public Result DeleteEvent(DbSession session, TTask task)
        //{
        //    Result result = new Result(nameof(DeleteEvent));
        //    result.ResultCode = task.DeleteEvents(session) ? EResultCode.Success : EResultCode.Failure;
        //    return result;
        //}
        ///// <summary>
        ///// 更新任务内容(版本控制的内容)
        ///// </summary>
        ///// <param name="session"></param>
        ///// <param name="task"></param>
        ///// <returns></returns>
        //public Result<UpdateTaskResult> UpdateVersionalContent(DbSession session, TTask task)
        //{
        //    Result<UpdateTaskResult> result = new Result<UpdateTaskResult>(nameof(UpdateVersionalContent));
        //    result.Data = task.BLUpdateVersionalContent(session);
        //    result.ResultCode = result.Data == UpdateTaskResult.Success ? EResultCode.Success : EResultCode.Failure;
        //    return result;
        //}
        /// <summary>
        /// 更新任务内容(非版本控制的内容)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public Result<UpdateTaskResult> UpdateUnversionalContent(DbSession session, TTask task)
        {
            Result<UpdateTaskResult> result = new Result<UpdateTaskResult>(nameof(UpdateUnversionalContent));
            result.Data = task.BLUpdateDescriptionalContent(session);
            result.ResultCode = result.Data == UpdateTaskResult.Success ? EResultCode.Success : EResultCode.Failure;
            return result;
        }
        ///// <summary>
        ///// 更新子任务
        ///// </summary>
        ///// <param name="session"></param>
        ///// <param name="event"></param>
        ///// <returns></returns>
        //public Result UpdateEvent(DbSession session, TEvent @event)
        //{
        //    Result result = new Result(nameof(CreateTask));
        //    result.ResultCode = @event.BLSettle(session) ? EResultCode.Success : EResultCode.Failure;
        //    return result;
        //}
    }
}
