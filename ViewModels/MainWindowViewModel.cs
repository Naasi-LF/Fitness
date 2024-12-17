using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Fitness.Services;
using System.Windows;

namespace Fitness.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        private SpeedViewModel _speedVM;
        private ChartData _chartData;
        private BoxViewModel _joggingBox;
        private BoxViewModel _heartRateBox;
        private StatusViewModel _statusVM;
        private MuteState _muteState;
        private SerialPortService? _serialPortService;

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

            InitializeSerialPort();
        }

        private void InitializeSerialPort()
        {
            try
            {
                _serialPortService = new SerialPortService();
                _serialPortService.OnDataReceived += OnSensorDataReceived;
                _serialPortService.OnError += OnSerialPortError;

                if (_serialPortService.Start())
                {
                    MessageBox.Show("串口连接成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化串口失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnSensorDataReceived(Models.SensorData data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                // 更新加速度数据
                SpeedVM.UpdateAcceleration(data);

                // 更新图表数据
                ChartData.UpdateData(data);

                // 更新步数
                JoggingBox.CurrentValue = data.Steps;

                // 更新心率
                HeartRateBox.CurrentValue = data.HeartRate;

                // 更新状态
                StatusVM.UpdateState(data.State);
            });
        }

        private void OnSerialPortError(string errorMessage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(errorMessage, "串口错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            });
        }

        public void Dispose()
        {
            _serialPortService?.Dispose();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
