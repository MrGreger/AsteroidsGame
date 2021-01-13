using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class OnEnemyDieEvent
{
    public IEnemy Enemy { get;}

    public OnEnemyDieEvent(IEnemy enemy)
    {
        Enemy = enemy;
    }
}

