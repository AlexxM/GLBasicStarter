using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimpleAndroidGame.Framework.Interfaces
{
	public abstract class Screen
    {
        protected IGame game;

        public Screen(IGame game)
        {
            this.game = game;
        }

        public abstract void Update(float deltaTime);

        public abstract void Present(float deltaTime);

        public abstract void Pause();

        public abstract void Resume();


        public abstract void Dispose();
    
    }
}