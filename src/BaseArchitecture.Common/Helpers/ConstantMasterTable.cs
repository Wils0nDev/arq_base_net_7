using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Common.Helpers
{
    public static class ConstantMasterTable
    {
        public static class Schema
        {
            public const string Cnfg = "Cnfg";
            public const string Demo = "Demo";
        }


        public static class Procedure
        {
            public const string ListMasterTable = "USP_LIST_MasterTable";
            public const string CreateAndUpdatePerson = "USP_CreateUpdatePerson";
        }
        public static class UrlGenerales
        {
            public const string LogoAntamina = "00201";
        }
    }
}
