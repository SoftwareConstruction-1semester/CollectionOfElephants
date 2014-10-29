using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.Storage;
using CollectionOfElephants.Annotations;
using CollectionOfElephants.Model;

namespace CollectionOfElephants.ViewModel
{
    class FilterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ElephantModel> AllElephants { get; set; }

        public ObservableCollection<ElephantModel> FilteredElephants { get; set; }

        public String InputTextBox { get; set; }

        public ICommand filterCommand { get; set; }

        public FilterViewModel()
        {
            AllElephants = new ObservableCollection<ElephantModel>();
            FilteredElephants = new ObservableCollection<ElephantModel>();

            // put all elephants in filteredelephant to begin with
            FilteredElephants = AllElephants;

            LoadElephantModels();

            filterCommand = new Common.RelayCommand(FilterElephants);

        }

        private void FilterElephants()
        {
            //executed statements in command blah blah

            //sort the elephant according to the string in 'InputTextBox' property

            FilteredElephants.Clear();
            OnPropertyChanged("FilteredElephants");

        }

        private async void LoadElephantModels()
        {
            //try to load elephants xml from local storage
            StorageFile file = null;
            try
            {
                file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("elephants.xml");
            }
            catch (Exception)
            {
            }

            // if it fails use assets  folder
            if (file == null)
            {
                StorageFolder installationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                string xmlfile = @"Assets\xml\AllElephants.xml";
                file = await installationFolder.GetFileAsync(xmlfile);
            }


            Stream elephantStream = await file.OpenStreamForReadAsync();
            XDocument elephantDocument = XDocument.Load(elephantStream);

            IEnumerable<XElement> elephantList = elephantDocument.Descendants("elephantmodel");

            foreach (XElement xElement in elephantList)
            {
                ElephantModel e = new ElephantModel();
                e.Name = xElement.Element("name").Value;
                e.Zoo = xElement.Element("zoo").Value;
                e.Weight = Convert.ToInt32(xElement.Element("weight").Value);
                e.imageURL = xElement.Element("imageurl").Value;
                e.NumberOfChildren = Convert.ToInt32(xElement.Element("children").Value);
                e.EarSize = xElement.Element("earsize").Value;
                AllElephants.Add(e);
            }
            OnPropertyChanged("AllElephants");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
