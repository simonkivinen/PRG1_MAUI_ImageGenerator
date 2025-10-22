using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ImageGenerator
{
    public partial class MainPage : ContentPage
    {
        private class ImageItem
        {
            public string FileName { get; set; }
            public string Title { get; set; }
        }

        private readonly List<ImageItem> _images = new()
        {
            new ImageItem { FileName = "image1", Title = "Man" },
            new ImageItem { FileName = "image2", Title = "Bird" },
            new ImageItem { FileName = "image3", Title = "Big Cat" },
            new ImageItem { FileName = "image4", Title = "Autumn road" },
            new ImageItem { FileName = "image5", Title = "Flowergirl" },
            new ImageItem { FileName = "image6", Title = "Far you have comed warior" },
            new ImageItem { FileName = "image7", Title = "Robots in descise" },
            new ImageItem { FileName = "image8", Title ="Rust"},
            new ImageItem { FileName = "image9", Title = "coding joke"},
            new ImageItem { FileName = "image10", Title ="Feer will keep them inline"},
        };

        private readonly List<string> _favoriteList = new();
        private readonly Stack<string> _recentFavorites = new();
        private string _currentImageKey;


        private Random random = new();

        public MainPage()
        {
            InitializeComponent();
            LastFavoriteButton.IsEnabled = false;

        }
        

        private void ImageOnClicked(object? sender, EventArgs e)
        {
            ShowImageAndText();
        }

        private void ShowImageAndText()
        {
            var item = _images[random.Next(_images.Count)];
            _currentImageKey = item.FileName;

            string showKey = GetImageFileEnding(item.FileName);

            ShowGallery.Source = showKey;
            ImageText.Text = item.Title;

            UpdatefavoriteIcon();
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
            if (string.IsNullOrEmpty(_currentImageKey))
                return;

            bool isFavorite = _favoriteList.Contains(_currentImageKey);

            if (isFavorite)
            {
                _favoriteList.Remove(_currentImageKey);
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = "\ue87e",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = Colors.Grey
                };
            }
            else
            {
                _favoriteList.Add(_currentImageKey);
                _recentFavorites.Push(_currentImageKey);
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = "\ue87d",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = Colors.Red
                };
                Debug.WriteLine($"Senast Favorit: {_recentFavorites.Peek()}");
            }
            LastFavoriteButton.IsEnabled = _favoriteList.Count > 0;
        }
        private void UpdatefavoriteIcon()
        {
            if (string.IsNullOrEmpty(_currentImageKey))
                return;
            bool isFavorite = _favoriteList.Contains(_currentImageKey);

            if (isFavorite)
            {
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = isFavorite ? "\ue87d" : "\ue87e",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = isFavorite ? Colors.Red : Colors.Grey
                };
            }
            else
            {
                FavoriteButton.Source = new FontImageSource
                {
                    Glyph = "\ue87e",
                    FontFamily = "MaterialIcons",
                    Size = 32,
                    Color = Colors.Grey
                };
            }
        }

        private void ShowLastFavorite(object sender, EventArgs e)
        {
            if (_recentFavorites.Count > 0)
            {
                string lastFav = _recentFavorites.Peek();
                Debug.WriteLine($"Senast favorit: {lastFav}");
                _currentImageKey = lastFav;
                ShowGallery.Source = GetImageFileEnding(lastFav);
                ImageText.Text = _images.First(x => x.FileName == lastFav).Title;
                UpdatefavoriteIcon();
            }
        }
    }
}
