using FEZAutoScore.Model.Setting;
using Reactive.Bindings;

namespace FEZAutoScore.ViewModel
{
    public class SettingDialogViewModel
    {
        public ReactiveProperty<AppSetting> AppSetting { get; }
        public ReactiveCommand RestoreDefaultCommand { get; }

        public SettingDialogViewModel(AppSetting setting)
        {
            AppSetting = new ReactiveProperty<AppSetting>(setting);

            RestoreDefaultCommand = new ReactiveCommand();
            RestoreDefaultCommand.Subscribe(() =>
            {
                var defaultSetting = new AppSetting();
                AppSetting.Value = defaultSetting;
            });
        }
    }
}
