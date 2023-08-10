using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Socket.Analyzer.Contracts;
using Titan.Socket.Analyzer.Providers;

namespace Titan.Socket.Analyzer
{
    internal class AnalyzerService
    {
        private static Dictionary<string, IAnalyzeProvider> SupportedProviders = new()
        {
            ["Google Chrome"] = new GoogleChromeAnalyzerProvider(),
            ["Microsoft Edge"] = new EdgeAnalyzerProvider()
        };

        public static async void Analyze(string type, string tag, string userRootFolder, string file)
        {
            Console.WriteLine($"Analyze: {type} / {tag} / {userRootFolder} / {file}");

            foreach(var provider in SupportedProviders)
            {
                if(provider.Value.Can(type, tag))
                {
                    await Task.Run(() =>
                    {
                        provider.Value.Analyze(type, tag, userRootFolder, file);
                    });
                }
            }
        }
    }
}
