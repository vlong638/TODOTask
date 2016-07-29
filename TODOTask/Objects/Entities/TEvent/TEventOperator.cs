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
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.EventId, OperatorType.Equal, entity.EventId));
            return IORMProvider.GetQueryOperator(session).Delete<TEvent>(session, query);
        }
        public static bool DbDelete(this List<TEvent> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            var Ids = entities.Select(c =>c.EventId );
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.EventId, OperatorType.In, Ids));
            return IORMProvider.GetQueryOperator(session).Delete<TEvent>(session, query);
        }
        public static bool DbInsert(this TEvent entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.TaskId, entity.TaskId));
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.EventId, entity.EventId));
            if (entity.Topic == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Topic));
            }
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Topic, entity.Topic));
            if (entity.Participant == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Participant));
            }
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Participant, entity.Participant));
            if (entity.WhenToStart.HasValue)
            {
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToStart, entity.WhenToStart.Value));
            }
            if (entity.WhenToEnd.HasValue)
            {
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToEnd, entity.WhenToEnd.Value));
            }
            if (entity.WhenStart.HasValue)
            {
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenStart, entity.WhenStart.Value));
            }
            if (entity.WhenEnd.HasValue)
            {
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenEnd, entity.WhenEnd.Value));
            }
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.DealStatus, entity.DealStatus));
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Version, entity.Version));
            query.InsertBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Insert<TEvent>(session, query);
        }
        public static bool DbInsert(this List<TEvent> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.TaskId, entity.TaskId));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.EventId, entity.EventId));
            if (entity.Topic == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Topic));
            }
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Topic, entity.Topic));
            if (entity.Participant == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Participant));
            }
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Participant, entity.Participant));
                if (entity.WhenToStart.HasValue)
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToStart, entity.WhenToStart.Value));
                }
                if (entity.WhenToEnd.HasValue)
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToEnd, entity.WhenToEnd.Value));
                }
                if (entity.WhenStart.HasValue)
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenStart, entity.WhenStart.Value));
                }
                if (entity.WhenEnd.HasValue)
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenEnd, entity.WhenEnd.Value));
                }
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.DealStatus, entity.DealStatus));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Version, entity.Version));
                query.InsertBuilders.Add(builder);
            }
            return IORMProvider.GetQueryOperator(session).InsertAll<TEvent>(session, query);
        }
        public static bool DbUpdate(this TEvent entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.EventId, OperatorType.Equal, entity.EventId));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.TaskId, entity.TaskId));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.EventId, entity.EventId));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Topic, entity.Topic));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Participant, entity.Participant));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToStart, entity.WhenToStart));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToEnd, entity.WhenToEnd));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenStart, entity.WhenStart));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenEnd, entity.WhenEnd));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.DealStatus, entity.DealStatus));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Version, entity.Version));
            }
            else
            {
                if (fields.Contains(TEventProperties.TaskId))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.TaskId, entity.TaskId));
                }
                if (fields.Contains(TEventProperties.Topic))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Topic, entity.Topic));
                }
                if (fields.Contains(TEventProperties.Participant))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Participant, entity.Participant));
                }
                if (fields.Contains(TEventProperties.WhenToStart))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToStart, entity.WhenToStart));
                }
                if (fields.Contains(TEventProperties.WhenToEnd))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToEnd, entity.WhenToEnd));
                }
                if (fields.Contains(TEventProperties.WhenStart))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenStart, entity.WhenStart));
                }
                if (fields.Contains(TEventProperties.WhenEnd))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenEnd, entity.WhenEnd));
                }
                if (fields.Contains(TEventProperties.DealStatus))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.DealStatus, entity.DealStatus));
                }
                if (fields.Contains(TEventProperties.Version))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Version, entity.Version));
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
                builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.EventId, OperatorType.Equal, entity.EventId));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.TaskId, entity.TaskId));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.EventId, entity.EventId));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Topic, entity.Topic));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Participant, entity.Participant));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToStart, entity.WhenToStart));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToEnd, entity.WhenToEnd));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenStart, entity.WhenStart));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenEnd, entity.WhenEnd));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.DealStatus, entity.DealStatus));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Version, entity.Version));
                }
                else
                {
                    if (fields.Contains(TEventProperties.TaskId))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.TaskId, entity.TaskId));
                    }
                    if (fields.Contains(TEventProperties.Topic))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Topic, entity.Topic));
                    }
                    if (fields.Contains(TEventProperties.Participant))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Participant, entity.Participant));
                    }
                    if (fields.Contains(TEventProperties.WhenToStart))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToStart, entity.WhenToStart));
                    }
                    if (fields.Contains(TEventProperties.WhenToEnd))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenToEnd, entity.WhenToEnd));
                    }
                    if (fields.Contains(TEventProperties.WhenStart))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenStart, entity.WhenStart));
                    }
                    if (fields.Contains(TEventProperties.WhenEnd))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.WhenEnd, entity.WhenEnd));
                    }
                    if (fields.Contains(TEventProperties.DealStatus))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.DealStatus, entity.DealStatus));
                    }
                    if (fields.Contains(TEventProperties.Version))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TEventProperties.Version, entity.Version));
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
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.TaskId);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.EventId);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.Topic);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.Participant);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.WhenToStart);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.WhenToEnd);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.WhenStart);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.WhenEnd);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.DealStatus);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.Version);
            }
            else
            {
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.EventId);
                foreach (var field in fields)
                {
                    builder.ComponentFieldAliases.FieldAliases.Add(field);
                }
            }
            builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.EventId, OperatorType.Equal, entity.EventId));
            query.SelectBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Select<TEvent>(session, query);
        }
        public static List<TEvent> DbSelect(this List<TEvent> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.TaskId);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.EventId);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.Topic);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.Participant);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.WhenToStart);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.WhenToEnd);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.WhenStart);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.WhenEnd);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.DealStatus);
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.Version);
            }
            else
            {
                builder.ComponentFieldAliases.FieldAliases.Add(TEventProperties.EventId);
                foreach (var field in fields)
                {
                    builder.ComponentFieldAliases.FieldAliases.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.EventId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TEventProperties.EventId, OperatorType.In, Ids));
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
