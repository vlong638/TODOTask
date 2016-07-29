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
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.Equal, entity.TaskId));
            return IORMProvider.GetQueryOperator(session).Delete<TTask>(session, query);
        }
        public static bool DbDelete(this List<TTask> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            var Ids = entities.Select(c =>c.TaskId );
            query.DeleteBuilder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.In, Ids));
            return IORMProvider.GetQueryOperator(session).Delete<TTask>(session, query);
        }
        public static bool DbInsert(this TTask entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.TaskId, entity.TaskId));
            if (entity.Topic == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Topic));
            }
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Topic, entity.Topic));
            if (entity.Tracing == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Tracing));
            }
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, entity.Tracing));
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, entity.DealStatus));
            builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, entity.Version));
            query.InsertBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Insert<TTask>(session, query);
        }
        public static bool DbInsert(this List<TTask> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.TaskId, entity.TaskId));
            if (entity.Topic == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Topic));
            }
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Topic, entity.Topic));
            if (entity.Tracing == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Tracing));
            }
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, entity.Tracing));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, entity.DealStatus));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, entity.Version));
                query.InsertBuilders.Add(builder);
            }
            return IORMProvider.GetQueryOperator(session).InsertAll<TTask>(session, query);
        }
        public static bool DbUpdate(this TTask entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.Equal, entity.TaskId));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.TaskId, entity.TaskId));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Topic, entity.Topic));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, entity.Tracing));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, entity.DealStatus));
                builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, entity.Version));
            }
            else
            {
                if (fields.Contains(TTaskProperties.Topic))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Topic, entity.Topic));
                }
                if (fields.Contains(TTaskProperties.Tracing))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, entity.Tracing));
                }
                if (fields.Contains(TTaskProperties.DealStatus))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, entity.DealStatus));
                }
                if (fields.Contains(TTaskProperties.Version))
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, entity.Version));
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
                builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.Equal, entity.TaskId));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.TaskId, entity.TaskId));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Topic, entity.Topic));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, entity.Tracing));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, entity.DealStatus));
                    builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, entity.Version));
                }
                else
                {
                    if (fields.Contains(TTaskProperties.Topic))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Topic, entity.Topic));
                    }
                    if (fields.Contains(TTaskProperties.Tracing))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Tracing, entity.Tracing));
                    }
                    if (fields.Contains(TTaskProperties.DealStatus))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.DealStatus, entity.DealStatus));
                    }
                    if (fields.Contains(TTaskProperties.Version))
                    {
                        builder.ComponentValue.Values.Add(new PDMDbPropertyValue(TTaskProperties.Version, entity.Version));
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
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.TaskId);
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.Topic);
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.Tracing);
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.DealStatus);
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.Version);
            }
            else
            {
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.TaskId);
                foreach (var field in fields)
                {
                    builder.ComponentFieldAliases.FieldAliases.Add(field);
                }
            }
            builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.Equal, entity.TaskId));
            query.SelectBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Select<TTask>(session, query);
        }
        public static List<TTask> DbSelect(this List<TTask> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.TaskId);
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.Topic);
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.Tracing);
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.DealStatus);
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.Version);
            }
            else
            {
                builder.ComponentFieldAliases.FieldAliases.Add(TTaskProperties.TaskId);
                foreach (var field in fields)
                {
                    builder.ComponentFieldAliases.FieldAliases.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.TaskId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Wheres.Add(new PDMDbPropertyOperateValue(TTaskProperties.TaskId, OperatorType.In, Ids));
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
