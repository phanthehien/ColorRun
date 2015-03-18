using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ColorRun.Controls
{
    public class ColorGrid : Grid
    {
        private readonly Color _primaryColor, _secondaryColor;
        private readonly int _numberOfCell;
        private readonly double _boxWidth;
        private readonly int _chosenPosition;

        public Color PrimaryColor
        {
            get
            {
                return _primaryColor;
            }
        }

        public Color SecondaryColor
        {
            get
            {
                return _secondaryColor;
            }
        }

        public int NumberOfCell
        {
            get
            {
                return _numberOfCell;
            }
        }

        public int ChosenPosition
        {
            get
            {
                return _chosenPosition;
            }
        }

        public double BoxWidth
        {
            get
            {
                return _boxWidth;
            }
        }

        public Action<ColorBox> BoxClick { get; set; }

        public ColorGrid(int numberOfCell, Color primaryColor, Color secondaryColor)
            : base()
        {
            _numberOfCell = numberOfCell;
            _primaryColor = primaryColor;
            _secondaryColor = secondaryColor;
            _boxWidth = 30;

            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            Padding = 5;
            BackgroundColor = Color.White;
            ColumnSpacing = 5;
            RowSpacing = 5;

            _chosenPosition = RandomPosition();

            for (int i = 0; i < numberOfCell; i++)
            {
                for (int j = 0; j < numberOfCell; j++)
                {
                    var boxView = new ColorBox(this, i, j, 30);
                    boxView.Color = boxView.Position == _chosenPosition ? secondaryColor : primaryColor;
                    boxView.BoxClick = ColorBox_Click;
                    this.Children.Add(boxView, i, j);
                }
            }
        }

        private void ColorBox_Click(ColorBox colorBox)
        {
            if (BoxClick != null)
            {
                BoxClick(colorBox);
            }
        }

        private int RandomPosition()
        {
            var random = new Random();
            return random.Next(0, _numberOfCell * _numberOfCell - 1);

        }
    }
}
