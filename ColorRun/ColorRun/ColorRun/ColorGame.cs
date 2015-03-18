using ColorRun.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ColorRun
{
    public class ColorGame
    {
        private ColorGrid _colorGrid;
        private int _currentDimension = 4;
        private readonly ContentPage _rootPage;
        private readonly Random _random = new Random();
        private readonly RelativeLayout _gameLayout;

        public ColorGrid Grid
        {
            get
            {
                return _colorGrid;
            }
        }

        public ColorGame(ContentPage rootPage)
        {
            _rootPage = rootPage;
            _gameLayout = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            
            _rootPage.Content = _gameLayout;
        }

        public void Play()
        {
            PlayGame();
        }

        private void Box_Clicked(ColorBox colorBox)
        {
            if (colorBox.Position == _colorGrid.ChosenPosition)
            {
                PlayGame();
            }
        }

        private void PlayGame()
        {
            var color = CreateRandomColor();

            if (_currentDimension >= 8)
            {
                _currentDimension = _random.Next(_currentDimension - 3, _currentDimension);
            }

            if (_colorGrid != null)
            {
                _gameLayout.Children.Remove(_colorGrid);
            }
            _colorGrid = new ColorGrid(++_currentDimension, color, color.MultiplyAlpha(0.85f));
            _colorGrid.BoxClick += Box_Clicked;
            _gameLayout.Children.Add(_colorGrid, Constraint.RelativeToParent((parent) =>
            {
                return 0;
            }));
        }

        private Color CreateRandomColor()
        {
            int r = _random.Next(0, 255);
            int g = _random.Next(0, 255);
            int b = _random.Next(0, 255);

            return Color.FromRgba(r, g, b, 255);

        }
    }
}

