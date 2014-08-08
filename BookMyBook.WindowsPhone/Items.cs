using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace BookMyBook
{
    public class Items : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Title = string.Empty;
        public string Title
        {
            get
            {
                return this._Title;
            }

            set
            {
                if (this._Title != value)
                {
                    this._Title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        private string _Subtitle = string.Empty;
        public string Subtitle
        {
            get
            {
                return this._Subtitle;
            }

            set
            {
                if (this._Subtitle != value)
                {
                    this._Subtitle = value;
                    this.OnPropertyChanged("Subtitle");
                }
            }
        }

        private ImageSource _Image = null;
        public ImageSource Image
        {
            get
            {
                return this._Image;
            }

            set
            {
                if (this._Image != value)
                {
                    this._Image = value;
                    this.OnPropertyChanged("Image");
                }
            }
        }

        public void SetImage(Uri baseUri, String path)
        {
            Image = new BitmapImage(new Uri(baseUri, path));
        }

        private string _Link = string.Empty;
        public string Link
        {
            get
            {
                return this._Link;
            }

            set
            {
                if (this._Link != value)
                {
                    this._Link = value;
                    this.OnPropertyChanged("Link");
                }
            }
        }

        
        private string _Description = string.Empty;
        public string Description
        {
            get
            {
                return this._Description;
            }

            set
            {
                if (this._Description != value)
                {
                    this._Description = value;
                    this.OnPropertyChanged("Description");
                }
            }
        }

        private string _Content = string.Empty;
        public string Content
        {
            get
            {
                return this._Content;
            }

            set
            {
                if (this._Content != value)
                {
                    this._Content = value;
                    this.OnPropertyChanged("Content");
                }
            }
        }
    }

}
