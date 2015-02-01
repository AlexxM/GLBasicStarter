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
using SimpleAndroidGame.Framework.Interfaces;
using Android.Content.Res;
using Android.Media;

namespace SimpleAndroidGame.Framework
{
    class AndroidAudio : IAudio
    {
        AssetManager _am;
        SoundPool _sp;

        public AndroidAudio(Activity a)
        {
            a.VolumeControlStream = Stream.Music;
            _am = a.Assets;
            _sp = new SoundPool(20,Stream.Music,0);
        }
        
        public IMusic NewMusic(string filename)
        {
            
           return new AndroidMusic(_am.OpenFd(filename)); 
        }

        public ISound NewSound(string filename)
        {
            int soundId = _sp.Load(_am.OpenFd(filename),0);
            return new AndroidSound(_sp,soundId);
        }


    }
}