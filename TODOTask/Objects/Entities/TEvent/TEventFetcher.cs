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
        public static bool FetchTTask(this TEvent tEvent, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (tEvent.TaskId == Guid.Empty)
            {
                var subselect = new SelectBuilder();
                subselect.TableName = nameof(TEvent);
                subselect.ComponentSelect.Values.Add(TEventProperties.TaskId);
                subselect.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.EventId, tEvent.EventId, LocateType.Equal));
                builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, subselect, LocateType.Equal));
            }
            else
            {
                builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, tEvent.TaskId, LocateType.Equal));
            }
            query.SelectBuilders.Add(builder);
            tEvent.Task = IORMProvider.GetQueryOperator(session).Select<TTask>(session, query);
            if (tEvent.Task == null)
            {
                throw new NotImplementedException(string.Format("1..* 关联未查询到匹配数据, Parent:{0}; Child: {1}", nameof(TEvent), nameof(TTask)));
            }
            return true;
        }
        #endregion
    }
}
