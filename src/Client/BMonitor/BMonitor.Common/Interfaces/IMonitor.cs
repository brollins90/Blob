
using BMonitor.Common.Models;

namespace BMonitor.Common.Interfaces
{
    public interface IMonitor
    {
        ResultData Execute(bool collectPerfData = false);
    }
}
