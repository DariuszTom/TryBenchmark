using BenchmarkDotNet.Running;
using TryBenchmark.Model;
using System;

namespace TryBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz fb = new(1000);
            fb.NaiveFizzBuzz();
            fb.BetterFizzBuzz();
            fb.TryBetterFizzBuzz();
            fb.StringBuilderBetterFizzBuzz();
            fb.ParrelBetterFizzBuzz();
            BenchmarkRunner.Run<FizzBuzz>();
        }
    }
}
