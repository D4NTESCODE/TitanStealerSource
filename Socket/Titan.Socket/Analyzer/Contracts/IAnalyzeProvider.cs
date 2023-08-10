using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Socket.Analyzer.Contracts
{
    internal interface IAnalyzeProvider
    {
        public bool Can(string type, string tag);
        public void Analyze(string type, string tag, string userRootFolder, string file);
    }
}
