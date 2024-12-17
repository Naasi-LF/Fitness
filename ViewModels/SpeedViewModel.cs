using System.ComponentModel;
using System.Runtime.CompilerServices;
using Fitness.Models;

namespace Fitness.ViewModels
{
    public class SpeedViewModel : INotifyPropertyChanged
    {
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

        public void UpdateAcceleration(SensorData data)
        {
            MaxSpeed = data.AccelX;
            AverageSpeed = data.AccelY;
            MinSpeed = data.AccelZ;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
