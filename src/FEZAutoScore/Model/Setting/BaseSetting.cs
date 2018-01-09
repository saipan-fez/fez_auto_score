using Reactive.Bindings;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive;

namespace FEZAutoScore.Model.Setting
{
    public abstract class BaseSetting
    {
        public IObservable<Unit> SettingChanged => _settingChanged;
        private ReactiveProperty<Unit> _settingChanged = new ReactiveProperty<Unit>(mode: ReactivePropertyMode.DistinctUntilChanged);

        public BaseSetting()
        {
            // 通知可能なプロパティを取得し、それらに変更があればSettingChangedを発火させる
            var notifyProperties = GetType().GetProperties()
                .Where(x => x.PropertyType.GetInterfaces().Any(y => y == typeof(INotifyPropertyChanged)) && x.Name != nameof(SettingChanged));

            foreach (var p in notifyProperties)
            {
                var o = p.GetValue(this) as INotifyPropertyChanged;
                if (o != null)
                {
                    o.PropertyChanged += (s, e) => _settingChanged.ForceNotify();
                }
            }
        }
    }
}
