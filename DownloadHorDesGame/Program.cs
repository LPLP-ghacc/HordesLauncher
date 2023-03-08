using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var downloadUrl = new Uri(config["DownloadLink"]);
var savePath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + config["DownloadFolder"];
var saveFileName = Environment.CurrentDirectory + Path.DirectorySeparatorChar + config["DownloadName"];

var executionName = savePath + Path.DirectorySeparatorChar + "Horizontal Descent.exe";

Console.Title = "HorDes Downloader";
Console.WriteLine("Downloading the game...");

using (var client = new WebClient())
{
    client.DownloadProgressChanged += (sender, e) =>
    {
        Console.Write($"\rProgress: {e.ProgressPercentage}%\t{e.BytesReceived} / {e.TotalBytesToReceive} bytes");
    };

    client.DownloadFileCompleted += (a, b) =>
    {
        Console.WriteLine("\nDownload completed.");
    };

    await client.DownloadFileTaskAsync(downloadUrl, saveFileName);
}

if (Directory.Exists(savePath))
    Directory.Delete(savePath);

ZipFile.ExtractToDirectory(saveFileName, savePath);
File.Delete(saveFileName);

Console.WriteLine("Wanna play? (Yes -> Enter, No -> Alt + F4)");
var th = Console.ReadLine();

Process.Start(savePath + Path.DirectorySeparatorChar + "Horizontal Descent.exe");
