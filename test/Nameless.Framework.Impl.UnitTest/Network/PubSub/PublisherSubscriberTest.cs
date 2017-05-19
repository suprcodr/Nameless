using System;
using Xunit;

namespace Nameless.Framework.Network.PubSub {

    public class PublisherSubscriberTest {
        internal class MyMessage {
            public string Topic { get; set; }
        }

        [Fact]
        public void Can_Subscribe_For_Notification() {
            // arrange
            var pubSub = new PublisherSubscriber();
            var topic = string.Empty;
            Action<MyMessage> receiveMessage = message => {
                topic = message.Topic;
            };

            // act
            var subscription = pubSub.Subscribe(receiveMessage);
            
            // assert
            Assert.NotNull(subscription);
        }

        [Fact]
        public void Can_Unsubscribe_For_Notification() {
            // arrange
            var pubSub = new PublisherSubscriber();
            var topic = string.Empty;
            Action<MyMessage> receiveMessage = message => {
                topic = message.Topic;
            };

            // act
            var subscription = pubSub.Subscribe(receiveMessage);
            var unsubscribed = pubSub.Unsubscribe(subscription);

            // assert
            Assert.True(unsubscribed);
        }

        [Fact]
        public void Can_Publish_Notification() {
            // arrange
            var pubSub = new PublisherSubscriber();
            var topic = string.Empty;
            Action<MyMessage> receiveMessage = message => {
                topic = message.Topic;
            };

            // act
            var subscription = pubSub.Subscribe(receiveMessage);
            pubSub.Publish(new MyMessage {
                Topic = "Test"
            });

            // assert
            Assert.Equal("Test", topic);
        }
    }
}