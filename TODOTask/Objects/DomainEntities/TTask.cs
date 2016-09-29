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
    public enum LatestTimeStatus
    {
        None,
        OverTime,
        Started,
        Unstarted,
    }

    public partial class TTask
    {
        public int EventCount { get { return Events.Count(); } }
        public int SettledEventCount { get { return Events.Where(c=>c.DealStatus==EEventDealStatus.Settled).Count(); } }


        public string LatestTimeOfWork
        {
            get
            {
                if (latestTimeOfWork==null)
                {
                    InitLatestWork();
                }
                return latestTimeOfWork;
            }
        }

        public LatestTimeStatus LatestTimeStatus
        {
            get
            {
                if (latestTimeStatus == LatestTimeStatus.None)
                {
                    InitLatestWork();
                }
                return latestTimeStatus;
            }
        }
        private string latestTimeOfWork;
        private LatestTimeStatus latestTimeStatus;
        private void InitLatestWork()
        {
            //�����ѳ��ڵ� ��ʾ������õ�
            var overEndEvent = Events.FirstOrDefault(c => c.DealStatus != EEventDealStatus.Settled && c.WhenToEnd <= DateTime.Now);
            if (overEndEvent != null)
            {
                latestTimeStatus = LatestTimeStatus.OverTime;
                latestTimeOfWork = Events.OrderBy(c => c.WhenToEnd).First(c => c.DealStatus != EEventDealStatus.Settled && c.WhenToEnd <= DateTime.Now).WhenToEnd.ToString();
                return;
            }
            //�����ڳ��ڵĵ������ѿ�ʼ��,��ʾ��ʼʱ����õ�
            var overStartEvent = Events.FirstOrDefault(c => c.DealStatus != EEventDealStatus.Settled && c.WhenToStart <= DateTime.Now);
            if (overStartEvent != null)
            {
                latestTimeStatus = LatestTimeStatus.Started;
                latestTimeOfWork = Events.OrderBy(c => c.WhenToStart).First(c => c.DealStatus != EEventDealStatus.Settled && c.WhenToStart <= DateTime.Now).WhenToStart.ToString();
                return;
            }
            //��δ��ʼ,����ʾ��ʼʱ�������
            latestTimeStatus = LatestTimeStatus.Unstarted;
            latestTimeOfWork = Events.OrderBy(c => c.WhenToStart).First().WhenToStart.ToString();
        }



        #region Entity Subject Function
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public bool BLCreate(DbSession session, string topic)
        {
            //������
            Topic = topic;
            //�ڲ�����
            TaskId = Guid.NewGuid();
            DealStatus = ETaskDealStatus.Ready;
            Version = 1;
            Tracing = HelperOfTask.GetTracingMessage(HelperOfTask.TracingType.CreateTask);
            //���������ݿ�
            return this.DbInsert(session);
        }
        /// <summary>
        /// ��������,����������״̬���ɸ���
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public StartTaskResult BLStart(DbSession session)
        {
            var @operator = IORMProvider.GetQueryOperator(session);
            //�����¼����,����ʼ�����й������¼�
            var select = IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            select.ComponentSelect.Values.Add(new ComponentValueOfSelect("Count(*)"));
            select.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.TaskId, TaskId, LocateType.Equal));
            var result = @operator.SelectAsInt<TEvent>(session, select);
            if (result == null || result.Value <= 0)
            {
                return StartTaskResult.LackOfEvent;
            }
            //��Ҫ��Ϣ����
            this.DbLoad(session, TTaskProperties.Tracing, TTaskProperties.Version);
            //�汾�����Ϣ¼��
            Tracing += HelperOfTask.GetTracingMessage(TracingType.StartTask);
            var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, TaskId, LocateType.Equal));
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.DealStatus, ETaskDealStatus.Ready, LocateType.Equal, "OldDealStatus"));
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.Version, Version, LocateType.Equal));
            query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Version, 1, UpdateType.IncreaseByValue, "IncreaseValue"));//���Ǹ���
            query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Tracing, Tracing));//���Ǹ���
            query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.DealStatus, ETaskDealStatus.Processing, "NewDealStatus"));
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
        /// ������������(�ǰ汾���Ƶ�����)
        /// Title
        /// </summary>
        /// <param name="session"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public UpdateTaskResult BLUpdateDescriptionalContent(DbSession session, params string[] fields)
        {
            //���������ݿ�
            var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, TaskId, LocateType.Equal));
            if (fields.Contains(nameof(Topic)))
            {
                query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Topic,UpdateType.SetValue, Topic));
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
        /// ɾ������
        /// </summary>
        /// <param name="session"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public DeleteTaskResult BLDelete(DbSession session)
        {
            //��������
            this.DbSelect(session);
            //ɾ��������������¼�
            var query = IORMProvider.GetDbQueryBuilder(session).DeleteBuilder;
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.TaskId, TaskId, LocateType.Equal));
            var @operator = IORMProvider.GetQueryOperator(session);
            if (!@operator.Delete<TEvent>(session, query))
            {
                return DeleteTaskResult.DeleteEventFailed;
            }
            //�����ݿ�ɾ��
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
        /// ��ȡ������δ��ɵ��¼�
        /// </summary>
        /// <param name="session"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public int? GetUnsettledEventCount(DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            query.ComponentSelect.Values.Add(new ComponentValueOfSelect("Count(*)"));
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.TaskId, TaskId, LocateType.Equal));
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.DealStatus, EEventDealStatus.Settled, LocateType.NotEqual));
            var @operator = IORMProvider.GetQueryOperator(session);
            return @operator.SelectAsInt<TEvent>(session, query);
        }
        #endregion

        #region Entity Object Function
        #endregion

        #region Group Object Function
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public List<TTask> GetAllTasks(DbSession session)
        {
            return new List<TTask>().DbSelect(session);
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public List<TTask> GetAllTODOTasks(DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.DealStatus, new Int16[] { (Int16)ETaskDealStatus.Processing, (Int16)ETaskDealStatus.Unsettled }, LocateType.In));
            var @operator = IORMProvider.GetQueryOperator(session);
            return @operator.SelectAll<TTask>(session, query);
        }
        #endregion

        #region Inner Function
        /// <summary>
        /// ɾ��Task������Events
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        internal bool DeleteEvents(DbSession session)
        {
            //�����ݿ�ɾ��
            var query = IORMProvider.GetDbQueryBuilder(session).DeleteBuilder;
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.TaskId, TaskId, LocateType.Equal));
            var @operator = IORMProvider.GetQueryOperator(session);
            return @operator.Delete<TEvent>(session, query);
        }
        /// <summary>
        /// ��������״̬,�ڲ�����
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
            //������״̬���
            if (isEventDealStatusChanged)
            {
                //������״̬���
                if (DealStatus == ETaskDealStatus.Ready)//ִ��״̬
                {
                    return UpdateTaskResult.ReadyForStart;
                }
                //������������
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
                //������״̬���
                if (DealStatus != ETaskDealStatus.Ready)//����״̬
                {
                    return UpdateTaskResult.NotReady;
                }
            }
            //���������ݿ�
            var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, TaskId, LocateType.Equal));
            query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.Version, Version, LocateType.Equal));
            query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Version,1, UpdateType.IncreaseByValue, "IncreaseValue"));//���Ǹ���
            query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Tracing,UpdateType.SetValue, Tracing));//���Ǹ���
            if (isTaskDealStatusChanged)
            {
                query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.DealStatus, DealStatus, UpdateType.SetValue));
            }
            var @operator = IORMProvider.GetQueryOperator(session);
            var queryString = query.ToQueryString(session);
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

        //---------------------------------�ָ���,δ���ƵĹ���---------------------------------
        ///// <summary>
        ///// ������������(�汾���Ƶ�����)
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
        ///// ������������(�汾���Ƶ�����)
        ///// Tracing
        ///// </summary>
        ///// <param name="session"></param>
        ///// <param name="fields"></param>
        ///// <returns></returns>
        //internal UpdateTaskResult BLUpdateForTaskDealStatusChanged(DbSession session, TracingType tracingType, ETaskDealStatus taskDealStatus)
        //{
        //    Tracing += HelperOfTask.GetTracingMessage(tracingType);
        //    var query = IORMProvider.GetDbQueryBuilder(session).UpdateBuilder;
        //    query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, TaskId, LocateType.Equal));
        //    query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.DealStatus, LocateType.Equal, ETaskDealStatus.Ready, "OldDealStatus"));
        //    query.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.Version, Version, LocateType.Equal, "OldVersion"));
        //    query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Version, Version + 1, "NewVersion"));//���Ǹ���
        //    query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Tracing, Tracing));//���Ǹ���
        //    query.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.DealStatus, DealStatus, "NewDealStatus"));
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
