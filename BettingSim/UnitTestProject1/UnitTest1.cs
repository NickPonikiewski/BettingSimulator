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

        [TestMethod]
        public void TestHorseRace()
        {
            HorseRace Player = new HorseRace(6);
            HorseRace Com = new HorseRace(5);
            Bet<HorseRace> TryBet = new Bet<HorseRace>();
            TryBet.Player = Player;
            TryBet.Computer[0] = Com;

            Assert.AreEqual(1, TryBet.DoBet());

            Player = new HorseRace(5);
            Com = new HorseRace(6);
            TryBet = new Bet<HorseRace>();
            TryBet.Player = Player;
            TryBet.Computer[0] = Com;

            Assert.AreEqual(-1, TryBet.DoBet());
        }

        [TestMethod]
        public void TestPoker()
        {
            int[] Player_Hand = { 1, 14, 27, 40 };
            int[] Com_Hand = { 2, 15, 28, 41 };
            Poker Player = new Poker(Player_Hand);
            Poker Com = new Poker(Com_Hand);
            Bet<Poker> TryBet = new Bet<Poker>();

            TryBet.Player = Player;
            TryBet.Computer[0] = Com;

            Assert.AreEqual(1, TryBet.DoBet());
        }
    }
}
