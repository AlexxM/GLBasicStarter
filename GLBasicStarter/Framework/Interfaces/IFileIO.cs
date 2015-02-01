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
using System.IO;
namespace SimpleAndroidGame.Framework.Interfaces
{
    public interface IFileIO
    {
        Stream ReadAsset(string fileName);
         
        Stream ReadFile(string fileName);
        
        Stream WriteFile(string fileName);

    }
}