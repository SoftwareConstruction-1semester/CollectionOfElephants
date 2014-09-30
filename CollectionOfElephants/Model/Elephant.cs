using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionOfElephants.Model
{
    class Elephant
    {
        private string _earSize;
        private string _name;
        private string _imageUrl;

        public string EarSize
        {
            get { return _earSize; }
            set { _earSize = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
