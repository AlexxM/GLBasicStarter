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
using Android.Media;
namespace SimpleAndroidGame.Framework
{
    class AndroidSound : ISound
    {
        int _soundId;
        SoundPool _sp;

        public AndroidSound(SoundPool sp,int soindId)
        {
            _soundId = soindId;
            _sp = sp;
        
        }
        
        public void Play(float volume)
        {
            _sp.Play(_soundId, volume, volume, 1, 0, 1.0f);
        }

        public void Dispose()
        {
            _sp.Unload(_soundId);
        }
    }
}