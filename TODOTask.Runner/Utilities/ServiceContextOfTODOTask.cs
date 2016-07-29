using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Common.DAS.Utilities;
using VL.Common.Logger.Objects;
using VL.Common.Protocol;
using VL.Common.Protocol.IService;

namespace TODOTask.Runner.Utilities
{
    public class ServiceContextOfTODOTask : ServiceContext
    {
        public ServiceContextOfTODOTask(DbConfigEntity databaseConfig, ProtocolConfig protocolConfig, ILogger serviceLogger) : base(databaseConfig, protocolConfig, serviceLogger)
        {
        }

        public override string GetUnitName()
        {
            return nameof(TODOTask);
        }
        protected override List<DependencyResult> InitOthers()
        {
            return new List<DependencyResult>();
        }
    }
}
