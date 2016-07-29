using System;
using System.Collections.Generic;
using VL.Common.ORM.Objects;

namespace TODOTask.Objects.Entities
{
    public partial class TTask : IPDMTBase
    {
        public List<TEvent> Events { get; set; }
    }
}
