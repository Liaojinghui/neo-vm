using System;
using System.Collections.Concurrent;

namespace Neo.VM.ObjectPool
{

    public class ObjectPool<T> where T : class
    {
        private readonly Func<T> _objectFactory;
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();


        public ObjectPool(Func<T> objectFactory)
        {
            _objectFactory = objectFactory;
        }


        public void Allocate(int count)
        {
            for (int i = 0; i < count; i++)
                _queue.Enqueue(_objectFactory());
        }


        public void Enqueue(T obj)
        {
            _queue.Enqueue(obj);
        }

        public T Dequeue()
        {
            T obj;
            return !_queue.TryDequeue(out obj) ? _objectFactory() : obj;
        }
    }
}
