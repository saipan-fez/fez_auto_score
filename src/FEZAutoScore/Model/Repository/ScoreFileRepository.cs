using FEZAutoScore.Model.Entity;
using FEZAutoScore.Model.TextFomatter;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FEZAutoScore.Model.Repository
{
    public class ScoreFileRepository
    {
        private const string LatestScoreFileName = "LatestScore.txt";
        private const string DefaultCsvFileName = "scores.csv";

        private static readonly DirectoryInfo _directory = new DirectoryInfo(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        public async Task SaveAsLatestScoreAsync(ScoreEntity score)
        {
            if (!_directory.Exists)
            {
                _directory.Create();
            }

            var fullpath = Path.Combine(_directory.FullName, LatestScoreFileName);

            using (var text = new StreamWriter(fullpath, false, Encoding.UTF8))
            {
                var kill = score.キル数;
                var dead = score.デッド数;
                var pcd = string.Format("{0:f1}", ((score.PC与ダメージ + 50.0d) / 1000.0d)); // "99.9k"と表示するため小数第二位で四捨五入

                await text.WriteLineAsync($"{kill}kill {dead}dead {pcd}k");
            }
        }

        public async Task SaveAsCsvAsync(IEnumerable<ScoreEntity> scores)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = DefaultCsvFileName;

            var result = saveFileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var csvText = CsvScoreTextFormatter.ToString(scores);

                using (var text = new StreamWriter(saveFileDialog.OpenFile(), Encoding.UTF8))
                {
                    await text.WriteLineAsync(csvText);
                }
            }
        }
    }
}
