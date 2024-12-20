namespace AdminDashboard.Helper
{
	public static class PictureSetting
	{
		public static string UploadFile(IFormFile file,string folderName)
		{
			var folderpath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\images" ,folderName);
			var fileName = $"{ Guid.NewGuid() }+ {file.FileName}";
			var filepath = Path.Combine(folderpath,fileName);
		    var fs = new FileStream(filepath,FileMode.Create);
			file.CopyTo(fs);
			return Path.Combine(@"images\products", fileName);
		}
		public static void DeleteFile(string folderName,string fileName)
		{
			var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName,fileName);
			if (File.Exists(filepath)) 
			{
				File.Delete(filepath);
			}

		}
	}
}
