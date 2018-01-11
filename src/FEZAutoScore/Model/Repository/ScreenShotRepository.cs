using FEZAutoScore.Model.Entity;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace FEZAutoScore.Model.Repository
{
    public class ScoreScreenShotRepository
    {
        private const string FolderName = "fez_screenshot";
        private static readonly DirectoryInfo _directory = new DirectoryInfo(
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                FolderName));

        private ScoreScreenShotRepository()
        {
        }

        public static ScoreScreenShotRepository Create(bool createDirectory)
        {
            var ret = new ScoreScreenShotRepository();

            if (createDirectory)
            {
                ret.CreateDirectoryIfNotExists();
            }

            return ret;
        }

        public void OpenDirectory()
        {
            CreateDirectoryIfNotExists();

            Process.Start(_directory.FullName);
        }

        public async Task SaveAsPngAsync(ScoreEntity score, Bitmap bitmap)
        {
            await Task.Run(() =>
            {
                CreateDirectoryIfNotExists();

                var fileName = $"{score.記録日時.ToString("yyyyMMdd_HHmmss")}.png";
                var fullpath = Path.Combine(_directory.FullName, fileName);

                bitmap.Save(fullpath, ImageFormat.Png);
            });
        }

        public void CreateDirectoryIfNotExists()
        {
            if (!_directory.Exists)
            {
                _directory.Create();
            }
        }
    }
}
