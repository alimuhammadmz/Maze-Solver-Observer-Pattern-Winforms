using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment3.Processor;

namespace Assignment3
{
    public interface IObservable
    {
        void add(IObserver view);                                 //adds frmMazeSolver object
        void remove(IObserver view);                              //removes frmMazeSolver object
        void MoveNextPos(int currentPos, int nextPos, dir direction);  //notify() method
        void InitializeMaze();
        void SetState(int position, state newState);
        int GetPos(int currentPos, dir direction);

    }
}
