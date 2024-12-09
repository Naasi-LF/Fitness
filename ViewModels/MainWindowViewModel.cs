using System.ComponentModel;

namespace Fitness.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MuteState MuteState { get; }
        public ChartData ChartData { get; }
        public BoxViewModel JoggingBox { get; }
        public BoxViewModel HeartRateBox { get; }
        public SpeedViewModel SpeedVM { get; }

        public MainWindowViewModel()
        {
            MuteState = new MuteState();
            ChartData = new ChartData();
            JoggingBox = new BoxViewModel("Daily Jogging", "steps", 1000, 10000);
            HeartRateBox = new BoxViewModel("Heart Rate", "bpm", 60, 120);
            SpeedVM = new SpeedViewModel();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
