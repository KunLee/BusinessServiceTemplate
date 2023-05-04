namespace BusinessServiceTemplate.Test.Common
{
    /// <summary>
    /// Used to generate identity incrementing ID's
    /// </summary>
    public class IdGenerator
    {
        private readonly IDictionary<string, int> _namedIds = new Dictionary<string, int>();

        private int _nextId = 1;

        public IdGenerator()
        {

        }

        public IdGenerator(int start)
        {
            _nextId= start;
        }

        /// <summary>
        /// Generates a new ID
        /// </summary>
        public int Next()
        {
            return _nextId++;
        }

        /// <summary>
        /// Generates a new ID, and associates a name to it (allowing future retrieval of ID by name)
        /// </summary>
        public int Next(string name)
        {
            var id = _nextId++;

            _namedIds[name] = id;

            return id;
        }

        /// <summary>
        /// Returns the last generated ID
        /// </summary>
        public int Last()
        {
            return _nextId - 1;
        }

        /// <summary>
        /// Get the ID associated with a name
        /// </summary>
        public int this[string name] => _namedIds[name];
    }
}
