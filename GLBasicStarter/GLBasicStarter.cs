using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GLBasicStarter
{
    [Activity(Label = "GLBasicStarter", MainLauncher = true, Icon = "@drawable/icon")]
    public class GLBasics : ListActivity
    {
		string[] items = new string[] { "GLSurfaceViewTest","GLTriangleTest",
			"GLColoredTriangleTest","GLTexturedTriangleTest",
			"GLIndexedVerticesTest","GLBlendingTest","GLModelViewMatrixTest"};

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1,items);

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {

            base.OnListItemClick(l, v, position, id);
            string selectedItem = items[position];
            try
            {

                Type t = Type.GetType("GLBasicStarter." + selectedItem);
                StartActivity(new Intent(this, t));
            }
            catch
            {
                Android.Util.Log.Debug("GLBasicStarter", "Incorrect class name!");
            }
        }
    }
}

