using System.Drawing;
using System.Runtime.Versioning;

namespace TopLevelStatements;

public class Worker
{
	private Task scanTask;

	public Worker()
	{
		scanTask = new Task(Run);
		scanTask.Start();
	}

	public void Run()
	{
		while (true)
		{
			if (Program.ImagePaths.TryDequeue(out string path))
			{
				Console.WriteLine($"Processing image: {path}");
				ProcessImage(path, $"Output\\{path.Replace("Images", "")}");
				File.Delete(path);
				Console.WriteLine($"Processed image: {path}");
			}
		}
	}

	[SupportedOSPlatform("windows")] //Remove warnings
	private void ProcessImage(string loadPath, string savePath)
	{
		using Bitmap img = new Bitmap(loadPath);
		using Bitmap output = new Bitmap(img.Width, img.Height);
		for (int i = 0; i < img.Width; i++)
		{
			for (int j = 0; j < img.Height; j++)
			{
				Color currentPixel = img.GetPixel(i, j);
				int grayScale = (int) (currentPixel.R * 0.3 + currentPixel.G * 0.59 + currentPixel.B * 0.11);
				Color newColor = Color.FromArgb(currentPixel.A, grayScale, grayScale, grayScale);
				output.SetPixel(i, j, newColor);
			}
		}
		output.Save(savePath); //Dont forget the filename
	}
}