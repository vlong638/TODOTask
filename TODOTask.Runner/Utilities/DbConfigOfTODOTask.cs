﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Common.DAS.Utilities;

namespace TODOTask.Runner.Utilities
{
    public class DbConfigOfTODOTask : DbConfigEntity
    {
        public static string DbNameOfTODOTask { set; get; } = nameof(TODOTask);

        public DbConfigOfTODOTask(string fileName) : base(fileName)
        {
        }

        protected override List<DbConfigItem> GetDbConfigItems()
        {
            List<DbConfigItem> result = new List<DbConfigItem>()
            {
                new DbConfigItem(DbNameOfTODOTask),
            };
            return result;
        }
    }
}
