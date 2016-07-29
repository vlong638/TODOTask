using System;
using System.Collections.Generic;
using VL.Common.ORM.Objects;

namespace TODOTask.Objects.Entities
{
    public partial class TEvent : IPDMTBase
    {
        public TTask Task { get; set; }
    }
}
