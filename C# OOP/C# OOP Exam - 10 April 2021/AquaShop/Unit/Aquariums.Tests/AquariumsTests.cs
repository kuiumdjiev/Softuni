using NUnit.Framework;

namespace Aquariums.Tests
{
    using System;
    [TestFixture]
    public class AquariumsTests
    {
        private Aquarium aquarium;

        [SetUp]
        public void SetUp()
        {
            this.aquarium = new Aquarium("Name", 3);
        }
        [Test]
        public void WorkOK()
        {
            string expectedName = "Name";
            int expectedCapacity = 3;
            int expectedFishCount = 0;

            var actualName = this.aquarium.Name;
            var actualCapacity = this.aquarium.Capacity;
            var actualFishCount = this.aquarium.Count;

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedCapacity, actualCapacity);
            Assert.AreEqual(expectedFishCount, actualFishCount);

        }

        [TestCase("")]
        [TestCase(null)]
        public void InvalidName(string input)
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(input, 3));
        }

        [Test]
        public void InvalidCapacit()
        {
            Assert.Throws<ArgumentException>(() => new Aquarium("Name", -3));
        }

        [Test]
        public void AddOK()
        {
            var fish = new Fish("Fish");

            this.aquarium.Add(fish);

            var expected = 1;
            var actual = this.aquarium.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddEqual()
        {
            var fish = new Fish("Fish");
            var fish2 = new Fish("Fish2");
            var fish3 = new Fish("Fish3");


            this.aquarium.Add(fish);
            this.aquarium.Add(fish2);
            this.aquarium.Add(fish3);

            Assert.Throws<InvalidOperationException>(() => this.aquarium.Add(new Fish("Fish")));
        }

        [Test]
        public void Remove()
        {
            var fish = new Fish("Fish");

            this.aquarium.Add(fish);

            this.aquarium.RemoveFish(fish.Name);

            var expected = 0;
            var actual = this.aquarium.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveNotOK()
        {
            var fish = new Fish("Fish");

            this.aquarium.Add(fish);

            Assert.Throws<InvalidOperationException>(() => this.aquarium.RemoveFish("NoExistingName"));
        }

        [Test]
        public void SellfishOK()
        {
            var fish = new Fish("Fish");
            
            this.aquarium.Add(fish);

            var actual = this.aquarium.SellFish(fish.Name);

            Assert.AreSame(fish, actual);
        }

        [Test]
        public void SellfishNotOk()
        {
            var fish = new Fish("Fish");

            this.aquarium.Add(fish);

            Assert.Throws<InvalidOperationException>(() => this.aquarium.SellFish("NoExistingName"));
        }

        [Test]
        public void ReportOk()
        {
            var fish = new Fish("Fish");
            var fish2 = new Fish("Fish2");
            var fish3 = new Fish("Fish3");


            this.aquarium.Add(fish);
            this.aquarium.Add(fish2);
            this.aquarium.Add(fish3);

            var expected = $"Fish available at Name: Fish, Fish2, Fish3";
            var actual = this.aquarium.Report();

            Assert.AreEqual(expected, actual);
        }
    }
}
