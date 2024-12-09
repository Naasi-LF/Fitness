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
        
        private double _maxSpeed;
        private double _avgSpeed;
        private double _minSpeed;

        public double MaxSpeed
        {
            get => _maxSpeed;
            private set
            {
                if (_maxSpeed != value)
                {
                    _maxSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        public double AverageSpeed
        {
            get => _avgSpeed;
            private set
            {
                if (_avgSpeed != value)
                {
                    _avgSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        public double MinSpeed
        {
            get => _minSpeed;
            private set
            {
                if (_minSpeed != value)
                {
                    _minSpeed = value;
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
            
            // 初始化速度值
            UpdateSpeeds();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateSpeeds();
        }

        private void UpdateSpeeds()
        {
            // 生成15-35之间的随机速度
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
