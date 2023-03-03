using System.Net;

var imagedownloader = new ImageDownloader();
imagedownloader.ImageStarted += () => Console.WriteLine("Скачивание файла началось");
var myTask = imagedownloader.Download("https://cdn.fishki.net/upload/post/201502/15/1429697/1_ngc-2264-chast---zvyozdnoe-skoplenie-snezhinka.jpg");
Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
if (Console.ReadKey(true).Key != ConsoleKey.A )
{
    while (!myTask.IsCompleted)
    {
        Console.WriteLine("не загружена");
        await Task.Delay(500);
    }
    Console.WriteLine("загружена");
    imagedownloader.ImageCompleted += () => Console.WriteLine("Скачивание файла закончилось");

}




class ImageDownloader
{

    public event Action ImageStarted;
    public event Action ImageCompleted;
    public async Task Download(string url)
    {
        string remoteUri = url;
		string fileName = "bigimage.jpg";
		var myWebClient = new WebClient();
        ImageStarted?.Invoke();
		Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileName, remoteUri);
		await myWebClient.DownloadFileTaskAsync(remoteUri, fileName);
		Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);
        ImageCompleted?.Invoke();
    }
}