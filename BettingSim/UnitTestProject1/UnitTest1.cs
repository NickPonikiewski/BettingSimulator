using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BettingSim;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPowerBall()
        {
            int[] PH = new int[5];
            int RB = 6;
            PH[0] = 0;
            PH[1] = 1;
            PH[2] = 2;
            PH[3] = 3;
            PH[4] = 4;

            PowerBall player = new PowerBall(PH, RB);
            PowerBall Com = new PowerBall(PH, RB);

            Bet<PowerBall> TryBet = new Bet<PowerBall>();
            TryBet.Player = player;
            TryBet.Computer[0] = Com;

            Assert.AreEqual(1, TryBet.DoBet());
        }
    }
}
