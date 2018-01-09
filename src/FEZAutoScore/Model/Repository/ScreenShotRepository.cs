using FEZAutoScore.Model.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace FEZAutoScore.Model.Repository
{
    public class ScoreScreenShotRepository
    {
        private const string FolderName = "screenshot";
        private static readonly DirectoryInfo _directory = new DirectoryInfo(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    FolderName));

        public async Task SaveAsPngAsync(ScoreEntity score, Bitmap bitmap)
        {
            await Task.Run(() =>
            {
                if (!_directory.Exists)
                {
                    _directory.Create();
                }

                var fileName = $"{score.記録日時.ToString("yyyyMMdd_HHmmss")}.png";
                var fullpath = Path.Combine(_directory.FullName, fileName);

                bitmap.Save(fullpath, ImageFormat.Png);
            });
        }
    }
}
