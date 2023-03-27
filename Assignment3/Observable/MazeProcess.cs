using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Assignment3;

namespace Assignment3.Processor
{
    public class MazeProcess : IObservable
    {
        private int SIZE;
        private int START_POS;
        private int END_POS;
        private state[,] states;
        private List<IObserver> observerViews;

        public void add(IObserver desktopObserver)
        {
            observerViews.Add(desktopObserver);
            
        }

        public void remove(IObserver observer)
        {
            observerViews.Remove(observer);
        }

        public MazeProcess(int SIZE, int START_POS, int END_POS) {
            observerViews = new List<IObserver>();
            states = new state[SIZE, SIZE];
            this.SIZE = SIZE;
            this.START_POS = START_POS;
            this.END_POS = END_POS;
        }

        public void InitializeMaze()
        {
            for (int rowIndex = 0; rowIndex < SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < SIZE; ++colIndex)
                {
                    states[rowIndex, colIndex] = state.Blank;
                    if ((rowIndex) * SIZE + (colIndex) == START_POS)
                    {
                        states[rowIndex, colIndex] = state.Start;
                    }
                    if ((rowIndex) * SIZE + (colIndex) == END_POS)
                    {
                        states[rowIndex, colIndex] = state.End;
                    }
                }
        }

        public void SetState(int position, state newState)
        {
            int rowIndex = position / SIZE;
            int colIndex = position % SIZE;

            states[rowIndex, colIndex] = newState;
        }

        public int solve(int currentPos)
        {
            while (currentPos != END_POS)
            {
                //SetState(currentPos, state.Traversed);
                dir direction = dir.NA;
                int nextPos = GetAvailablePos(currentPos, out direction);
                if (nextPos == -1)
                {
                    return -1;
                }
                MoveNextPos(currentPos, nextPos, direction);
                currentPos = nextPos;
            }
            return currentPos;
        }

        public state[,] Generate()
        {
            int pos = 0;
            Random rand = new Random(DateTime.Now.Millisecond);

            for (int rowIndex = 0; rowIndex < SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < SIZE; ++colIndex)
                {
                    if (pos == START_POS)
                    {
                        SetStateInd(rowIndex, colIndex, state.Start);
                    }

                    if (pos == END_POS)
                    {
                        SetStateInd(rowIndex, colIndex, state.End);
                    }

                    if (pos != START_POS && pos != END_POS)
                    {
                        int num = rand.Next(3);
                        if (num == 0)
                        {
                            SetStateInd(rowIndex, colIndex, state.Hurdle);
                        }
                        else
                        {
                            SetStateInd(rowIndex, colIndex, state.Blank);
                        }
                    }
                    pos++;
                }
            return states;
        }

        public void MoveNextPos(int currentPos, int nextPos, dir direction)
        {
            state currentState = states[currentPos / SIZE, currentPos % SIZE];
            state nextState = states[nextPos / SIZE, nextPos % SIZE];

            if (nextState == state.Blank || nextState == state.End)
            {
                SetState(currentPos, GetStateFromDirection(direction));
                foreach (IObserver observer in observerViews)
                {
                    observer.ShowState(currentPos, GetStateFromDirection(direction));     //update() method
                }
            }
            if (nextState == state.TraversedToNorth || nextState == state.TraversedToSouth
                || nextState == state.TraversedToEast || nextState == state.TraversedToWest)
            {
                SetState(currentPos, state.Backtracked);
                foreach (IObserver observer in observerViews)
                {
                    observer.ShowState(currentPos, state.Backtracked);    
                }
            }
        }

        public void SetStateInd(int rowIndex, int colIndex, state Tmp)
        {
            states[rowIndex, colIndex] = Tmp;
        }

        state GetStateFromDirection(dir direction)
        {
            switch (direction)
            {
                case dir.East: return state.TraversedToEast;
                case dir.West: return state.TraversedToWest;
                case dir.North: return state.TraversedToNorth;
                case dir.South: return state.TraversedToSouth;
            }
            return state.NoState;
        }


        public state GetNextState(int currentPos, dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / SIZE;
            int colIndex = currentPos % SIZE;
            switch (direction)
            {
                case dir.East:
                    if (colIndex == SIZE - 1)
                        return state.NoState;
                    colIndex++;
                    break;
                case dir.West:
                    if (colIndex == 0)
                        return state.NoState;
                    colIndex--;
                    break;
                case dir.North:
                    if (rowIndex == 0)
                        return state.NoState;
                    rowIndex--;
                    break;
                case dir.South:
                    if (rowIndex == SIZE - 1)
                        return state.NoState;
                    rowIndex++;
                    break;
                default:
                    return state.NoState;
            }
            return states[rowIndex, colIndex];
        }

        public int GetPos(int currentPos, dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / SIZE;
            int colIndex = currentPos % SIZE;

            // no error checking here, assuming everything is OK
            if (direction == dir.East) colIndex++;
            if (direction == dir.West) colIndex--;
            if (direction == dir.North) rowIndex--;
            if (direction == dir.South) rowIndex++;

            return (rowIndex * SIZE + colIndex);
        }

        public int GetAvailablePos(int currentPos, out dir direction)
        {
            // move right
            direction = dir.East;
            state rightState = GetNextState(currentPos, direction);
            if (rightState == state.Blank || rightState == state.End)
                return GetPos(currentPos, direction);

            // move down
            direction = dir.South;
            state downState = GetNextState(currentPos, direction);
            if (downState == state.Blank || downState == state.End)
                return GetPos(currentPos, direction);

            // move left
            direction = dir.West;
            state leftState = GetNextState(currentPos, direction);
            if (leftState == state.Blank || leftState == state.End)
                return GetPos(currentPos, direction);

            // move up
            direction = dir.North;
            state upState = GetNextState(currentPos, direction);
            if (upState == state.Blank || upState == state.End)
                return GetPos(currentPos, direction);

            // if no blanks look for traversed states, if there is any, to backtrack
            direction = dir.East;
            if (rightState == state.TraversedToWest)
                return GetPos(currentPos, direction);
            direction = dir.South;
            if (downState == state.TraversedToNorth)
                return GetPos(currentPos, direction);
            direction = dir.West;
            if (leftState == state.TraversedToEast)
                return GetPos(currentPos, direction);
            direction = dir.North;
            if (upState == state.TraversedToSouth)
                return GetPos(currentPos, direction);

            direction = dir.NA;
            return -1;
        }

    }
}
