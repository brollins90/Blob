using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public class WindowsUpdateMonitor : BaseMonitor
    {
        protected override string MonitorId { get { return MonitorName; } }
        protected override string MonitorName { get { return "WindowsUpdateMonitor"; } }
        protected override string MonitorDescription { get { return "Checks the status of Windows Updates"; } }

        public override ResultData Execute(bool collectPerfData = false)
        {
        //    // https://msdn.microsoft.com/en-us/library/aa387102%28v=vs.85%29.aspx

        //    //UpdateSession uSession = new UpdateSession();
        //    //IUpdateSearcher uSearcher = uSession.CreateUpdateSearcher();
        //    //uSearcher.Online = false;
        //    //try
        //    //{
        //    //    Console.WriteLine("Searching...");
        //    //    ISearchResult sResult = uSearcher.Search("IsInstalled=1 And IsHidden=0");
        //    //    Console.WriteLine("Found " + sResult.Updates.Count + " updates" + Environment.NewLine);
        //    //    foreach (IUpdate update in sResult.Updates)
        //    //    {
        //    //        Console.WriteLine(update.Title);
        //    //        //textBox1.AppendText(update.Title + Environment.NewLine);
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine("Something went wrong: " + ex.Message);
        //    //}

        //    ////Type t = Type.GetTypeFromProgID("Microsoft.Update.Session", "remotehostname");
        //    ////UpdateSession session = (UpdateSession)Activator.CreateInstance(t);
        //    ////IUpdateSearcher updateSearcher = session.CreateUpdateSearcher();
        //    ////int count = updateSearcher.GetTotalHistoryCount();
        //    ////IUpdateHistoryEntryCollection history = updateSearcher.QueryHistory(0, count);
        //    ////for (int i = 0; i < count; ++i)
        //    ////{
        //    ////    Console.WriteLine(string.Format("Title: {0}\tSupportURL: {1}\tDate: {2}\tResult Code: {3}\tDescription: {4}\r\n", history[i].Title, history[i].SupportUrl, history[i].Date, history[i].ResultCode, history[i].Description));
        //    ////}


            return base.Execute();
        }
    }
}
