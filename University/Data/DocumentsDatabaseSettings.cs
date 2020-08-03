

namespace ContosoUniversity.Data
{
    public class DocumentsDatabaseSettings : IDocumentsDatabaseSettings
    {
        public string DocumentsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDocumentsDatabaseSettings
    {
        string DocumentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }   
}
