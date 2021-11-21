using NUnit.Framework;
using TryBenchmark.Model;

namespace TestingFizzBuzz
{
    public class FizzBuzzTest
    {
        #region Fields
        private string[] _CorrectArr;
        private FizzBuzz _Instance;
        #endregion
        [SetUp]
        public void Setup()
        {
            _Instance = new();
            _CorrectArr = _Instance.NaiveFizzBuzz();
        }

        [Test]
        public void TestBetterFizzBuzz()
        {
            Assert.AreEqual(_CorrectArr, _Instance.BetterFizzBuzz());
        }
        [Test]
        public void TestTryBetterFizzBuzz()
        {
            Assert.AreEqual(_CorrectArr, _Instance.TryBetterFizzBuzz());
        }
        [Test]
        public void TestStringBuilderBetterFizzBuzz()
        {
            Assert.AreEqual(_CorrectArr, _Instance.StringBuilderBetterFizzBuzz());
        }
        [Test]
        public void TestParrelBetterFizzBuzz()
        {
            Assert.AreEqual(_CorrectArr, _Instance.ParrelBetterFizzBuzz());
        }
    }
}