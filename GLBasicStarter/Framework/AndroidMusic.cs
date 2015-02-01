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
using Android.Content.Res;
namespace SimpleAndroidGame.Framework
{
    class AndroidMusic : Java.Lang.Object,IMusic,Android.Media.MediaPlayer.IOnCompletionListener
    {
        MediaPlayer _mp=new MediaPlayer();
        bool _isPrepeared;

        public AndroidMusic(AssetFileDescriptor afd)
        {
            _mp.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
            _isPrepeared = true;
        }

        public bool IsLooping()
        {
            return _mp.Looping;
        }

        public bool IsPlaying()
        {
            return _mp.IsPlaying;
        }

        public bool IsStopped()
        {
            return !_isPrepeared;
        }

        public void SetLooping(bool isLooping)
        {
            _mp.Looping = isLooping;
        }

        public void SetVolume(float volume)
        {
            _mp.SetVolume(volume, volume);
        }

        public void Play()
        {
            if (_mp.IsPlaying)
                return;

            lock (this)
            {
                if (_isPrepeared)
                    _mp.Prepare();
                _mp.Start();
            }
        }

        public void Pause()
        {
            _mp.Pause();
        }

        public void Stop()
        {
            _mp.Stop();
            _isPrepeared = false;
        }


        public new void Dispose()
        {
            if (_mp.IsPlaying)
                _mp.Stop();

            _mp.Release();
        }

        public void OnCompletion(MediaPlayer mp)
        {
            lock (this)
            {
                _isPrepeared = false;
            }
        }

    }
}