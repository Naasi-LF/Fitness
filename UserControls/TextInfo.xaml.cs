using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fitness.UserControls
{
    /// <summary>
    /// TestInfo.xaml 的交互逻辑
    /// </summary>
    public partial class TextInfo : UserControl
    {
        public TextInfo()
        {
            InitializeComponent();
        }

        public string TextTop
        {
            get { return (string)GetValue(TextTopProperty); }
            set { SetValue(TextTopProperty, value); }
        }

        public static readonly DependencyProperty TextTopProperty =
            DependencyProperty.Register(
                "TextTop",        // 属性名称
                typeof(string),   // 属性类型
                typeof(TextInfo));// 所属类

        public string TextBottom
        {
            get { return (string)GetValue(TextBottomProperty); }
            set { SetValue(TextBottomProperty, value); }
        }

        public static readonly DependencyProperty TextBottomProperty =
            DependencyProperty.Register(
                "TextBottom",      // 属性名称
                typeof(string),    // 属性类型
                typeof(TextInfo),  // 所属类
                new PropertyMetadata(string.Empty)); // 默认值


        public string TextMiddle
        {
            get { return (string)GetValue(TextMiddleProperty); }
            set { SetValue(TextMiddleProperty, value); }
        }

        public static readonly DependencyProperty TextMiddleProperty =
            DependencyProperty.Register(
                "TextMiddle",      // 属性名称
                typeof(string),    // 属性类型
                typeof(TextInfo),  // 所属类
                new PropertyMetadata(string.Empty)); // 默认值


        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(
                "IsActive",        // 属性名称
                typeof(bool),      // 属性类型
                typeof(TextInfo),  // 所属类
                new PropertyMetadata(false)); // 默认值


    }
}
