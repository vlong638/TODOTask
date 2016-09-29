using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TODOTask.Objects.DomainFacades;
using TODOTask.Objects.Entities;
using TODOTask.Objects.Enums;
using TODOTask.Objects.SubResults;
using TODOTask.ServiceUtilities;
using VL.Common.DAS.Objects;
using VL.Common.Logger.Utilities;
using VL.Common.Protocol;
using VL.Common.Protocol.IService;

namespace TODOTask.Manager.Controllers
{
    public class TaskController : Controller
    {
        static string PartialPageName = "TaskPartial";
        static ServiceContextOfTODOTask _serviceContext { set; get; }
        static object serviceLock = new object();
        static ServiceContextOfTODOTask ServiceContext
        {
            set { _serviceContext = value; }
            get
            {
                if (_serviceContext == null)
                {
                    lock (serviceLock)
                    {
                        if (_serviceContext == null)
                        {
                            try
                            {
                                _serviceContext = new ServiceContextOfTODOTask(
                                    new DbConfigOfTODOTask("DbConnections.config"),
                                    new ProtocolConfig("ProtocolConfig.config"),
                                    LoggerProvider.GetLog4netLogger("ServiceLog"));
                                DependencyResult = ServiceContext.Init();
                            }
                            catch (Exception ex)
                            {
                                LoggerProvider.GetLog4netLogger("ServiceLog").Error(ex.ToString());
                            }
                        }
                    }
                }
                return _serviceContext;
            }
        }
        static DependencyResult DependencyResult { set; get; }
        static User User { set; get; } = new User() { Name = "vlong638" };

        #region Completed
        // GET: Task
        public ActionResult Index()
        {
            if (ServiceContext!=null)
            {
                return View(DependencyResult);
            }
            else
            {
                return View(new DependencyResult() { IsAllDependenciesAvailable = false });
            }
        }
        // GET: TaskList
        public ActionResult List()
        {
            Result<List<TTask>> result = ServiceContext.ServiceDelegator.HandleSimpleTransactionEvent(DbConfigOfTODOTask.DbNameOfTODOTask, (session) =>
            {
                return User.GetAllTasksWithEvents(session);
            });
            return View(result.Data);
        }
        // GET: TaskList
        public ActionResult TODOList()
        {
            Result<List<TTask>> result = ServiceContext.ServiceDelegator.HandleSimpleTransactionEvent(DbConfigOfTODOTask.DbNameOfTODOTask, (session) =>
            {
                return User.GetAllTODOTasksWithEvents(session);
            });
            return View(result.Data);
        }
        [HttpPost]
        public string SettleTask(Guid taskId, ETaskDealStatus taskStatus)
        {
            //Result<SettleTaskResult> result = ServiceContext.ServiceDelegator.HandleSimpleTransactionEvent(DbConfigOfTODOTask.DbNameOfTODOTask, (session) =>
            //{
            //    return User.SettleTask(session, taskId, taskStatus == ETaskDealStatus.Settled ? ETaskDealStatus.Unsettled : ETaskDealStatus.Settled);
            //});
            return "TODO";
        }
        [HttpPost]
        public string SettleEvent(Guid eventId, EEventDealStatus eventStatus)
        {
            Result<SettleEventResult, ETaskDealStatus> result = ServiceContext.ServiceDelegator.HandleSimpleTransactionEvent(DbConfigOfTODOTask.DbNameOfTODOTask, (session) =>
              {
                  return User.SettleEvent(session, eventId, eventStatus == EEventDealStatus.Settled ? EEventDealStatus.Unsettled : EEventDealStatus.Settled);
              });
            return ((eventStatus == EEventDealStatus.Settled && result.Data1 == Objects.SubResults.SettleEventResult.Success) ? EEventDealStatus.Unsettled.ToString()+",-1" : EEventDealStatus.Settled.ToString() + ",1")
                + "," + result.Data2;
        }
        #endregion

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
