using System;
using System.Windows.Controls;
using System.Windows;

namespace Fitness.UserControls
{
    public partial class UserLogCard : UserControl
    {
        public UserLogCard()
        {
            InitializeComponent();
            //Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 当前时间戳格式
        }

        public string FullName
        {
            get { return (string)GetValue(FullNameProperty); }
            set { SetValue(FullNameProperty, value); }
        }

        public static readonly DependencyProperty FullNameProperty =
            DependencyProperty.Register("FullName", typeof(string), typeof(UserLogCard), new PropertyMetadata(string.Empty));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(UserLogCard), new PropertyMetadata(string.Empty));

        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(string), typeof(UserLogCard), new PropertyMetadata(string.Empty));

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(string), typeof(UserLogCard), new PropertyMetadata(string.Empty));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(UserLogCard), new PropertyMetadata(false));
    }
}
