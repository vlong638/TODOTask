using System.Runtime.Serialization;
using System;
using VL.Common.DAS.Objects;
using TODOTask.Objects.Enums;
using VL.Common.ORM.Utilities.QueryBuilders;
using VL.Common.Protocol.IService.IORM;
using VL.Common.Protocol.IService;
using TODOTask.Objects.SubResults;
using TODOTask.Objects.Utilities;
using static TODOTask.Objects.Utilities.HelperOfTask;
using System.Collections.Generic;

namespace TODOTask.Objects.Entities
{
    public partial class TEvent
    {
        #region Entity Subject Function
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="session"></param>
        /// <param name="taskId"></param>
        /// <param name="participant"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public CreateEventResult BLCreate(DbSession session, Guid taskId, string participant, string topic)
        {
            //动态参数
            TaskId = taskId;
            Participant = participant;
            Topic = topic;
            //内置参数
            EventId = Guid.NewGuid();
            Version = 1;
            DealStatus = EEventDealStatus.Ready;
            //任务级联影响
            if (!this.FetchTTask(session))
            {
                return CreateEventResult.FetchTaskFailed;
            }
            var updateStatus = Task.UpdateVersionalContent(session, EEventChange.AddEvent, Topic);
            if (updateStatus != UpdateTaskResult.Success)
            {
                switch (updateStatus)
                {
                    case UpdateTaskResult.Failure:
                        return CreateEventResult.UpdateTaskResult_Failure;
                    case UpdateTaskResult.NotReady:
                        return CreateEventResult.UpdateTaskResult_NotReady;
                    case UpdateTaskResult.ReadyForStart:
                        return CreateEventResult.UpdateTaskResult_NotProcessing;
                    default:
                        return CreateEventResult.None;
                }
            }
            //数据库操作
            if (!this.DbInsert(session))
            {
                return CreateEventResult.Failure;
            }
            return CreateEventResult.Success;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="session"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public DeleteEventResult BLDelete(DbSession session)
        {
            //必要参数
            this.DbLoad(session, TEventProperties.TaskId, TEventProperties.Topic);
            //任务级联影响
            if (!this.FetchTTask(session))
            {
                return DeleteEventResult.FetchTaskFailed;
            }
            var updateStatus = Task.UpdateVersionalContent(session, EEventChange.RemoveEvent, Topic);
            if (updateStatus != UpdateTaskResult.Success)
            {
                switch (updateStatus)
                {
                    case UpdateTaskResult.Failure:
                        return DeleteEventResult.UpdateTaskResult_Failure;
                    case UpdateTaskResult.NotReady:
                        return DeleteEventResult.UpdateTaskResult_NotReady;
                    case UpdateTaskResult.ReadyForStart:
                        return DeleteEventResult.UpdateTaskResult_NotProcessing;
                    default:
                        return DeleteEventResult.None;
                }
            }
            //从数据库删除
            if (!this.DbDelete(session))
            {
                return DeleteEventResult.Failure;
            }
            return DeleteEventResult.Success;
        }
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public SettleEventResult BLSettle(DbSession session, EEventDealStatus dealStatus)
        {
            //状态检测
            if (DealStatus==EEventDealStatus.Settled)
            {
                return SettleEventResult.AllreadySettled;
            }
            //必要参数
            this.DbLoad(session, TEventProperties.TaskId, TEventProperties.Topic, TEventProperties.Version);
            //动态参数
            this.DealStatus = dealStatus;
            //任务级联影响
            if (!this.FetchTTask(session))
            {
                return SettleEventResult.FetchTaskFailed;
            }
            //更新事件状态
            var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.EventId, EventId, LocateType.Equal));
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.Version, Version, LocateType.Equal, "OldVersion"));
            query.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.DealStatus, DealStatus));
            query.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Version, 1, UpdateType.IncreaseByValue, "IncreaseVersion"));
            var @operator = IORMProvider.GetQueryOperator(session);
            if (!@operator.Update<TEvent>(session, query))
            {
                return SettleEventResult.Failure;
            }
            //任务级联影响
            var updateStatus = Task.UpdateVersionalContent(session, EEventChange.ChangeEventDealStatus, Topic);
            if (updateStatus != UpdateTaskResult.Success)
            {
                switch (updateStatus)
                {
                    case UpdateTaskResult.Failure:
                        return SettleEventResult.UpdateTaskResult_Failure;
                    case UpdateTaskResult.NotReady:
                        return SettleEventResult.UpdateTaskResult_NotReady;
                    case UpdateTaskResult.ReadyForStart:
                        return SettleEventResult.UpdateTaskResult_NotProcessing;
                    default:
                        return SettleEventResult.None;
                }
            }
            return SettleEventResult.Success;
        }
        #endregion

        #region Entity Object Function
        #endregion

        #region Group Object Function
        /// <summary>
        /// 获取所有事项
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public List<TEvent> GetAllEvents(DbSession session)
        {
            return new List<TEvent>().DbSelect(session);
        }
        #endregion

        #region Inner Function
        #endregion

        //---------------------------------分割线,未完善的功能---------------------------------

    }
}
