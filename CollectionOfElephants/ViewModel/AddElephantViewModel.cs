using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
    class AddElephantViewModel : INotifyPropertyChanged
    {
        private ElephantModel _newElephant;
        private ICommand _addNewElephant;
        private ObservableCollection<ZooModel> _zooModels;
        private ZooModel _selectedZooModel;

        public AddElephantViewModel()
        {
            _newElephant = new ElephantModel();
            _addNewElephant = new RelayCommand(AddElephantCommand);


            ZooModels = new ObservableCollection<ZooModel>();
            LoadZooModels();
            //SelectedZooModel = ZooModels[0];
        }

        public ObservableCollection<ZooModel> ZooModels
        {
            get { return _zooModels; }
            set { _zooModels = value; }
        }

        public ZooModel SelectedZooModel
        {
            get { return _selectedZooModel; }
            set { _selectedZooModel = value; }
        }

        private async void AddElephantCommand()
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


           

            Stream LoadStream = await file.OpenStreamForReadAsync();
            XDocument elephantDocument = XDocument.Load(LoadStream);
            LoadStream.Dispose();
            // create new elephant xml
            XElement elephantlist = elephantDocument.Element("elephantmodels");

            // add new elephant to xml
            XElement elephant = new XElement("elephantmodel");
            elephant.Add(new XElement("name", NewElephant.Name));
            elephant.Add(new XElement("earsize", NewElephant.EarSize));
            elephant.Add(new XElement("children", NewElephant.NumberOfChildren));
            elephant.Add(new XElement("weight", NewElephant.Weight));
            elephant.Add(new XElement("zoo", SelectedZooModel.Name));
            elephant.Add(new XElement("imageurl", ""));

            elephantlist.LastNode.AddAfterSelf(elephant);

            
            //set savefile to null
            StorageFile saveFile = null;

            //try to create the save file
            try
            {
                saveFile = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("elephants.xml");
            }
            catch (Exception)
            { 
            }

            // if it fails read the file already exists
            if (saveFile == null)
            {
                saveFile = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("elephants.xml");
            }
            
            
            
            Stream saveStream = await saveFile.OpenStreamForWriteAsync();
            elephantDocument.Save(saveStream);
            await saveStream.FlushAsync();
            saveStream.Dispose();
        }

        // this method is copied from mainviewmodel - think of something clever to do
        private async void LoadZooModels()
        {
            StorageFolder installationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            string xmlfile = @"Assets\xml\ZooModels.xml";
            StorageFile file = await installationFolder.GetFileAsync(xmlfile);

            Stream zooStream = await file.OpenStreamForReadAsync();
            XDocument zooModelDocument = XDocument.Load(zooStream);

            IEnumerable<XElement> zooModelsList = zooModelDocument.Descendants("zoomodel");

            ZooModels = new ObservableCollection<ZooModel>();

            foreach (XElement xElement in zooModelsList)
            {
                ZooModels.Add(new ZooModel()
                {
                    Name = xElement.Element("name").Value,
                    ImageUrl = xElement.Element("imageurl").Value,
                    Elephants = new List<ElephantModel>()
                });
            }

            OnPropertyChanged("ZooModels");

        }

        public ElephantModel NewElephant
        {
            get { return _newElephant; }
            set { _newElephant = value; }
        }

        public ICommand AddNewElephant
        {
            get { return _addNewElephant; }
            set { _addNewElephant = value; }
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
