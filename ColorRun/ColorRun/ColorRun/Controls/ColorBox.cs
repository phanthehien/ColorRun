using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ColorRun.Controls
{
    public class ColorBox : BoxView
    {
        private readonly double _dimension;

        private readonly ColorGrid _grid;
        private readonly int _columnIndex, _rowIndex;

        public ColorGrid Grid
        {
            get
            {
                return _grid;
            }
        }

        public int ColumnIndex
        {
            get
            {
                return _columnIndex;
            }
        }

        public int RowIndex
        {
            get
            {
                return _rowIndex;
            }
        }

        public int Position
        {
            get
            {
                return (_columnIndex * _grid.NumberOfCell + _rowIndex);
            }
        }

        public double Dimension
        {
            get
            {
                return _dimension;
            }
        }



        public Action<ColorBox> BoxClick { get; set; }


        public ColorBox(ColorGrid grid, int rowIndex, int columnIndex, double dimension)
            : base()
        {
            _grid = grid;
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _dimension = dimension;
            WidthRequest = dimension;
            HeightRequest = dimension;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                if (BoxClick != null)
                {
                    BoxClick(this);
                }
            };
            this.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
