using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using ETM.WCCOA;

namespace GettingStarted
{
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
            // Output UTC Time
            Console.WriteLine("test_utc is kind <{0}> <{1}>", dateTimeUTC.Kind, dateTimeUTC.ToString());
            // Output Local Time
            Console.WriteLine("test_localtime is kind <{0}> <{1}>", dateTime.Kind, dateTime.ToString());

            // create UTC Time Test DP
            processModel.CreateDp("test_utc", "ExampleDP_Int");
            // create Local Time Test DP
            processModel.CreateDp("test_localtime", "ExampleDP_Int");

            // set Value to 1 
            // Time gets converted from UTC -> local time -> to UTC by WinCC OA 
            // Time: 2019-05-03T19:21:21+00:00 !! BUG !!
            valueAccess.SetDpValue(dateTimeUTC, "test_utc.", 1);
            // set Value to 1
            // Time gets converted to UTC by WinCC OA 
            // Time: 2019-05-03T19:19:21+00:00
            valueAccess.SetDpValue(dateTime, "test_localtime.", 1);

        }
    }
}
