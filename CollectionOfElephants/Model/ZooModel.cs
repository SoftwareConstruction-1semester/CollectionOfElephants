using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionOfElephants.Model;

namespace CollectionOfElephants.ViewModel
{
    class ZooModel
    {
        private String _imageUrl;
        public String Name { get; set; }
        public String City { get; set; }
        public ObservableCollection<ElephantModel> Elephants { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }

        public String ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }
    }
}
