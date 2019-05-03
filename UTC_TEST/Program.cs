using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using ETM.WCCOA;

namespace GettingStarted
{
    // This C# program provides the same functionality as the C++ template manager.
    // it establishes connection to a project
    // reads necessary information from the config File if available
    // Connects on DPE1 and forwards value changes to DPE2
    class Program
    {
        static void Main(string[] args)
        {
            // Create Manager object
            OaManager myManager = OaSdk.CreateManager();

            // Initialize Manager Configuration
            myManager.Init(ManagerSettings.DefaultApiSettings, args);

            // Start the Manager and Connect to the OA project with the given configuration
            myManager.Start();

            // Get Access to the ProcessValues
            var valueAccess = myManager.ProcessValues;
            var processModel = myManager.ProcessModel;
            var dateTime = DateTime.Now;
            var dateTimeUTC = dateTime;
            dateTimeUTC = DateTime.SpecifyKind(dateTimeUTC, DateTimeKind.Utc);

            Console.WriteLine("test_utc is kind <{0}> <{1}>", dateTimeUTC.Kind, dateTimeUTC.ToString());
            Console.WriteLine("test_localtime is kind <{0}> <{1}>", dateTime.Kind, dateTime.ToString());

            processModel.CreateDp("test_utc", "ExampleDP_Int");
            processModel.CreateDp("test_localtime", "ExampleDP_Int");

            valueAccess.SetDpValue(dateTimeUTC, "test_utc.", 1);
            valueAccess.SetDpValue(dateTime, "test_localtime.", 1);

        }
    }
}