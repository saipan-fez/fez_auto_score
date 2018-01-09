﻿using FEZAutoScore.Model.Setting;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reflection;

namespace FEZAutoScore.Model.Repository
{
    public class SettingRepository
    {
        private const string FolderName = "setting";
        private const string AppSettingFileName = "app_setting.json";
        private const string ColumnVisibleSettingFileName = "column_setting.json";

        private static readonly DirectoryInfo _directory = new DirectoryInfo(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    FolderName));

        public ScoreDataGridColumnVisibleSetting GetColumnVisibleSetting()
        {
            return GetSetting<ScoreDataGridColumnVisibleSetting>(ColumnVisibleSettingFileName);
        }

        public AppSetting GetAppSetting()
        {
            return GetSetting<AppSetting>(AppSettingFileName);
        }

        private T GetSetting<T>(string fileName) where T : BaseSetting, new()
        {
            T setting = null;

            try
            {
                // ファイルから読み出し
                var file = _directory.GetFiles()
                    .Where(x => x.Name == fileName)
                    .FirstOrDefault();

                using (var stream = new StreamReader(file.FullName))
                {
                    var json = stream.ReadToEnd();
                    setting = JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch
            {
                // 読み出しに失敗すれば生成
                setting = new T();
            }

            // 変更通知が来たら保存するよう設定
            setting.SettingChanged.Subscribe(Observer.Create<Unit>(async _ =>
            {
                try
                {
                    if (!_directory.Exists)
                    {
                        _directory.Create();
                    }

                    var filePath = Path.Combine(_directory.FullName, fileName);

                    using (var stream = new StreamWriter(filePath, false))
                    {
                        var json = JsonConvert.SerializeObject(setting);
                        await stream.WriteAsync(json);
                    }
                }
                catch { }
            }));

            return setting;
        }
    }
}
