using System.Runtime.InteropServices.WindowsRuntime;
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
        private const int MAX_TIME = 5;
        private const int INIT_DIMENSION = 1;

        private readonly object _locker = new object();

        private ColorGrid _colorGrid;
        private int _currentDimension = INIT_DIMENSION;
        private readonly ContentPage _rootPage;
        private readonly dynamic _random = new Random();
        private readonly dynamic _gameLayout;
        private readonly dynamic _contentView;
        private readonly dynamic _lblHeading;
        private readonly dynamic _lblScore;
        private readonly Label _lblTimer;
        private readonly Button _btnPlay;
        private int _score = 0;
        private int _time = 0;
        private bool _isContinue = true;

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

            _lblHeading = new Label
            {
                Text = "Color Run", //\n(Developed by phanthehien@gmail.com)",
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                LineBreakMode = LineBreakMode.WordWrap,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center
            };

            _lblScore = new Label
            {
                Text = string.Format("Your Scores: {0}", _score),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            _lblTimer = new Label
            {
                Text = string.Format("Time: {0}", MAX_TIME),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            _btnPlay = new Button()
            {
                Text = "Play",
                TextColor = Color.Red,
                WidthRequest = 200,
                BorderColor = Color.Blue,
                BackgroundColor = Color.Silver,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            _btnPlay.Clicked += _btnPlay_Clicked;
            _contentView = new ContentView();

            //_gameLayout = new RelativeLayout()
            //{
            //    HorizontalOptions = LayoutOptions.StartAndExpand,
            //    VerticalOptions = LayoutOptions.StartAndExpand,
            //};


            //_gameLayout.Children.Add(lblHeading, 
            //    Constraint.RelativeToParent((parent) => { return 0; }),
            //    Constraint.RelativeToParent((parent) => { return 0; }));

            //_gameLayout.Children.Add(_contentView, 
            //    null, 
            //    Constraint.RelativeToParent((parent) => { return parent.Height / 4; }));

            //_gameLayout.Children.Add(lblBottom,
            //    Constraint.RelativeToView(_contentView, (layout, view) => { return 0; }),
            //    Constraint.RelativeToView(_contentView, (layout, view) => { return view.Y + view.Height + 5; }));

            _gameLayout = new StackLayout()
            {
                Children = 
                {
                    _lblHeading,
                    _btnPlay,
                    _contentView,
                    _lblTimer,
                    _lblScore 
                }
            };

            _rootPage.Content = _gameLayout;
        }
        public void Play()
        {
            _btnPlay.IsVisible = false;
            ResetGame();
            UpdateGrid();
        }

        private void _btnPlay_Clicked(object sender, EventArgs e)
        {
            Play();
        }

        private bool Timer_Handle()
        {
            lock (_locker)
            {
                _lblTimer.Text = string.Format("Time: {0}", --_time);

                if (_time <= 0)
                {
                    _isContinue = false;
                    //_rootPage.DisplayAlert("Game Over",
                    //    string.Format("Your score: {0}.\nDo you want to share it?", _score), "Yes", "No")
                    //    .ContinueWith(
                    //        (isYes) =>
                    //        {
                    //            if (isYes.Result)
                    //            {
                    //                _btnPlay.IsVisible = true;
                    //            }
                    //            else
                    //            {
                    //                Play();
                    //            }
                    //        }, TaskContinuationOptions.ExecuteSynchronously);

                    _rootPage.DisplayAlert("Game Over",
                        string.Format("Your score: {0}.\nDo you want to share it?", _score), "Replay")
                        .ContinueWith(
                            (isYes) =>
                            {
                                if (isYes.IsCompleted)
                                {
                                    Play();
                                }
                            }, TaskContinuationOptions.ExecuteSynchronously);
                }

                return _isContinue;
            }
        }

        private void ResetGame()
        {
            lock (_locker)
            {
                _time = MAX_TIME;
                _score = 0;
                _currentDimension = INIT_DIMENSION;
                _isContinue = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), Timer_Handle);
            }
        }


        private void Box_Clicked(ColorBox colorBox)
        {
            if (colorBox.Position == _colorGrid.ChosenPosition)
            {
                _score++;
                UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            var color = CreateRandomColor();

            if (_currentDimension >= 8)
            {
                _currentDimension = _random.Next(_currentDimension - 3, _currentDimension);
            }
            _colorGrid = null;
            _colorGrid = new ColorGrid(++_currentDimension, color, color.MultiplyAlpha(0.85f));
            _colorGrid.BoxClick += Box_Clicked;
            _contentView.Content = _colorGrid;

            _lblScore.Text = string.Format("Your Scores: {0}", _score);
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

