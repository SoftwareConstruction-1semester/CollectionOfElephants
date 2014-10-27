using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using CollectionOfElephants.Annotations;
using CollectionOfElephants.Model;

namespace CollectionOfElephants.ViewModel
{
    class FilterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ElephantModel> Elephants { get; set; }

        public FilterViewModel()
        {
            Elephants = new ObservableCollection<ElephantModel>();
            LoadElephantModels();
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
                string xmlfile = @"Assets\xml\Elephants.xml";
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
                Elephants.Add(e);
            }
            OnPropertyChanged("Elephants");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
