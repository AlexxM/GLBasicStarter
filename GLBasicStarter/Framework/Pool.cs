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
namespace SimpleAndroidGame.Framework
{
    class Pool<T>  where T : new()
    {
        private Queue<T> _freeObjects;
        private PoolObjectFactory<T> _factory;
        private int _maxSize;


        public Pool(PoolObjectFactory<T> pof,int maxSize)
        {
            _factory = pof;
            _maxSize = maxSize;
            _freeObjects = new Queue<T>(maxSize); 
        }

        public T NewObject()
        {
            if (_freeObjects.Count < _maxSize)
                return _factory.CreateObject();
            else
                return _freeObjects.Dequeue();
        }

        public void Add(T obj)
        {
            if (_freeObjects.Count < _maxSize)
                _freeObjects.Enqueue(obj);
        
        }

    }
}