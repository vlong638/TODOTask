using System;
using TODOTask.Objects.DomainFacades;
using TODOTask.Objects.Enums;
using TODOTask.ServiceUtilities;
using VL.Common.DAS.Objects;
using VL.Common.Logger.Utilities;
using VL.Common.Protocol;
using VL.Common.Protocol.IService;

namespace TODOTask.Runner
{
    class Program
    {
        static ServiceContextOfTODOTask ServiceContext { set; get; }
        static DependencyResult DependencyResult { set; get; }
        static DependencyResult InitServiceContext()
        {
            try
            {
                if (DependencyResult == null)
                {
                    ServiceContext = new ServiceContextOfTODOTask(
                        new DbConfigOfTODOTask("DbConnections.config"),
                        new ProtocolConfig("ProtocolConfig.config"),
                        LoggerProvider.GetLog4netLogger("ServiceLog"));
                }
                DependencyResult = ServiceContext.Init();
            }
            catch (Exception ex)
            {
                LoggerProvider.GetLog4netLogger("ServiceLog").Error(ex.ToString());
            }
            return DependencyResult;
        }

        static void Main(string[] args)
        {


            var result = InitServiceContext();
            if (!result.IsAllDependenciesAvailable)
                return;

            using (DbSession session = ServiceContext.GetDbSession(DbConfigOfTODOTask.DbNameOfTODOTask))
            {

                session.Open();

                User user = new User() { Name = "vlong638" };

                #region 业务逻辑(带状态校验)
                if (false)
                {
                    user.CreateTask(session, "1.玩法开奖");
                    var tasks = user.GetAllTasks(session);
                    foreach (var task in tasks.Data)
                    {
                        user.CreateEvent(session, task.TaskId, "1.1.彩果和中奖信息录入");
                        user.CreateEvent(session, task.TaskId, "1.2.走势确认");
                        var events = user.GetAllEvents(session);
                        foreach (var @event in events.Data)
                        {
                            user.SettleEvent(session, @event.EventId, EEventDealStatus.Settled);
                        }
                        foreach (var @event in events.Data)
                        {
                            user.DeleteEvent(session, @event.EventId);
                        }
                        user.CreateEvent(session, task.TaskId, "1.1.彩果和中奖信息录入");
                        user.CreateEvent(session, task.TaskId, "1.2.走势确认");
                        user.StartTask(session, task.TaskId);
                        events = user.GetAllEvents(session);
                        foreach (var @event in events.Data)
                        {
                            user.SettleEvent(session, @event.EventId, EEventDealStatus.Settled);
                        }
                        foreach (var @event in events.Data)
                        {
                            user.DeleteEvent(session, @event.EventId);
                        }
                    } 
                }
                #endregion

                #region 业务逻辑(规范流程)
                if (true)
                {
                    user.CreateTask(session, "1.玩法开奖1");
                    user.CreateTask(session, "1.玩法开奖2");
                    user.CreateTask(session, "1.玩法开奖3");
                    var tasks = user.GetAllTasks(session);
                    foreach (var task in tasks.Data)
                    {
                        user.CreateEvent(session, task.TaskId, "1.1.彩果和中奖信息录入");
                        user.CreateEvent(session, task.TaskId, "1.2.走势确认");
                        user.StartTask(session, task.TaskId);
                        var events = user.GetAllEvents(session);
                        foreach (var @event in events.Data)
                        {
                            user.SettleEvent(session, @event.EventId, EEventDealStatus.Settled);
                        }
                    } 
                }
                #endregion

                session.Close();
            }
        }
    }
}
