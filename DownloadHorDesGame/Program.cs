using System.Diagnostics;
using System.IO.Compression;
using System.Net;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "HORIZONTAL DESCENT LAUNCHER";

        Console.Title = "HorDes Downloader";
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("START DOWNLOAD...");

        WebClient client = new WebClient();

        NewMesssage("DOWNLOADING NOW...");

        client.DownloadFile(new Uri("https://www.dropbox.com/s/0aykaxz1f426bgi/Game.zip?dl=1"), Environment.CurrentDirectory + "/GAME.zip");

        NewMesssage("UNPACK ZIP FILE...");

        if (Directory.Exists(Environment.CurrentDirectory + "/GAME"))
            Directory.Delete(Environment.CurrentDirectory + "/GAME");

        ZipFile.ExtractToDirectory(Environment.CurrentDirectory + "/GAME.zip", Environment.CurrentDirectory + "/GAME");

        File.Delete(Environment.CurrentDirectory + "/GAME.zip");

        NewMesssage("DOWNLOAD COMPLETE \n WANNA PLAY NOW? (Y/N)");

        var input = Console.ReadLine();

        if (input?.ToLower() == "y")
            Process.Start(Environment.CurrentDirectory + "/GAME/Horizontal Descent.exe");

        Environment.Exit(0);
    }

    private static void NewMesssage(string message)
    {
        Console.Clear();
        Random rnd = new();
        Console.ForegroundColor = (ConsoleColor)rnd.Next(1, 14);
        Console.WriteLine(message);
    }
}


