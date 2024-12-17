using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Fitness.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
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
            
            UpdateTime();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        public void UpdateState(string state)
        {
            if (string.IsNullOrWhiteSpace(state)) return;

            switch (state.ToLower())
            {
                case "stationary":
                case "static":
                    IsStationaryHighlighted = true;
                    IsMovingHighlighted = false;
                    break;
                case "moving":
                case "move":
                    IsStationaryHighlighted = false;
                    IsMovingHighlighted = true;
                    break;
                default:
                    IsStationaryHighlighted = false;
                    IsMovingHighlighted = false;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 