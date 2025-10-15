using System.Diagnostics;

namespace ImageGenerator
{
    public partial class MainPage : ContentPage
    {
        static private bool _isFavorite;

        private Dictionary<string, string> ImageList = new()
            {
                {"image1", "Man" },
                {"image2", "Bird" },
                {"image3", "Big cat" },
                {"image4", "Autumn road" },
                {"image5", "Flowergirl" }
            };


        private Random random = new();

        public MainPage()
        {
            InitializeComponent();
        }

        private void ImageOnClicked(object? sender, EventArgs e)
        {
            ShowImageAndText();
        }

        private void ShowImageAndText()
        {
            var pairs = ImageList.ElementAt(random.Next(ImageList.Count));

            Debug.WriteLine(pairs.Key + ": " + pairs.Value); // för testning i Output

            string showKey = GetImageFileEnding(pairs.Key); // detta då Windows, men inte till exempel Android, kräver filändelse

            ShowGallery.Source = showKey;

            ImageText.Text = pairs.Value;
        }

        private string GetImageFileEnding(string imageKey)
        {
            #if WINDOWS
            return imageKey + ".jpg";
            #else
            return imageKey;
            #endif
        }

        private void OnFavoriteClicked(object sender, EventArgs e)
        {
            _isFavorite = !_isFavorite;

            if (_isFavorite)
            {
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = "\ue87d",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = Colors.Red
                };
            }
            else
            {
                _favoriteList.Add(_currentImageKey);
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = "\ue87d",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = Colors.Red
                };
            }
        }
    }
}
