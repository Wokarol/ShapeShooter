using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Wokarol.MessageSystem;

namespace Tests
{
    public class MessengerTest
    {
        Messenger messenger;

        int callCount = 0;
        int unusedCallCount = 0;
        void IncrementOnTestMessage(TestMessage m) {
            callCount += 1;
        }
        void IncrementOnUnusedTestMessage(UnusedTestMessage m) {
            callCount += 1;
        }

        private struct TestMessage
        {
        }

        private struct UnusedTestMessage
        {
        }

        [SetUp]
        public void BeforeTest() {
            callCount = 0;
            messenger = new Messenger();
        }

        [Test]
        public void _0_Messenger_Can_Be_Created () {
            Assert.NotNull(messenger);
        }

        [Test]
        public void _1_Messenger_Call_Method_Once() {
            messenger.RegisterSubscriberTo<TestMessage>(IncrementOnTestMessage);

            messenger.SendMessage(new TestMessage());

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void _2_Messenger_Calls_Multiple_Methods() {
            messenger.RegisterSubscriberTo<TestMessage>(IncrementOnTestMessage);
            messenger.RegisterSubscriberTo<TestMessage>(IncrementOnTestMessage);
            messenger.RegisterSubscriberTo<TestMessage>(IncrementOnTestMessage);

            messenger.SendMessage(new TestMessage());

            Assert.AreEqual(3, callCount);
        }

        [Test]
        public void _3_Messenger_Clears_Methods() {
            messenger.RegisterSubscriberTo<TestMessage>(IncrementOnTestMessage);
            messenger.RegisterSubscriberTo<TestMessage>(IncrementOnTestMessage);

            messenger.UnRegisterAllSubscribersForObjects(this);

            messenger.SendMessage(new TestMessage());

            Assert.AreEqual(0, callCount);
        }

        [Test]
        public void _4_Messenger_Does_not_Crosscall_Methods() {
            messenger.RegisterSubscriberTo<TestMessage>(IncrementOnTestMessage);
            messenger.RegisterSubscriberTo<TestMessage>(IncrementOnTestMessage);
            messenger.RegisterSubscriberTo<UnusedTestMessage>(IncrementOnUnusedTestMessage);
            messenger.RegisterSubscriberTo<UnusedTestMessage>(IncrementOnUnusedTestMessage);

            messenger.SendMessage(new TestMessage());

            Assert.AreEqual(2, callCount);
            Assert.AreEqual(0, unusedCallCount);
        }
    }
}
