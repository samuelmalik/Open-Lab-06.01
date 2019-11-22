using System;
using System.Collections;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Open_Lab_06._01
{
    [TestFixture]
    public class Tests
    {

        private Challenge challenge;
        private bool shouldStop;

        private const int RandSeed = 601601601;
        private const int RandTestCasesCount = 96;

        private const int RandResultMinSize = 10;
        private const int RandResultMaxSize = 500;

        // changing this to any value above 9 will make this challenge a lot harder
        private const int RandCharMaxRepeat = 9;

        [OneTimeSetUp]
        public void Init()
        {
            challenge = new Challenge();
            shouldStop = false;
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome;

            if (outcome == ResultState.Failure || outcome == ResultState.Error)
                shouldStop = true;
        }

        [TestCase("A4B5C2", "AAAABBBBBCC")]
        [TestCase("C2F1E5", "CCFEEEEE")]
        [TestCase("T4S2V2", "TTTTSSVV")]
        [TestCase("A1B2C3D4", "ABBCCCDDDD")]
        public void ChallengeTest(string str, string expected) =>
            Assert.That(challenge.MysteryFunc(str), Is.EqualTo(expected));

        [TestCaseSource(nameof(GetRandom))]
        public void ChallengeTestRandom(string str, string expected)
        {
            if (shouldStop)
                Assert.Ignore("Previous test failed!");

            ChallengeTest(str, expected);
        }

        private static IEnumerable GetRandom()
        {
            var rand = new Random(RandSeed);

            for (var t = 0; t < RandTestCasesCount; t++)
            {
                var finalLength = rand.Next(RandResultMinSize, RandResultMaxSize - 1);
                var inputBuilder = new StringBuilder();
                var resultBuilder = new StringBuilder(finalLength);

                for (var sum = 0; sum < finalLength;)
                {
                    var num = rand.Next(1, RandCharMaxRepeat + 1);
                    var character = (char) rand.Next('A', 'z' + 1);

                    if (sum + num > finalLength)
                        num = finalLength - sum;

                    inputBuilder.Append(character);
                    inputBuilder.Append(num);

                    for (var i = 0; i < num; i++)
                        resultBuilder.Append(character);

                    sum += num;
                }

                yield return new TestCaseData(inputBuilder.ToString(), resultBuilder.ToString());
            }
        }

    }
}
