using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace DmoPL.Helper
{
	public static class DocumentSettings
	{
		public static string Upload(IFormFile File, string FolderName)
		{
			if (File is not null)
			{
				string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
				string FileName = $"{Guid.NewGuid()}{File.FileName}";
				string FilePath = Path.Combine(FolderPath, FileName);
				using var FileSream = new FileStream(FilePath, FileMode.Create);
				File.CopyTo(FileSream);
				return FileName;
			}
			return null;
		}
		public static void Delete(string FileName, string FolderName)
		{
			if (FileName is not null&& FolderName is not null)
			{
				string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
			
				string FilePath = Path.Combine(FolderPath, FileName);
				if (File.Exists(FilePath))
				{
					File.Delete(FilePath);
				}
			}


		}
	}
}
