using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Fitness.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        private readonly Random _random = new Random();
        private readonly DispatcherTimer _timer;
        private bool _isStationaryHighlighted;
        private bool _isMovingHighlighted;
        private string _currentTime;

        public string CurrentTime
        {
            get => _currentTime;
            private set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsStationaryHighlighted
        {
            get => _isStationaryHighlighted;
            private set
            {
                if (_isStationaryHighlighted != value)
                {
                    _isStationaryHighlighted = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsMovingHighlighted
        {
            get => _isMovingHighlighted;
            private set
            {
                if (_isMovingHighlighted != value)
                {
                    _isMovingHighlighted = value;
                    OnPropertyChanged();
                }
            }
        }

        public StatusViewModel()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            
            // 初始状态
            UpdateHighlight();
            UpdateTime();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateHighlight();
            UpdateTime();
        }

        private void UpdateTime()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        private void UpdateHighlight()
        {
            // 随机选择一个状态高亮
            bool highlightStationary = _random.Next(2) == 0;
            
            // 确保状态确实发生了变化
            if (IsStationaryHighlighted != highlightStationary)
            {
                IsStationaryHighlighted = highlightStationary;
                IsMovingHighlighted = !highlightStationary;
            }
            else
            {
                // 如果状态没有变化，强制切换
                IsStationaryHighlighted = !IsStationaryHighlighted;
                IsMovingHighlighted = !IsMovingHighlighted;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 