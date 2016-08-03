using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;
using VL.Common.Protocol.IService.IORM;

namespace TODOTask.Objects.Entities
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TEvent entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.EventId, entity.EventId, LocateType.Equal));
            return IORMProvider.GetQueryOperator(session).Delete<TEvent>(session, query);
        }
        public static bool DbDelete(this List<TEvent> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            var Ids = entities.Select(c =>c.EventId );
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.EventId, Ids, LocateType.In));
            return IORMProvider.GetQueryOperator(session).Delete<TEvent>(session, query);
        }
        public static bool DbInsert(this TEvent entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.TaskId, entity.TaskId));
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.EventId, entity.EventId));
            if (entity.Topic == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Topic));
            }
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.Topic, entity.Topic));
            if (entity.Participant == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Participant));
            }
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.Participant, entity.Participant));
            if (entity.WhenToStart.HasValue)
            {
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.WhenToStart, entity.WhenToStart.Value));
            }
            if (entity.WhenToEnd.HasValue)
            {
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.WhenToEnd, entity.WhenToEnd.Value));
            }
            if (entity.WhenStart.HasValue)
            {
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.WhenStart, entity.WhenStart.Value));
            }
            if (entity.WhenEnd.HasValue)
            {
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.WhenEnd, entity.WhenEnd.Value));
            }
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.DealStatus, entity.DealStatus));
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.Version, entity.Version));
            query.InsertBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Insert<TEvent>(session, query);
        }
        public static bool DbInsert(this List<TEvent> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.TaskId, entity.TaskId));
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.EventId, entity.EventId));
            if (entity.Topic == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Topic));
            }
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.Topic, entity.Topic));
            if (entity.Participant == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Participant));
            }
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.Participant, entity.Participant));
                if (entity.WhenToStart.HasValue)
                {
                    builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.WhenToStart, entity.WhenToStart.Value));
                }
                if (entity.WhenToEnd.HasValue)
                {
                    builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.WhenToEnd, entity.WhenToEnd.Value));
                }
                if (entity.WhenStart.HasValue)
                {
                    builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.WhenStart, entity.WhenStart.Value));
                }
                if (entity.WhenEnd.HasValue)
                {
                    builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.WhenEnd, entity.WhenEnd.Value));
                }
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.DealStatus, entity.DealStatus));
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TEventProperties.Version, entity.Version));
                query.InsertBuilders.Add(builder);
            }
            return IORMProvider.GetQueryOperator(session).InsertAll<TEvent>(session, query);
        }
        public static bool DbUpdate(this TEvent entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.EventId, entity.EventId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.TaskId, entity.TaskId));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.EventId, entity.EventId));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Topic, entity.Topic));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Participant, entity.Participant));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenToStart, entity.WhenToStart));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenToEnd, entity.WhenToEnd));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenStart, entity.WhenStart));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenEnd, entity.WhenEnd));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.DealStatus, entity.DealStatus));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Version, entity.Version));
            }
            else
            {
                if (fields.Contains(TEventProperties.TaskId))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.TaskId, entity.TaskId));
                }
                if (fields.Contains(TEventProperties.Topic))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Topic, entity.Topic));
                }
                if (fields.Contains(TEventProperties.Participant))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Participant, entity.Participant));
                }
                if (fields.Contains(TEventProperties.WhenToStart))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenToStart, entity.WhenToStart));
                }
                if (fields.Contains(TEventProperties.WhenToEnd))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenToEnd, entity.WhenToEnd));
                }
                if (fields.Contains(TEventProperties.WhenStart))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenStart, entity.WhenStart));
                }
                if (fields.Contains(TEventProperties.WhenEnd))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenEnd, entity.WhenEnd));
                }
                if (fields.Contains(TEventProperties.DealStatus))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.DealStatus, entity.DealStatus));
                }
                if (fields.Contains(TEventProperties.Version))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Version, entity.Version));
                }
            }
            query.UpdateBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Update<TEvent>(session, query);
        }
        public static bool DbUpdate(this List<TEvent> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.EventId, entity.EventId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.TaskId, entity.TaskId));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.EventId, entity.EventId));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Topic, entity.Topic));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Participant, entity.Participant));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenToStart, entity.WhenToStart));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenToEnd, entity.WhenToEnd));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenStart, entity.WhenStart));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenEnd, entity.WhenEnd));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.DealStatus, entity.DealStatus));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Version, entity.Version));
                }
                else
                {
                    if (fields.Contains(TEventProperties.TaskId))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.TaskId, entity.TaskId));
                    }
                    if (fields.Contains(TEventProperties.Topic))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Topic, entity.Topic));
                    }
                    if (fields.Contains(TEventProperties.Participant))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Participant, entity.Participant));
                    }
                    if (fields.Contains(TEventProperties.WhenToStart))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenToStart, entity.WhenToStart));
                    }
                    if (fields.Contains(TEventProperties.WhenToEnd))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenToEnd, entity.WhenToEnd));
                    }
                    if (fields.Contains(TEventProperties.WhenStart))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenStart, entity.WhenStart));
                    }
                    if (fields.Contains(TEventProperties.WhenEnd))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.WhenEnd, entity.WhenEnd));
                    }
                    if (fields.Contains(TEventProperties.DealStatus))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.DealStatus, entity.DealStatus));
                    }
                    if (fields.Contains(TEventProperties.Version))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TEventProperties.Version, entity.Version));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return IORMProvider.GetQueryOperator(session).UpdateAll<TEvent>(session, query);
        }
        #endregion
        #region 读
        public static TEvent DbSelect(this TEvent entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Values.Add(TEventProperties.TaskId);
                builder.ComponentSelect.Values.Add(TEventProperties.EventId);
                builder.ComponentSelect.Values.Add(TEventProperties.Topic);
                builder.ComponentSelect.Values.Add(TEventProperties.Participant);
                builder.ComponentSelect.Values.Add(TEventProperties.WhenToStart);
                builder.ComponentSelect.Values.Add(TEventProperties.WhenToEnd);
                builder.ComponentSelect.Values.Add(TEventProperties.WhenStart);
                builder.ComponentSelect.Values.Add(TEventProperties.WhenEnd);
                builder.ComponentSelect.Values.Add(TEventProperties.DealStatus);
                builder.ComponentSelect.Values.Add(TEventProperties.Version);
            }
            else
            {
                builder.ComponentSelect.Values.Add(TEventProperties.EventId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Values.Add(field);
                }
            }
            builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.EventId, entity.EventId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Select<TEvent>(session, query);
        }
        public static List<TEvent> DbSelect(this List<TEvent> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Values.Add(TEventProperties.TaskId);
                builder.ComponentSelect.Values.Add(TEventProperties.EventId);
                builder.ComponentSelect.Values.Add(TEventProperties.Topic);
                builder.ComponentSelect.Values.Add(TEventProperties.Participant);
                builder.ComponentSelect.Values.Add(TEventProperties.WhenToStart);
                builder.ComponentSelect.Values.Add(TEventProperties.WhenToEnd);
                builder.ComponentSelect.Values.Add(TEventProperties.WhenStart);
                builder.ComponentSelect.Values.Add(TEventProperties.WhenEnd);
                builder.ComponentSelect.Values.Add(TEventProperties.DealStatus);
                builder.ComponentSelect.Values.Add(TEventProperties.Version);
            }
            else
            {
                builder.ComponentSelect.Values.Add(TEventProperties.EventId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Values.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.EventId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TEventProperties.EventId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).SelectAll<TEvent>(session, query);
        }
        public static void DbLoad(this TEvent entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (fields.Contains(TEventProperties.TaskId))
            {
                entity.TaskId = result.TaskId;
            }
            if (fields.Contains(TEventProperties.Topic))
            {
                entity.Topic = result.Topic;
            }
            if (fields.Contains(TEventProperties.Participant))
            {
                entity.Participant = result.Participant;
            }
            if (fields.Contains(TEventProperties.WhenToStart))
            {
                entity.WhenToStart = result.WhenToStart;
            }
            if (fields.Contains(TEventProperties.WhenToEnd))
            {
                entity.WhenToEnd = result.WhenToEnd;
            }
            if (fields.Contains(TEventProperties.WhenStart))
            {
                entity.WhenStart = result.WhenStart;
            }
            if (fields.Contains(TEventProperties.WhenEnd))
            {
                entity.WhenEnd = result.WhenEnd;
            }
            if (fields.Contains(TEventProperties.DealStatus))
            {
                entity.DealStatus = result.DealStatus;
            }
            if (fields.Contains(TEventProperties.Version))
            {
                entity.Version = result.Version;
            }
        }
        public static void DbLoad(this List<TEvent> entities, DbSession session, params PDMDbProperty[] fields)
        {
            foreach (var entity in entities)
            {
                entity.DbLoad(session, fields);
            }
        }
        #endregion
        #endregion
    }
}
