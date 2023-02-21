using System.Windows;
using MahApps.Metro.Controls;

namespace ChoseNumber
{
    /// <summary>
    /// Логика взаимодействия для NotificationDialog.xaml
    /// </summary>
    public partial class NotificationDialog : MetroWindow
    {
      
        public NotificationDialog(string message)
        {
            InitializeComponent();
            textMessage.Text = message;
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
