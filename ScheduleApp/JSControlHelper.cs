using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace ScheduleApp
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JSControlHelper
    {
        MainWindow prozor;
        public JSControlHelper(MainWindow w)
        {
            prozor = w;
        }
    }
}
