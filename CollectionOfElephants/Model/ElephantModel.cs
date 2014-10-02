using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionOfElephants.Model
{
    class ElephantModel
    {
        private string _earSize;
        private string _name;
        private string _imageUrl;
        private int _numberOfChildren;
        private int _weight;

        public string EarSize
        {
            get { return _earSize; }
            set
            {
                if (value.Length != 0)
                {
                    _earSize = value;
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int NumberOfChildren
        {
            get { return _numberOfChildren; }
            set { _numberOfChildren = value; }
        }

        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public String imageURL
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }
    }
}
