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
        private ICommand _removeSelectedElephant;
        private ObservableCollection<ZooModel> _zooModels;
        private ZooModel _selectedZoo;

        public MainViewModel()
        {
            //_elephants = new ObservableCollection<ElephantModel>();
            _zooModels = new ObservableCollection<ZooModel>();
            ZooModel z1 = new ZooModel(){ImageUrl = "/Assets/cphElephant.jpg", City = "Copenhagen", Name = "Copenhagen Zoo", Elephants = new ObservableCollection<ElephantModel>()};
            ZooModel z2 = new ZooModel() {ImageUrl = "/Assets/odenseElphant.jpg",City = "Odense", Name = "Odense Zoo", Elephants = new ObservableCollection<ElephantModel>() };
            _zooModels.Add(z1);
            _zooModels.Add(z2);
            #region create elephants
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

            z1.Elephants.Add(e1);
            z1.Elephants.Add(e2);

            z2.Elephants.Add(e3);

            SelectedZoo = z1;
            #endregion

            _newElephaneModel = new ElephantModel();
            _addNewElephant = new RelayCommand(AddElephant);

            _removeSelectedElephant = new RelayCommand(RemoveElephant);
        }

        public ZooModel SelectedZoo
        {
            get { return _selectedZoo; }
            set
            {
                _selectedZoo = value;
                OnPropertyChanged("SelectedZoo");
            }
        }

        public ObservableCollection<ZooModel> ZooModels
        {
            get { return _zooModels; }
            set { _zooModels = value; }
        }

        private void RemoveElephant()
        {
            _elephants.Remove(_selectedElephantModel);
        }

        //executed by the a relaycommand (addNewElephant)
        private void AddElephant()
        {
            _elephants.Add(_newElephaneModel);
            OnPropertyChanged("Elephants");
        }

        public ICommand RemoveSelectedElephant
        {
            get { return _removeSelectedElephant; }
            set { _removeSelectedElephant = value; }
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
