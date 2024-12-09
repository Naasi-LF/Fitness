using LiveCharts.Wpf;
using LiveCharts;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Fitness.ViewModels
{
    public class ChartData : INotifyPropertyChanged
    {
        public SeriesCollection ChartSeries { get; set; }
        private ObservableCollection<string> _labels;
        public ObservableCollection<string> Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }
        private Random random = new Random();

        public ChartData()
        {
            // 初始化 X 轴标签
            Labels = new ObservableCollection<string>(GenerateInitialTimestamps());

            // 定义 Y 轴数据
            ChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Speed",
                    Values = new ChartValues<double>(GenerateInitialValues(5, 50)),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10,
                    StrokeThickness = 2,
                    Fill = null, // 不显示填充区域
                    ScalesYAt = 0 // 使用左侧 Y 轴
                },
                new LineSeries
                {
                    Title = "Heart Rate",
                    Values = new ChartValues<double>(GenerateInitialValues(60, 120)),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 8,
                    StrokeThickness = 2,
                    Fill = null, // 不显示填充区域
                    ScalesYAt = 1 // 使用右侧 Y 轴
                }
            };

            // 启动数据动态更新
            StartDynamicUpdate();
        }

        private List<string> GenerateInitialTimestamps()
        {
            var timestamps = new List<string>();
            var currentTime = DateTime.Now;

            // 生成当前时间之前的 6 个时间戳
            for (int i = 5; i >= 0; i--)
            {
                timestamps.Add(currentTime.AddSeconds(-i).ToString("HH:mm:ss"));
            }

            return timestamps;
        }

        private List<double> GenerateInitialValues(int min, int max)
        {
            var values = new List<double>();
            for (int i = 0; i < 6; i++) // 初始6个数据点
            {
                values.Add(random.Next(min, max)); // 随机值在指定区间内
            }
            return values;
        }

        private void StartDynamicUpdate()
        {
            // 定时器模拟动态更新
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // 每1秒更新一次
            };

            timer.Tick += (sender, args) =>
            {
                // 更新时间戳
                var newTimestamp = DateTime.Now.ToString("HH:mm:ss");
                Labels.RemoveAt(0); // 移除第一个时间戳
                Labels.Add(newTimestamp); // 添加最新时间戳

                foreach (var series in ChartSeries)
                {
                    if (series is LineSeries lineSeries && lineSeries.Values is ChartValues<double> values)
                    {
                        // 根据数据范围更新值
                        if (values.Count > 0)
                            values.RemoveAt(0);

                        // 根据不同的数据集范围生成随机值
                        int min = lineSeries.Title == "Speed" ? 5 : 60;
                        int max = lineSeries.Title == "Speed" ? 50 : 120;

                        values.Add(random.Next(min, max));
                    }
                }
            };

            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
