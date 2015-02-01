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
using Android.Hardware;

namespace SimpleAndroidGame.Framework
{
    class AccelerometerHandler : Java.Lang.Object,ISensorEventListener
    {

        float _accelX;
        float _accelY;
        float _accelZ;

        public float GetX { get { return _accelX; } }

        public float GetY { get { return _accelY; } }

        public float GetZ { get { return _accelZ; } }

        public AccelerometerHandler(Context c)
        {
            SensorManager sm = (SensorManager) c.GetSystemService(Service.SensorService);

            if (sm.GetSensorList(SensorType.Accelerometer).Count != 0)
            {
                Sensor accel = sm.GetSensorList(SensorType.Accelerometer)[0];
                sm.RegisterListener(this,accel,SensorDelay.Game);

            }
        }

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {

        }
		//оптимизайия метода
        public void OnSensorChanged(SensorEvent e)
        {
			IList<float> values = e.Values;

			_accelX = values[0];
            _accelY = values[1];
            _accelZ = values[2];
			IDisposable d = values as IDisposable;
			d.Dispose ();


        }
	

    }
}