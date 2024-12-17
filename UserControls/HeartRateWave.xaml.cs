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
                _points.Add(new Point(i * 3, ActualHeight / 2));
            }

            // 设置初始点集合
            UpdateWavePoints();

            // 初始化定时器
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(30)
            };
            _timer.Tick += Timer_Tick;

            // 当控件加载完成后设置实际宽度并启动定时器
            Loaded += (s, e) => 
            {
                WaveCanvas.Width = ActualWidth;
                WaveCanvas.Height = ActualHeight;
                _timer.Start();
            };

            // 当控件卸载时停止定时器
            Unloaded += (s, e) => _timer.Stop();

            // 当尺寸改变时更新Canvas尺寸
            SizeChanged += (s, e) =>
            {
                WaveCanvas.Width = ActualWidth;
                WaveCanvas.Height = ActualHeight;
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                // 移除第一个点
                _points.RemoveAt(0);
                
                // 添加新点
                _phase += 0.2;
                double baseY = ActualHeight / 2;
                double amplitude = ActualHeight * 0.4;
                
                // 计算新的Y值
                double newY = baseY;
                double phaseNorm = _phase % (2 * Math.PI);

                if (phaseNorm < 0.2)
                {
                    // P波
                    newY = baseY - amplitude * 0.2;
                }
                else if (phaseNorm < 0.3)
                {
                    // Q波
                    newY = baseY + amplitude * 0.1;
                }
                else if (phaseNorm < 0.4)
                {
                    // R波（主波峰）
                    newY = baseY - amplitude * 0.8;
                }
                else if (phaseNorm < 0.5)
                {
                    // S波
                    newY = baseY + amplitude * 0.3;
                }
                else if (phaseNorm < 0.7)
                {
                    // T波
                    newY = baseY - amplitude * 0.2;
                }
                else
                {
                    // 基线
                    newY = baseY + Math.Sin(_phase * 3) * amplitude * 0.05;
                }

                // 添加微小随机波动
                newY += (_random.NextDouble() - 0.5) * (amplitude * 0.02);

                // 确保Y值在有效范围内
                newY = Math.Max(5, Math.Min(ActualHeight - 5, newY));

                // 添加新点
                _points.Add(new Point(_points[_points.Count - 1].X + 3, newY));

                // 更新所有点的X坐标
                for (int i = 0; i < _points.Count; i++)
                {
                    _points[i] = new Point(_points[i].X - 3, _points[i].Y);
                }

                UpdateWavePoints();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wave animation error: {ex.Message}");
            }
        }

        private void UpdateWavePoints()
        {
            if (_points.Count > 0)
            {
                var pointCollection = new PointCollection(_points);
                WaveLine.Points = pointCollection;
            }
        }
    }
}
