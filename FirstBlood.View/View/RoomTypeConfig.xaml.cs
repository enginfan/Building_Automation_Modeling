using FirstBlood.Core;
using FirstBlood.View.Model;
using FirstBlood.View.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace FirstBlood.View.View
{
    /// <summary>
    /// RoomTypeConfig.xaml 的交互逻辑
    /// </summary>
    public partial class RoomTypeConfig : System.Windows.Window
    {
        private static RoomTypeConfig _instance;
        public static RoomTypeConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RoomTypeConfig();
                    return _instance;
                }
                return _instance;
            }
        }

        private RoomTypeConfig()
        {
            InitializeComponent();
            unitA.DataContext = new RoomModelViewModel("宿舍单元");
        }

        public EventHandler<RoomEventArgs> Create;

        public RoomEventArgs Args = new RoomEventArgs();
        private void a_Checked(object sender, RoutedEventArgs e)
        {
            CheckedItem.AChecked = true;
        }

        private void w_Checked(object sender, RoutedEventArgs e)
        {
            CheckedItem.WChecked = true;
        }

        private void el_Checked(object sender, RoutedEventArgs e)
        {
            CheckedItem.EChecked = true;
        }

        private void h_Checked(object sender, RoutedEventArgs e)
        {
            CheckedItem.HChecked = true;
        }

        private void a_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckedItem.AChecked = false;
        }

        private void w_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckedItem.WChecked = false;
        }

        private void el_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckedItem.EChecked = false;
        }

        private void h_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckedItem.HChecked = false;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listBox = (ListBox)sender;
            var room = listBox.SelectedItem as Model.RoomModel;
            Args.Type = room.RoomType;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Args.Angle = angle.SelectedIndex == 0 ? 0 : 180;
            RoleType role = RoleType.NONE;
            if (a.IsChecked == true)
            {
                role |= RoleType.A;
            }

            if (w.IsChecked == true)
            {
                role |= RoleType.W;
            }
            if (h.IsChecked == true)
            {
                role |= RoleType.H;
            }

            if (el.IsChecked == true)
            {
                role |= RoleType.E;
            }
            Args.Role = role;
            Create?.Invoke(sender, Args);
        }
        
    }
}
