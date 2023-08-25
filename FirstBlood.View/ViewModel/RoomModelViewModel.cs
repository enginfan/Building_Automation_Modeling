using FirstBlood.View.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FirstBlood.View.ViewModel
{
    public class RoomModelViewModel
    {

        private ObservableCollection<RoomModel> _models = new ObservableCollection<RoomModel>();
        public ObservableCollection<RoomModel> Models
        {
            get { return _models; }
            set
            {
                if (_models.Count != value.Count)
                    _models = value;
            }
        }

        public RoomModelViewModel(string type)
        {
            if (type == "宿舍单元")
                Models = GetDormitoryModels();
        }

        private ObservableCollection<RoomModel> GetDormitoryModels()
        {
            var models = new ObservableCollection<RoomModel>();
            var room0 = new RoomModel();
            room0.Name = "房间A";
            room0.RoomType = Core.RoomType.A;
            room0.Id = 0;
            room0.Thumbnail = new BitmapImage(new Uri("pack://application:,,,/FirstBlood.View;component/Resources/房间A.png"));
            models.Add(room0);
            var room1 = new RoomModel();
            room1.Name = "房间B";
            room1.RoomType = Core.RoomType.B;
            room1.Id = 1;
            room1.Thumbnail = new BitmapImage(new Uri("pack://application:,,,/FirstBlood.View;component/Resources/房间B.png"));
            models.Add(room1);
            var room2 = new RoomModel();
            room2.Name = "房间C";
            room2.RoomType = Core.RoomType.C;
            room2.Id = 2;
            room2.Thumbnail = new BitmapImage(new Uri("pack://application:,,,/FirstBlood.View;component/Resources/房间C.png"));
            models.Add(room2);
            var room3 = new RoomModel();
            room3.Name = "房间D";
            room3.RoomType = Core.RoomType.D;
            room3.Id = 3;
            room3.Thumbnail = new BitmapImage(new Uri("pack://application:,,,/FirstBlood.View;component/Resources/房间D.png"));
            models.Add(room3);

            return models;
        }

    }
}
