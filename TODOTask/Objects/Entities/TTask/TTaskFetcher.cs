using System;
using System.Collections.Generic;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;
using VL.Common.Protocol.IService.IORM;

namespace TODOTask.Objects.Entities
{
    public static partial class EntityFetcher
    {
        #region Methods
        /// <summary>
        /// return false if fetch.Count()==0
        /// return true if fetch.Count()>0
        /// </summary>
        public static bool FetchEvents(this TTask tTask, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.TaskId, tTask.TaskId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            tTask.Events = IORMProvider.GetQueryOperator(session).SelectAll<TEvent>(session, query);
            return tTask.Events.Count > 0;
        }
        #endregion
    }
}
