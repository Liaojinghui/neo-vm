using System.Numerics;
using Neo.VM.Types;

namespace Neo.VM.ObjectPool
{
    public class PoolManager
    {

        ObjectPool<Integer> integerPool = new ObjectPool<Integer>(() => new Integer(0));

        ObjectPool<Boolean> boolPool = new ObjectPool<Boolean>(() => new Boolean(false));

        static PoolManager? _instance;

        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PoolManager();
                }
                return _instance;
            }
        }

        public PoolManager()
        {
            integerPool.Allocate(10);
            boolPool.Allocate(10);
        }

        public StackItem Reuse(BigInteger value)
        {
            var buffer = integerPool.Dequeue();
            buffer.Value = value;
            return buffer;

        }

        public StackItem Reuse(bool value)
        {
            var buffer = boolPool.Dequeue();
            buffer.Value = value;
            return buffer;
        }

        public void Collect(StackItem item)
        {

            switch (item.Type)
            {
                case StackItemType.Integer:
                    {
                        integerPool.Enqueue((Integer)item);
                        break;
                    }
                case StackItemType.Boolean:
                    {
                        boolPool.Enqueue((Boolean)item);
                        break;
                    }
                case 0:
                    break;
            }
        }
    }
}
