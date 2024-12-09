using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Fitness.UserControls
{
    public partial class HeartRateWave : UserControl
    {
        private readonly DispatcherTimer _timer;
        private readonly List<Point> _points;
        private readonly Random _random = new Random();
        private const int MaxPoints = 100;
        private double _phase = 0;

        public HeartRateWave()
        {
            InitializeComponent();
            _points = new List<Point>();
            
            // 初始化点集合
            for (int i = 0; i < MaxPoints; i++)
            {
                _points.Add(new Point(i * 3, 20));
            }

            // 设置初始点集合
            UpdateWavePoints();

            // 初始化定时器
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(30)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();

            // 当控件加载完成后设置实际宽度
            Loaded += (s, e) => 
            {
                WaveCanvas.Width = ActualWidth;
                WaveCanvas.Height = ActualHeight;
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 移除第一个点
            _points.RemoveAt(0);
            
            // 添加新点（使用正弦函数和随机值创造更自然的波动）
            _phase += 0.1;
            double baseY = ActualHeight / 2;
            double amplitude = ActualHeight / 4;
            double newY = baseY + Math.Sin(_phase) * amplitude * 0.5 + (_random.NextDouble() - 0.5) * 4;
            
            // 每隔一段时间添加一个心跳波峰
            if (_phase % (2 * Math.PI) < 0.2)
            {
                newY = Math.Max(5, baseY - amplitude);
            }
            else if (_phase % (2 * Math.PI) < 0.4)
            {
                newY = Math.Min(ActualHeight - 5, baseY + amplitude);
            }

            _points.Add(new Point(_points[_points.Count - 1].X + 3, newY));

            // 更新所有点的X坐标
            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new Point(_points[i].X - 3, _points[i].Y);
            }

            UpdateWavePoints();
        }

        private void UpdateWavePoints()
        {
            var pointCollection = new PointCollection(_points);
            WaveLine.Points = pointCollection;
        }
    }
}
