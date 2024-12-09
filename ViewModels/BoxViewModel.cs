using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Fitness.ViewModels
{
    public class BoxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly DispatcherTimer _timer;
        private readonly Random _random = new Random();

        private bool _isExpanded;
        private int _value;
        private string _title;
        private string _unit;

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public int Value
        {
            get => _value;
            private set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        public BoxViewModel(string title, string unit, int minValue, int maxValue)
        {
            Title = title;
            Unit = unit;
            IsExpanded = false;

            // 初始化定时器更新数据
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) => Value = _random.Next(minValue, maxValue);
            _timer.Start();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
