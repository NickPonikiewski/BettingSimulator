using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BettingSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        private void PowerBall_Play_Button_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int[] PlayerBalls = { (int)Ball1numericUpDown.Value, (int)Ball2numericUpDown.Value, (int)Ball3numericUpDown.Value, (int)Ball4numericUpDown.Value, (int)Ball5numericUpDown.Value, (int)Ball5numericUpDown.Value};
            int PlayerRedBall = (int)Ball6numericUpDown.Value;
            int[] ActualBaseBalls = { random.Next(1, 70), random.Next(1, 70), random.Next(1, 70), random.Next(1, 70), random.Next(1, 70), random.Next(1, 70) };
            int ActualRedBall = random.Next(1, 27);
            int Winnings = 0;
            
            Ball1.Text = Convert.ToString(ActualBaseBalls[0]);
            Ball2.Text = Convert.ToString(ActualBaseBalls[1]);
            Ball3.Text = Convert.ToString(ActualBaseBalls[2]);
            Ball4.Text = Convert.ToString(ActualBaseBalls[3]);
            Ball5.Text = Convert.ToString(ActualBaseBalls[4]);
            Ball6.Text = Convert.ToString(ActualRedBall);

            PowerBall Player = new PowerBall(PlayerBalls, PlayerRedBall);
            PowerBall Computer = new PowerBall(ActualBaseBalls, ActualRedBall);
            Bet<PowerBall> TryBet = new Bet<PowerBall>();
            TryBet.Player = Player;
            TryBet.Computer[0] = Computer;
            if (TryBet.DoBet() == 1)
            {
                if (TryBet.Player.WinType == 9)
                {
                    Winnings = Convert.ToInt32(Total_Winnings.Text);
                    Winnings += 100000000;
                    Total_Winnings.Text = Convert.ToString(Winnings);
                }
                else if (TryBet.Player.WinType == 8)
                {
                    Winnings = Convert.ToInt32(Total_Winnings.Text);
                    Winnings += 1000000;
                    Total_Winnings.Text = Convert.ToString(Winnings);
                }
                else if (TryBet.Player.WinType == 7)
                {
                    Winnings = Convert.ToInt32(Total_Winnings.Text);
                    Winnings += 50000;
                    Total_Winnings.Text = Convert.ToString(Winnings);
                }
                else if (TryBet.Player.WinType == 6 || TryBet.Player.WinType == 5)
                {
                    Winnings = Convert.ToInt32(Total_Winnings.Text);
                    Winnings += 100;
                    Total_Winnings.Text = Convert.ToString(Winnings);
                }
                else if (TryBet.Player.WinType == 4 || TryBet.Player.WinType == 3)
                {
                    Winnings = Convert.ToInt32(Total_Winnings.Text);
                    Winnings += 7;
                    Total_Winnings.Text = Convert.ToString(Winnings);
                }
                else if (TryBet.Player.WinType == 2 || TryBet.Player.WinType == 1)
                {
                    Winnings = Convert.ToInt32(Total_Winnings.Text);
                    Winnings += 4;
                    Total_Winnings.Text = Convert.ToString(Winnings);
                }

                PBWin.Text = "Winner!!";
            }
            PBWin.Text = "Loser!!";
            int Curr_Losses = Convert.ToInt32(Total_Losses.Text);
            Total_Losses.Text = Convert.ToString(Curr_Losses += 1);
        }

    }
    public class Bet<T> where T : IComparable
    {
        public T Player { get; set; }
        public T[] Computer = new T[20];

        public int WagerAmount { get; set; }

        public int DoBet()
        {
            if (Player == null || Computer == null)
            {
                throw new InvalidOperationException("Must define Mine and Theirs before betting");
            }
            if (Computer.Length == 0)
            {
                return WagerAmount;
            }

            T Winner = Player;
            foreach (T other in Computer)
            {
                if (other != null)
                {
                    if (Winner.CompareTo(other) == -1)
                    {
                        Winner = other;
                    }
                }
            }

            if (Player.CompareTo(Winner) == 0)
            {
                return 1;
            }
            else 
            {
                return 0;
            }
        }
    }

    class PowerBall : IComparable
    {
        private int[] Balls;
        private int RedBall;
        public int WinType
        {
            get { return WinType;}
            private set { WinType = value; }
        }
        public PowerBall(int[] GBalls, int RB)
        {
            Balls = GBalls;
            RedBall = RB;
        }

        public int CompareTo(object obj)
        {
            PowerBall other = obj as PowerBall;
            Array.Sort(this.Balls);
            Array.Sort(other.Balls);

            if (this.Balls == other.Balls && this.RedBall == other.RedBall)
            {
                WinType = 9;
                return 1;
            }
            else if (this.Balls == other.Balls)
            {
                WinType = 8;
                return 1;
            }
            else if (this.Balls[0] == other.Balls[0] && this.Balls[1] == other.Balls[1] && this.Balls[2] == other.Balls[2] && this.Balls[3] == other.Balls[3] && this.RedBall == other.RedBall)
            {
                WinType = 7;
                return 1;
            }
            else if (this.Balls[0] == other.Balls[0] && this.Balls[1] == other.Balls[1] && this.Balls[2] == other.Balls[2] && this.Balls[3] == other.Balls[3])
            {
                WinType = 6;
                return 1;
            }
            else if (this.Balls[0] == other.Balls[0] && this.Balls[1] == other.Balls[1] && this.Balls[2] == other.Balls[2] && this.RedBall == other.RedBall)
            {
                WinType = 5;
                return 1;
            }
            else if (this.Balls[0] == other.Balls[0] && this.Balls[1] == other.Balls[1] && this.Balls[2] == other.Balls[2])
            {
                WinType = 4;
                return 1;
            }
            else if (this.Balls[0] == other.Balls[0] && this.Balls[1] == other.Balls[1] && this.RedBall == other.RedBall)
            {
                WinType = 3;
                return 1;
            }
            else if (this.Balls[0] == other.Balls[0] && this.RedBall == other.RedBall)
            {
                WinType = 2;
                return 1;
            }
            else if (this.RedBall == other.RedBall)
            {
                WinType = 1;
                return 1;
            }

            return -1;
        }


        
    }

    class HorseRace : IComparable
    {
        private Random r = new Random();
        private int HorseSpeed;
        private int WinType
        {
            get { return WinType; }
            set { WinType = value; }
        }
        public HorseRace()
        {
            HorseSpeed = r.Next(1, 12);
        }
         public int CompareTo(object obj)
         {
             HorseRace other = obj as HorseRace;

             if (this.HorseSpeed > other.HorseSpeed)
             {
                 return 1;
             }
             else if (this.HorseSpeed < other.HorseSpeed)
             {
                 return -1;
             }
             else
             {
                 return 0;
             }
         }
    }

    class Poker : IComparable
    {
        
        private Random r = new Random();
        private int[] Hand = new int[5];
        private int HandValue;
        private int WinType
        {
            get { return WinType; }
            set { WinType = value; }
        }
        public Poker()
        {
            for (int i = 0; i < Hand.Length; i++)
            {
                Hand[i] = r.Next(1, 53);
            }
            Array.Sort(Hand);
            //Royal Flush
            if (((Hand[0] == 1) && (Hand[0] == 13) && (Hand[0] == 12) && (Hand[0] == 11) && (Hand[0] == 10)) || ((Hand[0] == 14) && (Hand[0] == 26) && (Hand[0] == 25) && (Hand[0] == 24) && (Hand[0] == 23)) || ((Hand[0] == 27) && (Hand[0] == 39) && (Hand[0] == 38) && (Hand[0] == 37) && (Hand[0] == 36)) || ((Hand[0] == 40) && (Hand[0] == 52) && (Hand[0] == 51) && (Hand[0] == 50) && (Hand[0] == 49)))
            {
                HandValue = 9;

            } else if (Straight_Flush(Hand))
            {
                HandValue = 8;

            } else if (FourKind(Hand))
            {
                HandValue = 7;

            } else if (FullHouse(Hand))
            {
                HandValue = 6;

            } else if (Flush(Hand))
            {
                HandValue = 5;

            } else if (Straight(Hand))
            {
                HandValue = 4;

            } else if (ThreeKind(Hand))
            {
                HandValue = 3;

            } else if (TwoPair(Hand))
            {
                HandValue = 2;

            } else if (OnePair(Hand))
            {
                HandValue = 1;
            } else
            {
                HandValue = HighCard(Hand);
            }
        }
        int HighCard(int[] H)
        {
            int Suits = 4;
            int[] As = { 1, 14, 27, 40 };
            int[] Twos = { 2, 15, 28, 41 };
            int[] Threes = { 3, 16, 29, 42 };
            int[] Fours = { 4, 17, 30, 43 };
            int[] Fives = { 5, 18, 31, 44 };
            int[] Sixes = { 6, 19, 32, 45 };
            int[] Sevens = { 7, 20, 33, 46 };
            int[] Eights = { 8, 21, 34, 47 };
            int[] Nines = { 9, 22, 35, 48 };
            int[] Tens = { 10, 23, 36, 49 };
            int[] Js = { 11, 24, 37, 50 };
            int[] Qs = { 12, 25, 38, 51 };
            int[] Ks = { 13, 26, 39, 52 };
            int[][] Deck = { Twos, Threes, Fours, Fives, Sixes, Sevens, Eights, Nines, Tens, Js, Qs, Ks, As };
 
            for (int i = 0; i < Suits; i++)
            {
                for (int j = 0; j < 15; i++)
                {
                    if (H[0] == Deck[j][i])
                    {
                        return (-1 * j);
                    }
                }
            }

            return 0;
        
        }
        bool OnePair(int[] H)
        {
            if (Count(H) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool TwoPair(int[] H)
        {
            int UsedI = -1, UsedJ = -1, PairCount = 0;
            for (int i = 0; i < H.Length; i++)
            {
                for (int j = 0; j < H.Length; j++)
                {
                    if(H[i] == H[j] && i != UsedI && j != UsedJ)
                    {
                        PairCount++;
                        if (PairCount == 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        bool Straight(int[] H)
        {
            int[] As = { 1, 14, 27, 40 };
            int[] Twos = { 2, 15, 28, 41 };
            int[] Threes = { 3, 16, 29, 42 };
            int[] Fours = { 4, 17, 30, 43 };
            int[] Fives = { 5, 18, 31, 44 };
            int[] Sixes = { 6, 19, 32, 45 };
            int[] Sevens = { 7, 20, 33, 46 };
            int[] Eights = { 8, 21, 34, 47 };
            int[] Nines = { 9, 22, 35, 48 };
            int[] Tens = { 10, 23, 36, 49 };
            int[] Js = { 11, 24, 37, 50 };
            int[] Qs = { 12, 25, 38, 51 };
            int[] Ks = { 13, 26, 39, 52 };

            for (int i = 0; i <= 3; i++)
            {
                if((H[0] == As[i]) && (H[1] == Ks[i]) && (H[2] == Qs[i]) && (H[3] == Js[i]) && (H[4] == Tens[i]))
                {
                    return true;
                }
                if ((H[0] == Ks[i]) && (H[1] == Qs[i]) && (H[2] == Js[i]) && (H[3] == Tens[i]) && (H[4] == Nines[i]))
                {
                    return true;
                }
                if ((H[0] == Qs[i]) && (H[1] == Js[i]) && (H[2] == Tens[i]) && (H[3] == Nines[i]) && (H[4] == Eights[i]))
                {
                    return true;
                }
                if ((H[0] == Js[i]) && (H[1] == Tens[i]) && (H[2] == Nines[i]) && (H[3] == Eights[i]) && (H[4] == Sevens[i]))
                {
                    return true;
                }
                if ((H[0] == Tens[i]) && (H[1] == Nines[i]) && (H[2] == Eights[i]) && (H[3] == Sevens[i]) && (H[4] == Sixes[i]))
                {
                    return true;
                }
                if ((H[0] == Nines[i]) && (H[1] == Eights[i]) && (H[2] == Sevens[i]) && (H[3] == Sixes[i]) && (H[4] == Fives[i]))
                {
                    return true;
                }
                if ((H[0] == Eights[i]) && (H[1] == Sevens[i]) && (H[2] == Sixes[i]) && (H[3] == Fives[i]) && (H[4] == Fours[i]))
                {
                    return true;
                }
                if ((H[0] == Sevens[i]) && (H[1] == Sixes[i]) && (H[2] == Fives[i]) && (H[3] == Fours[i]) && (H[4] == Threes[i]))
                {
                    return true;
                }
                if ((H[0] == Sixes[i]) && (H[1] == Fives[i]) && (H[2] == Fours[i]) && (H[3] == Threes[i]) && (H[4] == Twos[i]))
                {
                    return true;
                }
            }

            return false;
 
        }
        bool Flush (int[] H)
        {
            int[] B_Spades = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            int[] R_Harts = { 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
            int[] B_Clubs = { 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 };
            int[] R_Diamonds = { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52 };
            int[][] Deck = { B_Spades, R_Harts, B_Clubs, R_Diamonds };
            bool Spade = false, Harts = false, Club = false, Diamond = false;

            for(int card = 0; card < H.Length; card++)
            {
                for (int suit = 0; suit <= 3; suit++)
                {
                    for (int D_card = 0; D_card <=13; D_card++)
                    {
                        if(H[card] == Deck[suit][D_card])
                        {
                            if(suit == 0) Spade = true;
                            if(suit == 1) Harts = true;
                            if(suit == 2) Club = true;
                            if(suit == 3) Diamond = true;

                        }
                    }
                }
            }

            if(Spade == true && Harts == false && Club == false && Diamond == false)
            {
                return true;
            }
            if (Spade == false && Harts == true && Club == false && Diamond == false)
            {
                return true;
            }
            if (Spade == false && Harts == false && Club == true && Diamond == false)
            {
                return true;
            }
            if (Spade == false && Harts == false && Club == false && Diamond == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool FullHouse(int[] H)
        {
            if (Count(H) == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool FourKind(int[] H)
        {
            if(Count(H) == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool ThreeKind(int[] H)
        {
            if (Count(H) == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Straight_Flush(int[] H)
        {
            if (Hand[0]-1 == Hand[1] && Hand[1]-1 == Hand[2] && Hand[2]-1 == Hand[3] && Hand[3]-1 == Hand[4])
            {
                return true;
            }

            return false;
        }
        private int Count(int[] H)
        {
            int Spade_A = 0, Spade_2 = 0, Spade_3 = 0, Spade_4 = 0, Spade_5 = 0, Spade_6 = 0, Spade_7 = 0, Spade_8 = 0, Spade_9 = 0, Spade_10 = 0, Spade_J = 0, Spade_Q = 0, Spade_K = 0;
            int Hart_A = 0, Hart_2 = 0, Hart_3 = 0, Hart_4 = 0, Hart_5 = 0, Hart_6 = 0, Hart_7 = 0, Hart_8 = 0, Hart_9 = 0, Hart_10 = 0, Hart_J = 0, Hart_Q = 0, Hart_K = 0;
            int Club_A = 0, Club_2 = 0, Club_3 = 0, Club_4 = 0, Club_5 = 0, Club_6 = 0, Club_7 = 0, Club_8 = 0, Club_9 = 0, Club_10 = 0, Club_J = 0, Club_Q = 0, Club_K = 0;
            int Diamond_A = 0, Diamond_2 = 0, Diamond_3 = 0, Diamond_4 = 0, Diamond_5 = 0, Diamond_6 = 0, Diamond_7 = 0, Diamond_8 = 0, Diamond_9 = 0, Diamond_10 = 0, Diamond_J = 0, Diamond_Q = 0, Diamond_K = 0;
            int sum_A, sum_2, sum_3, sum_4, sum_5, sum_6, sum_7, sum_8, sum_9, sum_10, sum_J, sum_Q, sum_K;
            bool TwoCount = false, ThreeCount = false, FourCount = false;
            for (int card = 1; card < H.Length; card++)
            {
                switch (H[card])
                {
                    case 1:
                       Spade_A++;
                        break;
                    case 2:
                        Spade_2++;
                        break;
                    case 3:
                        Spade_3++;
                        break;
                    case 4:
                        Spade_4++;
                        break;
                    case 5:
                        Spade_5++;
                        break;
                    case 6:
                        Spade_6++;
                        break;
                    case 7:
                        Spade_7++;
                        break;
                    case 8:
                        Spade_8++;
                        break;
                    case 9:
                        Spade_9++;
                        break;
                    case 10:
                        Spade_10++;
                        break;
                    case 11:
                        Spade_J++;
                        break;
                    case 12:
                        Spade_Q++;
                        break;
                    case 13:
                        Spade_K++;
                        break;
                    case 14:
                        Hart_A++;
                        break;
                    case 15:
                        Hart_2++;
                        break;
                    case 16:
                        Hart_3++;
                        break;
                    case 17:
                        Hart_4++;
                        break;
                    case 18:
                        Hart_5++;
                        break;
                    case 19:
                        Hart_6++;
                        break;
                    case 20:
                        Hart_7++;
                        break;
                    case 21:
                        Hart_8++;
                        break;
                    case 22:
                        Hart_9++;
                        break;
                    case 23:
                        Hart_10++;
                        break;
                    case 24:
                        Hart_J++;
                        break;
                    case 25:
                        Hart_Q++;
                        break;
                    case 26:
                        Hart_K++;
                        break;
                    case 27:
                        Club_A++;
                        break;
                    case 28:
                        Club_2++;
                        break;
                    case 29:
                        Club_3++;
                        break;
                    case 30:
                        Club_4++;
                        break;
                    case 31:
                        Club_5++;
                        break;
                    case 32:
                        Club_6++;
                        break;
                    case 33:
                        Club_7++;
                        break;
                    case 34:
                        Club_8++;
                        break;
                    case 35:
                        Club_9++;
                        break;
                    case 36:
                        Club_10++;
                        break;
                    case 37:
                        Club_J++;
                        break;
                    case 38:
                        Club_Q++;
                        break;
                    case 39:
                        Club_K++;
                        break;
                    case 40:
                        Diamond_A++;
                        break;
                    case 41:
                        Diamond_2++;
                        break;
                    case 42:
                        Diamond_3++;
                        break;
                    case 43:
                        Diamond_4++;
                        break;
                    case 44:
                        Diamond_5++;
                        break;
                    case 45:
                        Diamond_6++;
                        break;
                    case 46:
                        Diamond_7++;
                        break;
                    case 47:
                        Diamond_8++;
                        break;
                    case 48:
                        Diamond_9++;
                        break;
                    case 49:
                        Diamond_10++;
                        break;
                    case 50:
                        Diamond_J++;
                        break;
                    case 51:
                        Diamond_Q++;
                        break;
                    case 52:
                        Diamond_K++;
                        break;
                }
            }

            sum_A = Spade_A + Hart_A + Club_A + Diamond_A;
            sum_2 = Spade_2 + Hart_2 + Club_2 + Diamond_2;
            sum_3 = Spade_3 + Hart_3 + Club_3 + Diamond_3;
            sum_4 = Spade_4 + Hart_4 + Club_4 + Diamond_4;
            sum_5 = Spade_5 + Hart_5 + Club_5 + Diamond_5;
            sum_6 = Spade_6 + Hart_6 + Club_6 + Diamond_6;
            sum_7 = Spade_7 + Hart_7 + Club_7 + Diamond_7;
            sum_8 = Spade_8 + Hart_8 + Club_8 + Diamond_8;
            sum_9 = Spade_9 + Hart_9 + Club_9 + Diamond_9;
            sum_10 = Spade_10 + Hart_10 + Club_10 + Diamond_10;
            sum_J = Spade_J+ Hart_J + Club_J + Diamond_J;
            sum_Q = Spade_Q + Hart_Q + Club_Q + Diamond_Q;
            sum_K = Spade_K + Hart_K + Club_K + Diamond_K;

            CheckCount(Spade_A, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_2, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_3, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_4, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_5, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_6, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_7, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_8, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_9, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_10, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_J, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_Q, TwoCount, ThreeCount, FourCount);
            CheckCount(Spade_K, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_A, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_2, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_3, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_4, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_5, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_6, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_7, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_8, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_9, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_10, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_J, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_Q, TwoCount, ThreeCount, FourCount);
            CheckCount(Hart_K, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_A, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_2, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_3, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_4, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_5, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_6, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_7, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_8, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_9, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_10, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_J, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_Q, TwoCount, ThreeCount, FourCount);
            CheckCount(Club_K, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_A, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_2, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_3, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_4, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_5, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_6, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_7, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_8, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_9, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_10, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_J, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_Q, TwoCount, ThreeCount, FourCount);
            CheckCount(Diamond_K, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_A, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_2, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_3, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_4, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_5, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_6, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_7, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_8, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_9, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_10, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_J, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_Q, TwoCount, ThreeCount, FourCount);
            CheckCount(sum_K, TwoCount, ThreeCount, FourCount);
            if (FourCount == true) // four kind
            {
                return 4;

            } else if (ThreeCount == true && TwoCount == true) // full house
            {
                return 5;

            } else if (TwoCount == true) // one pair
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        private void CheckCount(int Count, bool TwoCount, bool ThreeCount, bool FourCount)
        {
            switch (Count)
            {
                case 2:
                    TwoCount = true;
                    break;
                case 3:
                    ThreeCount = true;
                    break;
                case 4:
                    FourCount = true;
                    break;
            }
        }
        public int CompareTo(object obj)
        {
            Poker other = obj as Poker;

            if (this.HandValue == other.HandValue)
            {
                return 0;

            } else if (this.HandValue > other.HandValue)
            {
                return 1;

            } else // this.HandValue < other.HandValue
            {
                return -1;
            }
        }
    }

}
