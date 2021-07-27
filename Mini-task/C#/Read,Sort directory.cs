using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace 
{
	class Program
	{
	/*
         * При запуске программы в консольном окне выводится список файлов из текущего
         каталога (или каталога, указанного в качестве параметра командной строки). Файлы сортируются
         по размеру. Информация о файлах должна выводиться в трех столбцах, содержащих имя файла,
         его размер (в байтах) и дату создания. 
         */

		static void Main(string[] args)
		{
			string path="";
			List<string> dir;
			foreach (string s in args)
				path += s + " ";
			path = path.Trim();
			if (path.Length < 2 || path[1] != ':')
				path = AppDomain.CurrentDomain.BaseDirectory + path;
			Console.WriteLine(path);
			if (Directory.Exists(path))
			{
				if (path[path.Length - 1] != '\\')
					path += "\\";

				dir = new List<string>();
				Directory
							 .GetFiles(path, "*", SearchOption.TopDirectoryOnly)
							 .ToList()
							 .ForEach(f => dir.Add(Path.GetFileName(f)));

				for (int j = 1; j < dir.Count; j++)
					for (int i = 0; i < dir.Count - 1; i++)
						if (new System.IO.FileInfo(path + dir[i]).Length > new System.IO.FileInfo(path + dir[i + 1]).Length)
						{
							string s = dir[i];
							dir[i] = dir[i + 1];
							dir[i + 1] = s;
						}

				foreach (string s in dir)
				{
					Console.WriteLine(s + " | " + new System.IO.FileInfo(path + s).Length + " | " + System.IO.File.GetCreationTime(path + s));
				}
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine("Некорректный ввод!");
			}

			while (true)
			{
				do
				{
					Console.WriteLine("Введите путь к директории:");
					path = Console.ReadLine();
					if (path.Length < 2 || path[1] != ':')
						path = AppDomain.CurrentDomain.BaseDirectory + path;
					if (Directory.Exists(path))
						break;
					else
						Console.WriteLine("Некорректный ввод!");
				}
				while (true);

				if (path[path.Length - 1] != '\\')
					path += "\\";

				dir = new List<string>();
				Directory
							 .GetFiles(path, "*", SearchOption.TopDirectoryOnly)
							 .ToList()
							 .ForEach(f => dir.Add(Path.GetFileName(f)));

				for (int j = 1; j < dir.Count; j++)
					for (int i = 0; i < dir.Count - 1; i++)
						if (new System.IO.FileInfo(path + dir[i]).Length > new System.IO.FileInfo(path + dir[i + 1]).Length)
						{
							string s = dir[i];
							dir[i] = dir[i + 1];
							dir[i + 1] = s;
						}

				foreach (string s in dir)
				{
					Console.WriteLine(s + " | " + new System.IO.FileInfo(path + s).Length + " | " + System.IO.File.GetCreationTime(path + s));
				}
				Console.WriteLine();
			}
			//Console.ReadKey(true);
		}
	}
}
