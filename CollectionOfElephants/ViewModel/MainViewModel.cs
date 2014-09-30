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
        private ObservableCollection<Elephant> _elephants;
        private Elephant _selectedElephant;

        public MainViewModel()
        {
            _elephants = new ObservableCollection<Elephant>();
            
            Elephant e1 = new Elephant();
            e1.EarSize = "Big";
            e1.Name = "Monty";
            e1.imageURL = @"/Assets/whiteElephant.jpg";

            Elephant e2 = new Elephant();
            e2.EarSize = "Small";
            e2.Name = "Python";
            e2.imageURL = @"/Assets/elephants-9a.jpg";

            _elephants.Add(e1);
            _elephants.Add(e2);

           // SelectedElephant = _elephants[0];
        }

        public Elephant SelectedElephant
        {
            get { return _selectedElephant; }
            set
            {
                _selectedElephant = value;
                OnPropertyChanged("SelectedElephant");
            }
        }

        public ObservableCollection<Elephant> Elephants
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
