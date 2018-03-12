using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace FEZAutoScore.Model.Process
{
    public class FEZExecuteBatchFactory
    {
        private static readonly string BatchCommand =
            "start \"\" \"%FEZExecutePath%\"" + Environment.NewLine +
            "start \"\" \"%FEZAutoScoreExecutePath%\"";

        private const string BatchFileName = "FEZClientStart.cmd";

        public static void CreateBatchFile()
        {
            string batchCommand = BatchCommand;
            string fezClientPath = string.Empty;
            string exeDirectiory = string.Empty;

            // バッチコマンド作成
            try
            {
                // FEZ Client
                fezClientPath = GetFEZClientPath();

                // FEZ Auto Score
                var exePath = Assembly.GetExecutingAssembly().Location;
                exeDirectiory = Path.GetDirectoryName(exePath);

                batchCommand = batchCommand.Replace("%FEZExecutePath%", fezClientPath);
                batchCommand = batchCommand.Replace("%FEZAutoScoreExecutePath%", exePath);
            }
            catch
            {
                throw new FileNotFoundException();
            }

            // バッチファイル作成 (FEZAutoScore.exeと同じフォルダに保存)
            var batchFilePath = Path.Combine(exeDirectiory, BatchFileName);
            using (var sw = new StreamWriter(batchFilePath, false, Encoding.GetEncoding("Shift_JIS")))
            {
                sw.WriteLine(batchCommand);
            }

            // デスクトップにショートカットを作成
            var shortcutPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                "FEZ+FAS.lnk");
            CreateShortcut(shortcutPath, batchFilePath, fezClientPath);
        }

        private static void CreateShortcut(string shortcutPath, string targetPath, string iconPath)
        {
            dynamic shell = null;
            dynamic shortcut = null;

            try
            {
                shell = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                shortcut = shell.CreateShortcut(shortcutPath);

                shortcut.TargetPath = targetPath;
                shortcut.IconLocation = iconPath + ",0";
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
                shortcut.WindowStyle = 7;
                shortcut.Description = string.Empty;

                shortcut.Save();
            }
            finally
            {
                try
                {
                    if (shortcut != null) Marshal.FinalReleaseComObject(shortcut);
                }
                catch { }
                try
                {
                    if (shell != null) Marshal.FinalReleaseComObject(shell);
                }
                catch { }
            }
        }

        private static string GetFEZClientPath()
        {
            // レジストリから実行ファイルの場所を特定できないため、
            // 固定値(デフォルトのインストール先)を取得
            var clientPath = Path.Combine(
                Environment.Is64BitOperatingSystem ?
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) :
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "SquareEnix\\FantasyEarthZero\\FEzero.exe");

            // ディレクトリが存在しなければ手動で実行ファイルを選択してもらう
            if (!File.Exists(clientPath))
            {
                MessageBox.Show("FEZクライアントの実行ファイルを選択してください。");

                OpenFileDialog dialog = new OpenFileDialog()
                {
                    CheckFileExists = true,
                    CheckPathExists = true,
                    FilterIndex = 1,
                    Filter = "実行ファイル(.exe)|*.exe|All Files (*.*)|*.*",
                    InitialDirectory = Environment.Is64BitOperatingSystem ?
                        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) :
                        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    FileName = "FEzero.exe"
                };

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    clientPath = dialog.FileName;
                }
            }

            return clientPath;
        }
    }
}
