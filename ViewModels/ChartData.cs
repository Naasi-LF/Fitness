using LiveCharts.Wpf;
using LiveCharts;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using Fitness.Models;
using System.Windows.Media;

namespace Fitness.ViewModels
{
    public class ChartData : INotifyPropertyChanged
    {
        private SeriesCollection? _chartSeries;
        private ObservableCollection<string>? _labels;
        private readonly int _maxDataPoints = 6;
        private readonly double _minTemperature = 20;
        private readonly double _maxTemperature = 40;
        private readonly double _minHeartRate = 40;
        private readonly double _maxHeartRate = 200;

        public SeriesCollection ChartSeries
        {
            get => _chartSeries ?? new SeriesCollection();
            private set
            {
                _chartSeries = value;
                OnPropertyChanged(nameof(ChartSeries));
            }
        }

        public ObservableCollection<string> Labels
        {
            get => _labels ?? new ObservableCollection<string>();
            private set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        public ChartData()
        {
            // 初始化 X 轴标签
            Labels = new ObservableCollection<string>(GenerateInitialTimestamps());

            // 定义 Y 轴数据
            ChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Temperature",
                    Values = new ChartValues<double>(),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 8,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(255, 99, 71)), // 温度曲线颜色
                    Fill = null,
                    ScalesYAt = 0
                },
                new LineSeries
                {
                    Title = "Heart Rate",
                    Values = new ChartValues<double>(),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 6,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromRgb(106, 90, 205)), // 心率曲线颜色
                    Fill = null,
                    ScalesYAt = 1
                }
            };

            // 初始化数据点
            InitializeDataPoints();
        }

        private void InitializeDataPoints()
        {
            for (int i = 0; i < _maxDataPoints; i++)
            {
                ((ChartValues<double>)ChartSeries[0].Values).Add(_minTemperature);
                ((ChartValues<double>)ChartSeries[1].Values).Add(_minHeartRate);
            }
        }

        private List<string> GenerateInitialTimestamps()
        {
            var timestamps = new List<string>();
            var currentTime = DateTime.Now;

            for (int i = _maxDataPoints - 1; i >= 0; i--)
            {
                timestamps.Add(currentTime.AddSeconds(-i).ToString("HH:mm:ss"));
            }

            return timestamps;
        }

        public void UpdateData(SensorData? data)
        {
            if (data == null) return;

            // 更新时间戳
            var newTimestamp = DateTime.Now.ToString("HH:mm:ss");
            Labels.RemoveAt(0);
            Labels.Add(newTimestamp);

            // 限制温度范围
            double temperature = Math.Max(_minTemperature, Math.Min(_maxTemperature, data.Temperature));
            var temperatureValues = (ChartValues<double>)ChartSeries[0].Values;
            temperatureValues.RemoveAt(0);
            temperatureValues.Add(temperature);

            // 限制心率范围
            double heartRate = Math.Max(_minHeartRate, Math.Min(_maxHeartRate, data.HeartRate));
            var heartRateValues = (ChartValues<double>)ChartSeries[1].Values;
            heartRateValues.RemoveAt(0);
            heartRateValues.Add(heartRate);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Func<double, string> TemperatureFormatter { get; } = value => value.ToString("F2");
    }
}
