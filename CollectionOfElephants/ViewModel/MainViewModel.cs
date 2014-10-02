using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CollectionOfElephants.Annotations;
using CollectionOfElephants.Model;

namespace CollectionOfElephants.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ElephantModel> _elephants;
        private ElephantModel _selectedElephantModel;

        public MainViewModel()
        {
            _elephants = new ObservableCollection<ElephantModel>();
            
            ElephantModel e1 = new ElephantModel();
            e1.EarSize = "Big";
            e1.Name = "Monty";
            e1.imageURL = @"/Assets/whiteElephant.jpg";

            ElephantModel e2 = new ElephantModel();
            e2.EarSize = "Small";
            e2.Name = "Python";
            e2.imageURL = @"/Assets/elephants-9a.jpg";

            _elephants.Add(e1);
            _elephants.Add(e2);

           // SelectedElephantModel = _elephants[0];
        }

        public ElephantModel SelectedElephantModel
        {
            get { return _selectedElephantModel; }
            set
            {
                _selectedElephantModel = value;

                // this line tells the UI (and the rest of the system) that a property changed
                OnPropertyChanged("SelectedElephantModel");
            }
        }

        public ObservableCollection<ElephantModel> Elephants
        {
            get { return _elephants; }
            set { _elephants = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
