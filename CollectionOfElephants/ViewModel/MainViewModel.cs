using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CollectionOfElephants.Annotations;
using CollectionOfElephants.Model;

namespace CollectionOfElephants.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ElephantModel> _elephants;
        private ElephantModel _selectedElephantModel;
        private ElephantModel _newElephaneModel;
        private ICommand _addNewElephant;

        public MainViewModel()
        {
            _elephants = new ObservableCollection<ElephantModel>();
   
            ElephantModel e1 = new ElephantModel();
            e1.EarSize = "Big";
            e1.Name = "Monty";
            e1.imageURL = "/Assets/whiteElephant.jpg";

            ElephantModel e2 = new ElephantModel();
            e2.EarSize = "Small";
            e2.Name = "Python";
            e2.imageURL = "/Assets/elephants-9a.jpg";

            // short way of doing the same as above
            ElephantModel e3 = new ElephantModel(){EarSize = "small", Name = "Ebbe", NumberOfChildren = 2, Weight = 78, imageURL = ""};

            _elephants.Add(e1);
            _elephants.Add(e2);
            _elephants.Add(e3);

            _newElephaneModel = new ElephantModel();
            _addNewElephant = new RelayCommand(AddElephant);


        }
        //executed by the a relaycommand (addNewElephant)
        private void AddElephant()
        {
            _elephants.Add(_newElephaneModel);
            OnPropertyChanged("Elephants");
        }

        public ICommand AddNewElephant
        {
            get { return _addNewElephant; }
            set { _addNewElephant = value; }
        }

        public ElephantModel NewElephaneModel
        {
            get { return _newElephaneModel; }
            set { _newElephaneModel = value; }
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
