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
        private int _currentDimension = 3;
        private readonly ContentPage _rootPage;

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
        }

        public void Play()
        {
            PlayGame();
        }

        private void Box_Clicked(ColorBox colorBox)
        {
            if(colorBox.Position == _colorGrid.ChosenPosition)
            {
                PlayGame();                
            }
        }

        private void PlayGame()
        {
            _colorGrid = new ColorGrid(++_currentDimension, Color.Red, Color.Pink);
            _colorGrid.BoxClick += Box_Clicked;
            _rootPage.Content = _colorGrid;
        }
    }
}
