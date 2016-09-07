using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Messenger.Controls
{
    /// <summary>
    /// Interaction logic for BorderButton.xaml
    /// </summary>
    public partial class BorderButton : INotifyPropertyChanged
    {
        private static readonly DependencyProperty _contentNormal = DependencyProperty.Register("ContentNormal", typeof(DrawingImage), typeof(BorderButton), new PropertyMetadata(null, null));

        private static readonly DependencyProperty _contentHower = DependencyProperty.Register("ContentHower", typeof(DrawingImage), typeof(BorderButton), new PropertyMetadata(null, null));

        public BorderButton()
        {
            InitializeComponent();
        }
        
        public DrawingImage ContentNormal
        {
            get { return GetValue(_contentNormal) as DrawingImage; }
            set { SetValue(_contentNormal, value); }
        }

        public DrawingImage ContentHower
        {
            get { return GetValue(_contentHower) as DrawingImage; }
            set { SetValue(_contentHower, value); }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
