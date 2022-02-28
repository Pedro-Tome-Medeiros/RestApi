namespace RestApi.Data
{
    public class MsqlConfiguration
    {
        public MsqlConfiguration(string connectionString) => ConnectionString = connectionString;

        public string ConnectionString { get; set; }
    }
}
