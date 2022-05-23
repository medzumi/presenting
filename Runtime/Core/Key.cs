using System;

namespace unityPresenting.Core
{
    [Serializable]
    public sealed class Key
    {
        public string Name;
        public string KeyValue;

        public Key()
        {
            KeyValue = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Name} : {KeyValue}";
        }
    }
}