using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace Fitness.ViewModels
{
    public class SpeedViewModel : INotifyPropertyChanged
    {
        private SerialPort _serialPort;

        private double _xComponent;
        private double _yComponent;
        private double _zComponent;

        public double Xcomponent
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

        public double Ycomponent
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

        public double Zcomponent
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
            // 初始化串口
            _serialPort = new SerialPort("COM7", 9600)
            {
                ReadTimeout = 500,
                NewLine = "\r\n"     // 根据你的设备输出行尾符进行设置
            };

            try
            {
                _serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to open serial port: " + ex.Message);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                try
                {
                    // 从串口读取一行数据
                    string line = _serialPort.ReadLine().Trim();

                    // 数据格式: "X: 0.19 g, Y: -0.05 g, Z: 1.08 g"
                    string[] parts = line.Split(',');

                    double xVal = ParseAcceleration(parts[0]);
                    double yVal = ParseAcceleration(parts[1]);
                    double zVal = ParseAcceleration(parts[2]);

                    // 使用WPF主线程进行UI更新
                    Application.Current?.Dispatcher.Invoke(() =>
                    {
                        Xcomponent = xVal;
                        Ycomponent = yVal;
                        Zcomponent = zVal;
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading/parsing serial data: " + ex.Message);
                }
            }
        }

        private double ParseAcceleration(string segment)
        {
            // segment 示例: "X: 0.19 g"
            segment = segment.Trim();
            int colonIndex = segment.IndexOf(':');
            int gIndex = segment.IndexOf('g');
            if (colonIndex < 0 || gIndex < 0) throw new FormatException("Invalid data format: " + segment);

            string valueStr = segment.Substring(colonIndex + 1, gIndex - (colonIndex + 1)).Trim();
            return double.Parse(valueStr);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
