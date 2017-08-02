using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace Nameless.Framework.Data.Generic.Mongo {

    public class PersisterTest {

        [Fact]
        public void Can_Save() {
            // arrange
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase("TempDatabase");
            var persister = new Persister(mongoDatabase);
            var entity = new MyEntity {
                Id = ObjectId.GenerateNewId(),
                Value = "Test"
            };

            // act
            persister.SaveAsync(cancellationToken: CancellationToken.None, progress: NullProgress<int>.Instance, entities: entity);
            var collection = mongoDatabase.GetCollection<MyEntity>(typeof(MyEntity).FullName);
            var item = collection.AsQueryable().First();

            // assert
            Assert.NotNull(item);
            Assert.Equal(entity.Id, item.Id);
            Assert.Equal(entity.Value, item.Value);
        }

        public class MyEntity {
            private object _id;

            public object Id {
                get { return _id; }
                set { _id = value; }
            }

            public string Value { get; set; }
        }
    }
}