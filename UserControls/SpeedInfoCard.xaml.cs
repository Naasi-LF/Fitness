using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MahApps.Metro.IconPacks;

namespace Fitness.UserControls
{
    public partial class SpeedInfoCard : UserControl
    {
        private const double MIN_ANGLE = -150;  // Start angle for speedometer
        private const double MAX_ANGLE = 150;   // End angle for speedometer
        private const int MARK_COUNT = 15;      // Number of marks on speedometer

        public SpeedInfoCard()
        {
            InitializeComponent();
            GenerateSpeedometerMarks();
        }

        private void GenerateSpeedometerMarks()
        {
            var figures = new PathFigureCollection();
            double angleStep = (MAX_ANGLE - MIN_ANGLE) / (MARK_COUNT - 1);
            
            for (int i = 0; i < MARK_COUNT; i++)
            {
                double angle = MIN_ANGLE + (i * angleStep);
                double radians = angle * Math.PI / 180;
                
                // Calculate start and end points for mark lines
                double startX = 32.5 + Math.Cos(radians) * 25;
                double startY = 32.5 + Math.Sin(radians) * 25;
                double endX = 32.5 + Math.Cos(radians) * 30;
                double endY = 32.5 + Math.Sin(radians) * 30;

                var figure = new PathFigure
                {
                    StartPoint = new Point(startX, startY),
                    Segments = new PathSegmentCollection
                    {
                        new LineSegment(new Point(endX, endY), true)
                    }
                };
                figures.Add(figure);
            }

            SpeedometerMarks.Data = new PathGeometry { Figures = figures };
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SpeedInfoCard), 
                new PropertyMetadata("Component", OnTitleChanged));

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SpeedInfoCard card)
            {
                card.TitleText.Text = e.NewValue.ToString().ToUpper();
            }
        }

        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set { SetValue(SubtitleProperty, value); }
        }

        public static readonly DependencyProperty SubtitleProperty =
            DependencyProperty.Register("Subtitle", typeof(string), typeof(SpeedInfoCard),
                new PropertyMetadata("Acceleration", OnSubtitleChanged));

        private static void OnSubtitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SpeedInfoCard card)
            {
                card.SubtitleText.Text = e.NewValue.ToString().ToUpper();
            }
        }

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(SpeedInfoCard),
                new PropertyMetadata(0.0, OnSpeedChanged));

        private static void OnSpeedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SpeedInfoCard card)
            {
                card.SpeedValue.Text = ((double)e.NewValue).ToString("F1");
            }
        }

        public PackIconMaterialKind Icon
        {
            get { return (PackIconMaterialKind)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(PackIconMaterialKind), typeof(SpeedInfoCard),
                new PropertyMetadata(PackIconMaterialKind.Speedometer, OnIconChanged));

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SpeedInfoCard card)
            {
                card.CardIcon.Kind = (PackIconMaterialKind)e.NewValue;
            }
        }

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static readonly DependencyProperty StartColorProperty =
            DependencyProperty.Register("StartColor", typeof(Color), typeof(SpeedInfoCard),
                new PropertyMetadata(Colors.Transparent, OnColorChanged));

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public static readonly DependencyProperty EndColorProperty =
            DependencyProperty.Register("EndColor", typeof(Color), typeof(SpeedInfoCard),
                new PropertyMetadata(Colors.Transparent, OnColorChanged));

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SpeedInfoCard card)
            {
                var gradient = (LinearGradientBrush)card.bgColor.Background;
                gradient.GradientStops[0].Color = card.StartColor;
                gradient.GradientStops[1].Color = card.EndColor;
            }
        }
    }
}
