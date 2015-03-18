using ColorRun.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace ColorRun.Pages
{
    public class FloorPage : ContentPage
    {
        private ColorGame _game;

        public FloorPage()
        {
            _game = new ColorGame(this);
            //_game.Play();
        }
    }
}
