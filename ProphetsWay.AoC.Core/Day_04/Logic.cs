using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProphetsWay.AoC.Core.Day_04
{
    public class Logic : BaseLogic
    {
        public (List<CallOut>, List<Board>) SetupGame()
        {
            var reader = GetInputTextReader();

            var callOuts = new List<CallOut>();
            var lookups = new Dictionary<int, CallOut>();
            var boards = new List<Board>();

            Board currBoard = null;
            var firstRow = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;

                //first row is 
                if (firstRow)
                {
                    var calloutsArr = line.Split(",");
                    foreach (var calloutStr in calloutsArr)
                    {
                        var calloutInt = int.Parse(calloutStr);
                        var callout = new CallOut(calloutInt);
                        callOuts.Add(callout);
                        lookups.Add(calloutInt, callout);
                    }

                    firstRow = false;
                    continue;
                }

                // if we're at an empty line, then we start a new board
                if (string.IsNullOrEmpty(line))
                {
                    currBoard = new Board();
                    boards.Add(currBoard);
                    currBoard.ID = boards.Count;
                    continue;
                }

                //if we have a line, then we're working on the current board's next row
                var placingsArr = line.Split(" ");
                foreach (var placingStr in placingsArr)
                {
                    if (string.IsNullOrEmpty(placingStr))
                        continue;

                    var placingInt = int.Parse(placingStr);
                    var callout = lookups[placingInt];

                    currBoard.AddPlacing(callout);
                }

            }

            return (callOuts, boards);
        }

        public int Part1()
        {
            var (callOuts, boards) = SetupGame();

            foreach (var callout in callOuts)
            {
                callout.CallNumber();
                var winningBoard = boards.SingleOrDefault(x => x.WinningBoard);

                if (winningBoard != null)
                    return winningBoard.Score;
            }

            throw new Exception("shouldn't be here...");
        }

        public int Part2()
        {
            var (callOuts, boards) = SetupGame();

            Board lastWinningBoard = null;
            for(var i = 0; i < callOuts.Count; i++)
            {
                var callout = callOuts[i];
                callout.CallNumber();
                var winningBoards = boards.Where(x => x.WinningBoard);

                if (winningBoards.Count() > 0)
                {
                    if (winningBoards.Count() == 1)
                        lastWinningBoard = winningBoards.Single();

                    boards.RemoveAll(x=> x.WinningBoard);
                }
            }

            return lastWinningBoard.Score;
        }

        public class Board
        {
            public override string ToString()
            {
                return $"Board:{ID} | Winning:{WinningBoard} | Score:{Score} | LastHit:{LastCallout}";
            }

            public Board()
            {
                Placings = new Placing[,] {
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null },
                    { null, null, null, null, null }
                };
            }

            public int ID { get; set; }

            public int Score { get; private set; }

            public void AddPlacing(CallOut callOut)
            {
                for (var x = 0; x < 5; x++)
                    for (var y = 0; y < 5; y++)
                    {
                        if (Placings[x, y] == null)
                        {
                            var p = new Placing(callOut, x, y);
                            p.PlacingHit += (sender, args) => CheckHit(args);
                            Placings[x, y] = p;
                            return;
                        }
                    }

                throw new Exception("shouldn't have gotten here.  Too many callouts provided");
            }

            public int LastCallout { get; private set; }
            public bool WinningBoard { get; private set; }

            public void CheckHit(Placing placing)
            {
                if (WinningBoard)
                    return;

                LastCallout = placing.CallOut.Number;

                //check row
                var winningRow = true;
                for (var x = 0; x < 5; x++)
                {
                    if (!Placings[x, placing.Col].Hit)
                    {
                        winningRow = false;
                        break;
                    }
                }
              
                //check col
                var winningCol = true;
                for (var y = 0; y < 5; y++)
                {
                    if (!Placings[placing.Row, y].Hit)
                    {
                        winningCol = false;
                        break;
                    }
                }

                WinningBoard = winningCol || winningRow;

                if (WinningBoard)
                {
                    var unmarkedSum = 0;
                    //calculate winning number
                    for (var x = 0; x < 5; x++)
                        for (var y = 0; y < 5; y++)
                        {
                            var curr = Placings[x, y];
                            if (!curr.Hit)
                                unmarkedSum += curr.CallOut.Number;
                        }

                    Score = unmarkedSum * LastCallout;
                }
            }

            public Placing[,] Placings { get; }
        }

        public class Placing
        {
            public override string ToString()
            {
                return $"Row/Col:  {Row}/{Col} | Hit: {Hit} | Number: {CallOut}";
            }

            public Placing(CallOut callOut, int row, int col)
            {
                CallOut = callOut;
                Row = row;
                Col = col;

                callOut.NumberCalled += (sender, args) => {
                    Hit = true;
                    PlacingHit?.Invoke(sender, this);
                };
            }

            public bool Hit { get; private set; }
            public EventHandler<Placing> PlacingHit;
            public CallOut CallOut { get; }
            public int Row { get; }
            public int Col { get; }
        }

        public class CallOut
        {
            public override string ToString()
            {
                return $"Number:  {Number}";
            }
            public CallOut(int number)
            {
                Number = number;
            }

            public int Number { get; }

            public void CallNumber()
            {
                NumberCalled?.Invoke(this, this);
            }

            public EventHandler<CallOut> NumberCalled;
        }
    }
}
