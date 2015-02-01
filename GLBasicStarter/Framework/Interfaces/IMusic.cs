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
    public interface IMusic : IDisposable
    {

        bool IsLooping();

        bool IsPlaying();

        bool IsStopped();
        
        void SetLooping(bool isLooping);

        void SetVolume(float volume);
        
        void Play();

        void Pause();
        
        void Stop();
    }


}