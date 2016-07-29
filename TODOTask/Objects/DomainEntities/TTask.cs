using System.Runtime.Serialization;
using System;
using VL.Common.DAS.Objects;
using TODOTask.Objects.Enums;
using VL.Common.ORM.Utilities.QueryBuilders;
using VL.Common.Protocol.IService.IORM;
using VL.Common.Protocol.IService;
using TODOTask.Objects.SubResults;
using System.Collections.Generic;
using System.Linq;
using TODOTask.Objects.Utilities;
using static TODOTask.Objects.Utilities.HelperOfTask;

namespace TODOTask.Objects.Entities
{
    public partial class TTask
    {
        #region Entity Subject Function
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public bool BLCreate(DbSession session, string topic)
        {
            //外界参数
            Topic = topic;
            //内部参数
            TaskId = Guid.NewGuid();
            DealStatus = ETaskDealStatus.Ready;
            Version = 1;
            Tracing = HelperOfTask.GetTracingMessage(HelperOfTask.TracingType.CreateTask);
            //保存入数据库
            return this.DbInsert(session);
        }
        /// <summary>
        /// 启动任务,任务启动后状态不可更改
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public StartTaskResult BLStart(DbSession session)
        {
            var @operator = IORMProvider.GetQueryOperator(session);
            //关联事件检测,任务开始必须有关联的事件
            var select = IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            select.ComponentFieldAliases.FieldAliases.Add(new FieldAlias("Count(*)"));
            select.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.TaskId, OperatorType.Equal, TaskId));
            var result = @operator.SelectAsInt<TEvent>(session, select);
            if (result == null || result.Value <= 0)
            {
                return StartTaskResult.LackOfEvent;
            }
            //必要信息加载
            this.DbLoad(session, TTaskProperties.Tracing, TTaskProperties.Version);
            //版本相关信息录入
            Tracing += HelperOfTask.GetTracingMessage(TracingType.StartTask);
            var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.Equal, TaskId));
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.DealStatus, OperatorType.Equal, ETaskDealStatus.Ready, "OldDealStatus"));
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.Version, OperatorType.Equal, Version, "OldVersion"));
            query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, Version + 1, "NewVersion"));//总是更新
            query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, Tracing));//总是更新
            query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, ETaskDealStatus.Processing, "NewDealStatus"));
            if (@operator.Update<TTask>(session, query))
            {
                return StartTaskResult.Success;
            }
            else
            {
                return StartTaskResult.Failure;
            }
        }
        /// <summary>
        /// 更新任务内容(非版本控制的内容)
        /// Title
        /// </summary>
        /// <param name="session"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public UpdateTaskResult BLUpdateDescriptionalContent(DbSession session, params string[] fields)
        {
            //保存入数据库
            var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.Equal, TaskId));
            if (fields.Contains(nameof(Topic)))
            {
                query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Topic, Topic));
            }
            var @operator = IORMProvider.GetQueryOperator(session);
            if (@operator.Update<TTask>(session, query))
            {
                return UpdateTaskResult.Success;
            }
            else
            {
                return UpdateTaskResult.Failure;
            }
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="session"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public DeleteTaskResult BLDelete(DbSession session)
        {
            //加载数据
            this.DbSelect(session);
            //删除与任务关联的事件
            var query = IORMProvider.GetDbQueryBuilder(session).DeleteBuilder;
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.TaskId, OperatorType.Equal, TaskId));
            var @operator = IORMProvider.GetQueryOperator(session);
            if (!@operator.Delete<TEvent>(session, query))
            {
                return DeleteTaskResult.DeleteEventFailed;
            }
            //从数据库删除
            if (this.DbDelete(session))
            {
                return DeleteTaskResult.Success;
            }
            else
            {
                return DeleteTaskResult.Failure;
            }
        }
        /// <summary>
        /// 获取任务下未完成的事件
        /// </summary>
        /// <param name="session"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public int? GetUnsettledEventCount(DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            query.ComponentFieldAliases.FieldAliases.Add(new FieldAlias("Count(*)"));
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.TaskId, OperatorType.Equal, TaskId));
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.DealStatus, OperatorType.NotEqual, EEventDealStatus.Settled));
            var @operator = IORMProvider.GetQueryOperator(session);
            return @operator.SelectAsInt<TEvent>(session, query);
        }
        #endregion

        #region Entity Object Function
        #endregion

        #region Group Object Function
        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public List<TTask> GetAllTasks(DbSession session)
        {
            return new List<TTask>().DbSelect(session);
        }
        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public List<TTask> GetAllTODOTasks(DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.DealStatus, OperatorType.In, new Int16[] { (Int16)ETaskDealStatus.Processing, (Int16)ETaskDealStatus.Unsettled }));
            var @operator = IORMProvider.GetQueryOperator(session);
            return @operator.SelectAll<TTask>(session, query);
        }
        #endregion

        #region Inner Function
        /// <summary>
        /// 删除Task关联的Events
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        internal bool DeleteEvents(DbSession session)
        {
            //从数据库删除
            var query = IORMProvider.GetDbQueryBuilder(session).DeleteBuilder;
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.TaskId, OperatorType.Equal, TaskId));
            var @operator = IORMProvider.GetQueryOperator(session);
            return @operator.Delete<TEvent>(session, query);
        }
        /// <summary>
        /// 更新任务状态,内部机制
        /// </summary>
        /// <param name="session"></param>
        /// <param name="eventChange"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal UpdateTaskResult UpdateVersionalContent(DbSession session, EEventChange eventChange, string message)
        {
            bool isEventDealStatusChanged = false;
            bool isTaskDealStatusChanged = false;
            switch (eventChange)
            {
                case EEventChange.AddEvent:
                    Tracing += HelperOfTask.GetTracingMessage(TracingType.AddEvent, message);
                    break;
                case EEventChange.RemoveEvent:
                    Tracing += HelperOfTask.GetTracingMessage(TracingType.RemoveEvent, message);
                    break;
                case EEventChange.ChangeEventDealStatus:
                    Tracing += HelperOfTask.GetTracingMessage(TracingType.ChangeEventDealStatus, message);
                    isEventDealStatusChanged = true;
                    break;
                default:
                    return UpdateTaskResult.None;
            }
            //主任务状态检测
            if (isEventDealStatusChanged)
            {
                //主任务状态检测
                if (DealStatus == ETaskDealStatus.Ready)//执行状态
                {
                    return UpdateTaskResult.ReadyForStart;
                }
                //关联子任务检测
                if (new TTask() { TaskId = TaskId }.GetUnsettledEventCount(session) > 0)
                {
                    if (DealStatus == ETaskDealStatus.Settled)
                    {
                        DealStatus = ETaskDealStatus.Processing;
                        isTaskDealStatusChanged = true;
                        Tracing += HelperOfTask.GetTracingMessage(HelperOfTask.TracingType.RestartTask, "");
                    }
                }
                else
                {
                    DealStatus = ETaskDealStatus.Settled;
                    isTaskDealStatusChanged = true;
                    Tracing += HelperOfTask.GetTracingMessage(HelperOfTask.TracingType.SettleTask, "");
                }
            }
            else
            {
                //主任务状态检测
                if (DealStatus != ETaskDealStatus.Ready)//待机状态
                {
                    return UpdateTaskResult.NotReady;
                }
            }
            //保存入数据库
            var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.Equal, TaskId));
            query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.Version, OperatorType.Equal, Version, "OldVersion"));
            query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, Version + 1, "NewVersion"));//总是更新
            query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, Tracing));//总是更新
            if (isTaskDealStatusChanged)
            {
                query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, DealStatus));
            }
            var @operator = IORMProvider.GetQueryOperator(session);
            if (@operator.Update<TTask>(session, query))
            {
                return UpdateTaskResult.Success;
            }
            else
            {
                return UpdateTaskResult.Failure;
            }
        }
        #endregion

        //---------------------------------分割线,未完善的功能---------------------------------
        ///// <summary>
        ///// 更新任务内容(版本控制的内容)
        ///// Tracing
        ///// </summary>
        ///// <param name="session"></param>
        ///// <param name="fields"></param>
        ///// <returns></returns>
        //internal UpdateTaskResult BLUpdateForEventChanged(DbSession session, TracingType tracingType, string message)
        //{
        //    Tracing += HelperOfTask.GetTracingMessage(tracingType, message);
        //    switch (tracingType)
        //    {
        //        case TracingType.AddEvent:
        //        case TracingType.RemoveEvent:
        //            return BLUpdateVersionalContent(session, false, ETaskDealStatus.Ready);
        //        case TracingType.ChangeEventDealStatus:
        //            return BLUpdateVersionalContent(session, true, ETaskDealStatus.Ready);
        //        default:
        //            return UpdateTaskResult.None;
        //    }
        //}
        ///// <summary>
        ///// 更新任务内容(版本控制的内容)
        ///// Tracing
        ///// </summary>
        ///// <param name="session"></param>
        ///// <param name="fields"></param>
        ///// <returns></returns>
        //internal UpdateTaskResult BLUpdateForTaskDealStatusChanged(DbSession session, TracingType tracingType, ETaskDealStatus taskDealStatus)
        //{
        //    Tracing += HelperOfTask.GetTracingMessage(tracingType);
        //    var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
        //    query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.Equal, TaskId));
        //    query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.DealStatus, OperatorType.Equal, ETaskDealStatus.Ready, "OldDealStatus"));
        //    query.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.Version, OperatorType.Equal, Version, "OldVersion"));
        //    query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, Version + 1, "NewVersion"));//总是更新
        //    query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, Tracing));//总是更新
        //    query.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, DealStatus, "NewDealStatus"));
        //    var @operator = IORMProvider.GetQueryOperator(session);
        //    if (@operator.Update<TTask>(session, query))
        //    {
        //        return UpdateTaskResult.Success;
        //    }
        //    else
        //    {
        //        return UpdateTaskResult.Failure;
        //    }
        //}

    }
}
