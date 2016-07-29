using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using VL.Common.ORM.Objects;
using TODOTask.Objects.Enums;

namespace TODOTask.Objects.Entities
{
    [DataContract]
    public partial class TTask : IPDMTBase
    {
        #region Properties
        [DataMember]
        public Guid TaskId { get; set; }
        [DataMember]
        public String Topic { get; set; }
        [DataMember]
        public String Tracing { get; set; }
        [DataMember]
        public ETaskDealStatus DealStatus { get; set; }
        [DataMember]
        public Int16 Version { get; set; }
        #endregion

        #region Constructors
        public TTask()
        {
        }
        public TTask(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.TaskId = new Guid(reader[nameof(this.TaskId)].ToString());
            this.Topic = Convert.ToString(reader[nameof(this.Topic)]);
            this.Tracing = Convert.ToString(reader[nameof(this.Tracing)]);
            this.DealStatus = (ETaskDealStatus)Enum.Parse(typeof(ETaskDealStatus), reader[nameof(this.DealStatus)].ToString());
            this.Version = Convert.ToInt16(reader[nameof(this.Version)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(TaskId)))
            {
                this.TaskId = new Guid(reader[nameof(this.TaskId)].ToString());
            }
            if (fields.Contains(nameof(Topic)))
            {
                this.Topic = Convert.ToString(reader[nameof(this.Topic)]);
            }
            if (fields.Contains(nameof(Tracing)))
            {
                this.Tracing = Convert.ToString(reader[nameof(this.Tracing)]);
            }
            if (fields.Contains(nameof(DealStatus)))
            {
                this.DealStatus = (ETaskDealStatus)Enum.Parse(typeof(ETaskDealStatus), reader[nameof(this.DealStatus)].ToString());
            }
            if (fields.Contains(nameof(Version)))
            {
                this.Version = Convert.ToInt16(reader[nameof(this.Version)]);
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TTask);
            }
        }
        #endregion
    }
}
