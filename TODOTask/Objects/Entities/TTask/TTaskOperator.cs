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
        public static bool DbDelete(this TTask entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, entity.TaskId, LocateType.Equal));
            return IORMProvider.GetQueryOperator(session).Delete<TTask>(session, query);
        }
        public static bool DbDelete(this List<TTask> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            var Ids = entities.Select(c =>c.TaskId );
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, Ids, LocateType.In));
            return IORMProvider.GetQueryOperator(session).Delete<TTask>(session, query);
        }
        public static bool DbInsert(this TTask entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.TaskId, entity.TaskId));
            if (entity.Topic == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Topic));
            }
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.Topic, entity.Topic));
            if (entity.Tracing == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Tracing));
            }
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.Tracing, entity.Tracing));
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.DealStatus, entity.DealStatus));
            builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.Version, entity.Version));
            query.InsertBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Insert<TTask>(session, query);
        }
        public static bool DbInsert(this List<TTask> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.TaskId, entity.TaskId));
            if (entity.Topic == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Topic));
            }
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.Topic, entity.Topic));
            if (entity.Tracing == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Tracing));
            }
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.Tracing, entity.Tracing));
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.DealStatus, entity.DealStatus));
                builder.ComponentInsert.Values.Add(new ComponentValueOfInsert(TTaskProperties.Version, entity.Version));
                query.InsertBuilders.Add(builder);
            }
            return IORMProvider.GetQueryOperator(session).InsertAll<TTask>(session, query);
        }
        public static bool DbUpdate(this TTask entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, entity.TaskId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.TaskId, entity.TaskId));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Topic, entity.Topic));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Tracing, entity.Tracing));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.DealStatus, entity.DealStatus));
                builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Version, entity.Version));
            }
            else
            {
                if (fields.Contains(TTaskProperties.Topic))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Topic, entity.Topic));
                }
                if (fields.Contains(TTaskProperties.Tracing))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Tracing, entity.Tracing));
                }
                if (fields.Contains(TTaskProperties.DealStatus))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.DealStatus, entity.DealStatus));
                }
                if (fields.Contains(TTaskProperties.Version))
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Version, entity.Version));
                }
            }
            query.UpdateBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Update<TTask>(session, query);
        }
        public static bool DbUpdate(this List<TTask> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, entity.TaskId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.TaskId, entity.TaskId));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Topic, entity.Topic));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Tracing, entity.Tracing));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.DealStatus, entity.DealStatus));
                    builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Version, entity.Version));
                }
                else
                {
                    if (fields.Contains(TTaskProperties.Topic))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Topic, entity.Topic));
                    }
                    if (fields.Contains(TTaskProperties.Tracing))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Tracing, entity.Tracing));
                    }
                    if (fields.Contains(TTaskProperties.DealStatus))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.DealStatus, entity.DealStatus));
                    }
                    if (fields.Contains(TTaskProperties.Version))
                    {
                        builder.ComponentSet.Values.Add(new ComponentValueOfSet(TTaskProperties.Version, entity.Version));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return IORMProvider.GetQueryOperator(session).UpdateAll<TTask>(session, query);
        }
        #endregion
        #region 读
        public static TTask DbSelect(this TTask entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Values.Add(TTaskProperties.TaskId);
                builder.ComponentSelect.Values.Add(TTaskProperties.Topic);
                builder.ComponentSelect.Values.Add(TTaskProperties.Tracing);
                builder.ComponentSelect.Values.Add(TTaskProperties.DealStatus);
                builder.ComponentSelect.Values.Add(TTaskProperties.Version);
            }
            else
            {
                builder.ComponentSelect.Values.Add(TTaskProperties.TaskId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Values.Add(field);
                }
            }
            builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, entity.TaskId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Select<TTask>(session, query);
        }
        public static List<TTask> DbSelect(this List<TTask> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Values.Add(TTaskProperties.TaskId);
                builder.ComponentSelect.Values.Add(TTaskProperties.Topic);
                builder.ComponentSelect.Values.Add(TTaskProperties.Tracing);
                builder.ComponentSelect.Values.Add(TTaskProperties.DealStatus);
                builder.ComponentSelect.Values.Add(TTaskProperties.Version);
            }
            else
            {
                builder.ComponentSelect.Values.Add(TTaskProperties.TaskId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Values.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.TaskId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Wheres.Add(new ComponentValueOfWhere(TTaskProperties.TaskId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).SelectAll<TTask>(session, query);
        }
        public static void DbLoad(this TTask entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (fields.Contains(TTaskProperties.Topic))
            {
                entity.Topic = result.Topic;
            }
            if (fields.Contains(TTaskProperties.Tracing))
            {
                entity.Tracing = result.Tracing;
            }
            if (fields.Contains(TTaskProperties.DealStatus))
            {
                entity.DealStatus = result.DealStatus;
            }
            if (fields.Contains(TTaskProperties.Version))
            {
                entity.Version = result.Version;
            }
        }
        public static void DbLoad(this List<TTask> entities, DbSession session, params PDMDbProperty[] fields)
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
