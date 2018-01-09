using FEZAutoScore.Model.Entity;
using FEZAutoScore.Model.TextFomatter;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FEZAutoScore.Model.Repository
{
    public class ScoreFileRepository
    {
        private const string LatestScoreFileName = "LatestScore.txt";
        private const string DefaultCsvFileName = "scores.csv";

        private static readonly DirectoryInfo _directory = new DirectoryInfo(
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "fez_auto_score")
            );

        private ScoreFileRepository()
        {
        }

        public static ScoreFileRepository Create(bool createDummyFile)
        {
            var ret = new ScoreFileRepository();

            if (createDummyFile)
            {
                ret.CreateDummyLatestScoreIfNotExists();
            }

            return ret;
        }

        public void OpenDirectory()
        {
            CreateDirectoryIfNotExists();

            Process.Start(_directory.FullName);
        }

        public async Task SaveAsLatestScoreAsync(string format, ScoreEntity score)
        {
            CreateDirectoryIfNotExists();

            var fullpath = GetLatestScoreFilePath();

            using (var sw = new StreamWriter(fullpath, false, Encoding.UTF8))
            {
                var text = ScoreTextFormatter.ToString(format, score);

                await sw.WriteLineAsync(text);
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

        public void CreateDummyLatestScoreIfNotExists()
        {
            CreateDirectoryIfNotExists();

            var fullpath = GetLatestScoreFilePath();
            var file = new FileInfo(fullpath);

            if (!file.Exists)
            {
                file.Create();
            }
        }

        public void CreateDirectoryIfNotExists()
        {
            if (!_directory.Exists)
            {
                _directory.Create();
            }
        }

        private string GetLatestScoreFilePath()
        {
            return Path.Combine(_directory.FullName, LatestScoreFileName);
        }
    }
}
