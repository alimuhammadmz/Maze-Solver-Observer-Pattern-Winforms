using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment3;

namespace Assignment3
{
    public interface IObserver                              //each view must SET its state on front-end and SHOW it
    {
        void ShowState(int position, state newState);       //update() method
        void SetState(int position, state newState);
    }
}
