using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Fitness.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private SpeedViewModel _speedVM;
        private ChartData _chartData;
        private BoxViewModel _joggingBox;
        private BoxViewModel _heartRateBox;
        private StatusViewModel _statusVM;
        private MuteState _muteState;

        public MuteState MuteState
        {
            get => _muteState;
            set
            {
                _muteState = value;
                OnPropertyChanged();
            }
        }

        public SpeedViewModel SpeedVM
        {
            get => _speedVM;
            set
            {
                _speedVM = value;
                OnPropertyChanged();
            }
        }

        public ChartData ChartData
        {
            get => _chartData;
            set
            {
                _chartData = value;
                OnPropertyChanged();
            }
        }

        public BoxViewModel JoggingBox
        {
            get => _joggingBox;
            set
            {
                _joggingBox = value;
                OnPropertyChanged();
            }
        }

        public BoxViewModel HeartRateBox
        {
            get => _heartRateBox;
            set
            {
                _heartRateBox = value;
                OnPropertyChanged();
            }
        }

        public StatusViewModel StatusVM
        {
            get => _statusVM;
            set
            {
                _statusVM = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            SpeedVM = new SpeedViewModel();
            ChartData = new ChartData();
            JoggingBox = new BoxViewModel("Daily Jogging", "steps", 1000, 10000);
            HeartRateBox = new BoxViewModel("Heart Rate", "bpm", 60, 120);
            StatusVM = new StatusViewModel();
            MuteState = new MuteState();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
