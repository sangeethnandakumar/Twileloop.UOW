namespace Twileloop.UOW {
    public class LiteDBConnection {
        public LiteDBConnection(string name, string connectionString) {
            Name = name;
            ConnectionString = connectionString;
        }

        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
