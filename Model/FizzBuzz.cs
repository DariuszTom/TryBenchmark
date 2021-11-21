using System;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace TryBenchmark.Model
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class FizzBuzz
    {
        #region Fields
        private long _NumberOfLoops=1000;
        #endregion
        #region Constructor
        public FizzBuzz() { }
        public FizzBuzz(long numberOfLoops)
        {
            _NumberOfLoops = numberOfLoops;
        }
        #endregion
        #region Methods

        [Benchmark]
        public string[] NaiveFizzBuzz()
        {
            string[] tempArrNaive = new string[_NumberOfLoops];
            for (long loopL = 1; loopL < _NumberOfLoops; loopL++)
            {
                string output = string.Empty;
                if (loopL % 3 == 0) output = "Fizz";
                if (loopL % 5 == 0) output = "Buzz";
                if (loopL % 3 == 0 && loopL % 5 == 0) output = "FizzBuzz";
                tempArrNaive[loopL]=!string.IsNullOrEmpty(output) ? output : loopL.ToString();
            }
            return tempArrNaive;
        }

        [Benchmark]
        public string[] BetterFizzBuzz()
        {
            string fizz = "Fizz"; string buzz = "Buzz";
            string[] tempArrBetter = new string[_NumberOfLoops];
            for (long loopL = 1; loopL < _NumberOfLoops; loopL++)
            {
                string add = loopL % 3 == 0 ? fizz : string.Empty;
                add += loopL % 5 == 0 ? buzz : string.Empty;

                tempArrBetter[loopL] = (string.IsNullOrEmpty(add) ? loopL.ToString() : add);
            }
            return tempArrBetter;
        }

        [Benchmark]
        public string[] TryBetterFizzBuzz()
        {
            string[] tempArrBetter = new string[_NumberOfLoops];
            Span<char> fizz = new ("Fizz".ToCharArray());
            Span<char> buzz = new ("Buzz".ToCharArray());
            Span<char> add; Span<char> add2;
            for (long loopL = 1; loopL < _NumberOfLoops; loopL++)
            {
                add = loopL % 3 == 0 ? fizz : null;
                add2 = loopL % 5 == 0 ? buzz : null;

                tempArrBetter[loopL] = (!add.IsEmpty || !add2.IsEmpty) == false ? loopL.ToString() : string.Concat(add, add2);
            }
            return tempArrBetter;
        }

        [Benchmark]
        public string[] StringBuilderBetterFizzBuzz()
        {
            string[] tempArrBetter = new string[_NumberOfLoops];
            string fizz = "Fizz"; string buzz = "Buzz";
            StringBuilder sb = new();
            for (long loopL = 1; loopL < _NumberOfLoops; loopL++)
            {
                if (loopL % 3 == 0) sb.Append(fizz);
                if (loopL % 5== 0) sb.Append(buzz);

                if (sb.Length > 3)
                {
                    tempArrBetter[loopL] = sb.ToString();
                    sb.Clear();
                }else
                    tempArrBetter[loopL] = loopL.ToString();
            }
            return tempArrBetter;
        }

        [Benchmark]
        public string[] ParrelBetterFizzBuzz()
        {
            string[] tempArrBetter = new string[_NumberOfLoops];
            string fizz = "Fizz"; string buzz = "Buzz";

            Parallel.For(1, _NumberOfLoops, loopL=>
            {
                if (loopL % 3 == 0) tempArrBetter[loopL] = fizz;
                if (loopL % 5 == 0) tempArrBetter[loopL] += buzz;
                if (string.IsNullOrEmpty(tempArrBetter[loopL])) tempArrBetter[loopL] = loopL.ToString();
            });
            return tempArrBetter;
        }
        #endregion
    }
}
