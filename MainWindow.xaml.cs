using Fitness.ViewModels;
using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Input;

namespace Fitness
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // 初始化 ViewModel
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // 退出应用程序
        }

        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            // 切换静音状态
            _viewModel.MuteState.ToggleMute();

            // 更新图标
            if (MuteIcon is PackIconMaterial materialIcon)
            {
                materialIcon.Kind = _viewModel.MuteState.GetIcon();
            }
        }

        private void JoggingBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.JoggingBox.IsExpanded = !_viewModel.JoggingBox.IsExpanded;
        }

        private void HeartRateBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _viewModel.HeartRateBox.IsExpanded = !_viewModel.HeartRateBox.IsExpanded;
        }

        private void SpeedInfoCard_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
