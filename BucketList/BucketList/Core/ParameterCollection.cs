namespace BucketList.Core
{
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class ParameterCollection
    {
        private readonly List<string> _internalCollection;

        public ParameterCollection()
        {
            this._internalCollection = new List<string>();
        }

        public void Add(string param)
        {
            this._internalCollection.Add(param);
        }

        public string this[int index] => this._internalCollection[index];
    }
}