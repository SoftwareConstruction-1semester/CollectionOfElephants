﻿using System;
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
using Windows.Devices.Input;
using Windows.Storage;
using Windows.UI.Xaml;
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
        private ICommand _editElephantName;

        public MainViewModel()
        {
            #region create ZooModels
            //_zooModels = new ObservableCollection<ZooModel>();
            //ZooModel z1 = new ZooModel(){ImageUrl = "/Assets/cphElephant.jpg", City = "Copenhagen", Name = "Copenhagen Zoo", AllElephants = new List<ElephantModel>()};
            //ZooModel z2 = new ZooModel() {ImageUrl = "/Assets/odenseElphant.jpg",City = "Odense", Name = "Odense Zoo", AllElephants = new List<ElephantModel>() };
            //_zooModels.Add(z1);
            //_zooModels.Add(z2);

            //Load XML;
            LoadZooModels();

            #endregion
            #region create elephants
            //ElephantModel e1 = new ElephantModel();
            //e1.EarSize = "Big";
            //e1.Name = "Monty";
            //e1.imageURL = "/Assets/whiteElephant.jpg";

            //ElephantModel e2 = new ElephantModel();
            //e2.EarSize = "Small";
            //e2.Name = "Python";
            //e2.imageURL = "/Assets/elephants-9a.jpg";

            //// short way of doing the same as above
            //ElephantModel e3 = new ElephantModel(){EarSize = "small", Name = "Ebbe", NumberOfChildren = 2, Weight = 78, imageURL = ""};

            //ZooModels[0].AllElephants.Add(e1);
            //ZooModels[0].AllElephants.Add(e2);

            //ZooModels[1].AllElephants.Add(e3);

            //Load XML
            LoadElephantModels();

            //SelectedZoo = ZooModels[0];
            #endregion

            _newElephaneModel = new ElephantModel();
            _addNewElephant = new RelayCommand(AddElephant);
            _removeSelectedElephant = new RelayCommand(RemoveElephant);
            //_editElephantName = new RelayCommand(EditElephantNameCommand);
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
                
                //find the correct zoo and add the elephant to it!
                foreach (ZooModel zooModel in ZooModels)
                {
                    if (zooModel.Name.Equals(e.Zoo))
                    {
                        zooModel.Elephants.Add(e);
                    }
                }

            }
            OnPropertyChanged("Elephants");
        }

        /// <summary>
        /// Loads ZooModels
        /// </summary>
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
                ZooModels.Add(new ZooModel(){
                    Name = xElement.Element("name").Value,
                    ImageUrl = xElement.Element("imageurl").Value,
                    Elephants = new List<ElephantModel>()
                });
            }

            OnPropertyChanged("ZooModels");

        }

        //private void EditElephantNameCommand()
        //{
        //    SelectedElephantModel.Name = "New Name";
        //    OnPropertyChanged("SelectedElephantModel");
        //    OnPropertyChanged("SelectedZoo");
        //}

        public ICommand EditElephantName

        {
            get { return _editElephantName; }
            set { _editElephantName = value; }
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
