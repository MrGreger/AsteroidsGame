using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class OnNewHighScoreEvent
{
    public int Score { get; }

    public OnNewHighScoreEvent(int score)
    {
        Score = score;
    }
}

