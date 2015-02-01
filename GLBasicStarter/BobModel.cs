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

namespace GLBasicStarter
{
	class BobModel
	{
		const float UPDATE_TIME = 50;
		float _geltaTickTime=0;
		static Random _rnd =new Random();
		public int dx;
		public int dy;
		int _dirX;
		int _dirY;
		public int grad;


		public BobModel()
		{
			dx = (int)(_rnd.NextDouble () * 540);
			dy = (int)(_rnd.NextDouble () * 960);
			_dirX = 25;
			_dirY = 25;
			grad = _rnd.Next (360);
		}

		public void Update(float deltaTime)
		{
			_geltaTickTime += deltaTime;
			if (_geltaTickTime >= UPDATE_TIME) 
			{
				_geltaTickTime = 0;


				dx = dx + _dirX;
				dy = dy + _dirY;
				grad += 10;
				if (dx < 0) {
					_dirX = -_dirX;
					dx = 0;
				}

				if (dx > 540) {
					_dirX = -_dirX;
					dx = 540;
				}

				if (dy > 960) {
					_dirY = -_dirY;
					dy = 960;
				}

				if (dy < 0) {
					_dirY = -_dirY;
					dy = 0;
				
				}

				if (grad > 360) {
					grad = 0;
				} 
			}
		}
	
	}
}

