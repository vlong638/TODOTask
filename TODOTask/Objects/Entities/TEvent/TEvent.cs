using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using VL.Common.ORM.Objects;
using TODOTask.Objects.Enums;

namespace TODOTask.Objects.Entities
{
    [DataContract]
    public partial class TEvent : IPDMTBase
    {
        #region Properties
        [DataMember]
        public Guid TaskId { get; set; }
        [DataMember]
        public Guid EventId { get; set; }
        [DataMember]
        public String Topic { get; set; }
        [DataMember]
        public String Participant { get; set; }
        [DataMember]
        public DateTime? WhenToStart { get; set; }
        [DataMember]
        public DateTime? WhenToEnd { get; set; }
        [DataMember]
        public DateTime? WhenStart { get; set; }
        [DataMember]
        public DateTime? WhenEnd { get; set; }
        [DataMember]
        public EEventDealStatus DealStatus { get; set; }
        [DataMember]
        public Int16 Version { get; set; }
        #endregion

        #region Constructors
        public TEvent()
        {
        }
        public TEvent(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.TaskId = new Guid(reader[nameof(this.TaskId)].ToString());
            this.EventId = new Guid(reader[nameof(this.EventId)].ToString());
            this.Topic = Convert.ToString(reader[nameof(this.Topic)]);
            this.Participant = Convert.ToString(reader[nameof(this.Participant)]);
            if (reader[nameof(this.WhenToStart)] != DBNull.Value)
            {
                this.WhenToStart = Convert.ToDateTime(reader[nameof(this.WhenToStart)]);
            }
            if (reader[nameof(this.WhenToEnd)] != DBNull.Value)
            {
                this.WhenToEnd = Convert.ToDateTime(reader[nameof(this.WhenToEnd)]);
            }
            if (reader[nameof(this.WhenStart)] != DBNull.Value)
            {
                this.WhenStart = Convert.ToDateTime(reader[nameof(this.WhenStart)]);
            }
            if (reader[nameof(this.WhenEnd)] != DBNull.Value)
            {
                this.WhenEnd = Convert.ToDateTime(reader[nameof(this.WhenEnd)]);
            }
            this.DealStatus = (EEventDealStatus)Enum.Parse(typeof(EEventDealStatus), reader[nameof(this.DealStatus)].ToString());
            this.Version = Convert.ToInt16(reader[nameof(this.Version)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(TaskId)))
            {
                this.TaskId = new Guid(reader[nameof(this.TaskId)].ToString());
            }
            if (fields.Contains(nameof(EventId)))
            {
                this.EventId = new Guid(reader[nameof(this.EventId)].ToString());
            }
            if (fields.Contains(nameof(Topic)))
            {
                this.Topic = Convert.ToString(reader[nameof(this.Topic)]);
            }
            if (fields.Contains(nameof(Participant)))
            {
                this.Participant = Convert.ToString(reader[nameof(this.Participant)]);
            }
            if (fields.Contains(nameof(WhenToStart)))
            {
                if (reader[nameof(this.WhenToStart)] != DBNull.Value)
                {
                    this.WhenToStart = Convert.ToDateTime(reader[nameof(this.WhenToStart)]);
                }
            }
            if (fields.Contains(nameof(WhenToEnd)))
            {
                if (reader[nameof(this.WhenToEnd)] != DBNull.Value)
                {
                    this.WhenToEnd = Convert.ToDateTime(reader[nameof(this.WhenToEnd)]);
                }
            }
            if (fields.Contains(nameof(WhenStart)))
            {
                if (reader[nameof(this.WhenStart)] != DBNull.Value)
                {
                    this.WhenStart = Convert.ToDateTime(reader[nameof(this.WhenStart)]);
                }
            }
            if (fields.Contains(nameof(WhenEnd)))
            {
                if (reader[nameof(this.WhenEnd)] != DBNull.Value)
                {
                    this.WhenEnd = Convert.ToDateTime(reader[nameof(this.WhenEnd)]);
                }
            }
            if (fields.Contains(nameof(DealStatus)))
            {
                this.DealStatus = (EEventDealStatus)Enum.Parse(typeof(EEventDealStatus), reader[nameof(this.DealStatus)].ToString());
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
                return nameof(TEvent);
            }
        }
        #endregion
    }
}
