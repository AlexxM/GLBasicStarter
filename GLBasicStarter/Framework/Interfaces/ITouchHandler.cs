﻿using System;
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
    public interface ITouchHandler : View.IOnTouchListener
    {
        bool IsTouchDown(int pointer);

        int  TouchX(int pointer);

        int TouchY(int pointer);

        List<TouchEvent> GetTouchEvents();
    }
}