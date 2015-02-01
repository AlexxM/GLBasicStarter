using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SimpleAndroidGame.Framework.Interfaces;
using Android.Content.Res;
namespace SimpleAndroidGame.Framework
{
    [Activity(Label = "My Activity")]
    public class AndroidFileIO : IFileIO
    {

        AssetManager _am;
        string _externalStoragePath;

        public AndroidFileIO(AssetManager am)
        {
            _am = am;
            _externalStoragePath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/";
        
        }

        public Stream ReadAsset(string fileName)
        {
           return _am.Open(fileName);
        }

        public Stream ReadFile(string fileName)
        {
            return new FileStream(_externalStoragePath+fileName,FileMode.Open);
        }

        public Stream WriteFile(string fileName)
        {
            return new FileStream(_externalStoragePath + fileName, FileMode.Create);
        }
    }
}