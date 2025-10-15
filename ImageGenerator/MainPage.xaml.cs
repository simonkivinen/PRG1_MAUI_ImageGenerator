using System.Diagnostics;

namespace ImageGenerator
{
    public partial class MainPage : ContentPage
    {
        private class ImageItem
        {
            public string FileName { get; set; }
            public string Title { get; set; }
            public bool IsFavorite { get; set; }
        }

        private readonly List<ImageItem> _images = new()
        {
            new ImageItem { FileName = "image1", Title = "Man" },
            new ImageItem { FileName = "image2", Title = "Bird" },
            new ImageItem { FileName = "image3", Title = "Big Cat" },
            new ImageItem { FileName = "image4", Title = "Autumn road" },
            new ImageItem { FileName = "image5", Title = "Flowergirl" },
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
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = "\ue87e",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = Colors.Gray
                };
            }
        }
    }
}
