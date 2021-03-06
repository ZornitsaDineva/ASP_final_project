﻿using ContosoUniversity.Data;
using ContosoUniversity.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace ContosoUniversity.Services
{
    public class DocumentService
    {
        private readonly IMongoCollection<Document> _document;

        public DocumentService(IDocumentsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _document = database.GetCollection<Document>(settings.DocumentsCollectionName);
        }

        public List<Document> Get() =>
            _document.Find(document => true).ToList();

        public Document Get(string id) =>
            _document.Find(document => document.DocumentID == id).FirstOrDefault();

        public Document Create(Document document)
        {
            _document.InsertOne(document);
            return document;
        }

        public void Replace(string id, Document documentIn) =>
            _document.ReplaceOne(document => document.DocumentID == id, documentIn);

        public void Update(string id, Document documentIn)
        {
            var filter = Builders<Document>.Filter.Eq("DocumentID", id);

            var update = Builders<Document>.Update.Set("DocumentTitle", documentIn.DocumentTitle);
            update.Set("DocumentDescription", documentIn.DocumentDescription);
            update.Set("DateTime", documentIn.DateTime);

            _document.UpdateOne(filter, update);
        }

        public void Remove(Document documentIn) =>
            _document.DeleteOne(document => document.DocumentID == documentIn.DocumentID);

        public void Remove(string id) =>
            _document.DeleteOne(document => document.DocumentID == id);
    }
}
