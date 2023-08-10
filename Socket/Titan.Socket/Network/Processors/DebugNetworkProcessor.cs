using System;
using System.Text;
using Titan.Socket.Analyzer;
using Titan.Socket.Network.Contracts;
using Titan.Socket.Terminal;

namespace Titan.Socket.Network.Processors
{
    internal class DebugNetworkProcessor : INetworkProcessor
    {
        public bool Can(Dictionary<string, object> metadata)
        {
            return true;
        }

        public async Task<(Dictionary<string, object> metadata, byte[] payload)> Process(byte[] data, Dictionary<string, object> metadata)
        {
            var parsedMetaData = string.Join(", ", metadata);

            TerminalService.Trace($"Received packet metadata: {parsedMetaData} / SIZE: {data.Length}");

            if (metadata["bridge"].ToString() == "binary")
            {
                var userRootFolder = $"C:\\Server\\Uploads\\{metadata["language"]}\\" + metadata["guid"];
                var path = userRootFolder + "\\" + metadata["tag"] + "\\" + metadata["type"] + "\\";
                var file_name = path + metadata["subtype"];

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    File.WriteAllBytes(file_name, data);
                    TerminalService.Trace($"File saved to: {file_name}");

                    AnalyzerService.Analyze(metadata["type"].ToString(), metadata["tag"].ToString(), userRootFolder, file_name);
                }
                catch { }
            }

            if (metadata["bridge"].ToString() == "profile")
            {
                var path = $"C:\\Server\\Uploads\\{metadata["language"]}\\" + metadata["guid"];
                var file_name = path +"\\Profile.txt";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if(!File.Exists(path + "ReadMe.txt"))
                {
                    File.WriteAllLines(path + "\\ReadMe.txt", AnalyzerUtils.GetReadme());
                }

                if(File.Exists(file_name))
                {
                    var content = File.ReadAllLines(file_name).ToList();
                    var newContent = new List<string>();
                    var str = Encoding.UTF8.GetString(data);

                    foreach(var line in content)
                    {
                        
                        if(line != str)
                        {
                            newContent.Add(line);
                        }
                    }

                    newContent.Add(str);
                    File.WriteAllLines(file_name, newContent);
                }
                else
                {
                    string[] logo = AnalyzerUtils.GetLogo("Machine Profile");
                    string[] line = new string[]
                    {
                        Encoding.UTF8.GetString(data)
                    };
                    string[] content = new string[logo.Length + line.Length];

                    logo.CopyTo(content, 0);
                    line.CopyTo(content, logo.Length);

                    // Create New File
                    File.WriteAllLines(file_name, content);
                }
            }

            var responseMetaData = new Dictionary<string, object>()
            {
                ["code"] = 0,
                ["data.received"] = data.Length
            };

            return await Task.Run(() =>
            {
                return (responseMetaData, Array.Empty<byte>());
            });
        }
    }
}
