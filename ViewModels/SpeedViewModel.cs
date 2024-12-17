using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Fitness.ViewModels
{
    public class SpeedViewModel : INotifyPropertyChanged
    {
        private readonly Random _random = new Random();
        private readonly DispatcherTimer _timer;
        
        private double _xComponent;
        private double _yComponent;
        private double _zComponent;

        public double MaxSpeed
        {
            get => _xComponent;
            private set
            {
                if (_xComponent != value)
                {
                    _xComponent = value;
                    OnPropertyChanged();
                }
            }
        }

        public double AverageSpeed
        {
            get => _yComponent;
            private set
            {
                if (_yComponent != value)
                {
                    _yComponent = value;
                    OnPropertyChanged();
                }
            }
        }

        public double MinSpeed
        {
            get => _zComponent;
            private set
            {
                if (_zComponent != value)
                {
                    _zComponent = value;
                    OnPropertyChanged();
                }
            }
        }

        public SpeedViewModel()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            
            // 初始化分量值
            UpdateSpeeds();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateSpeeds();
        }

        private void UpdateSpeeds()
        {
            // 生成15-35之间的随机分量值
            MaxSpeed = _random.NextDouble() * 20 + 15;
            AverageSpeed = _random.NextDouble() * 20 + 15;
            MinSpeed = _random.NextDouble() * 20 + 15;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
