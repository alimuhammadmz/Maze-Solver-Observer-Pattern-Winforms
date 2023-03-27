using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Assignment3.Processor;

namespace Assignment3
{
    public partial class graphicalView : Form, IObserver
    {
        private List<Button> btnList = new List<Button>();
        private static int SIZE = 20;
        private static int START_POS = 0;
        private static int END_POS = 399;
        private MazeProcess MazeProcessor;                      //object of observable
        
        public graphicalView(MazeProcess maze)
        {
            InitializeComponent();
            Font buttonFont = new Font("Arial", 8);
            this.SuspendLayout();
            for (int rowIndex = 0; rowIndex < SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < SIZE; ++colIndex)
                {
                    Button btn = new Button();
                    btn.Name = string.Format("btn{0}_{1}", rowIndex, colIndex);
                    btn.Parent = pnlParent;
                    btn.Location = new Point(colIndex * SIZE, rowIndex * SIZE);
                    btn.Size = new Size(SIZE, SIZE);
                    btn.Text = "";
                    btn.Font = buttonFont.Clone() as Font;
                    btn.Enabled = false;
                    if ((rowIndex) * SIZE + (colIndex) == START_POS)
                    {
                        btn.Text = "S";
                    }
                    if ((rowIndex) * SIZE + (colIndex) == END_POS)
                    {
                        btn.Text = "E";
                    }
                    btnList.Add(btn);
                }
            this.MazeProcessor = maze; 
            MazeProcessor.add(this);
            this.ResumeLayout();
        }

        public void SetState(int position, state newState)
        {
            MazeProcessor.SetState(position, newState);
            ShowState(position, newState);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int pos = 0;
            state[,] states = MazeProcessor.Generate();
            for (int rowIndex = 0; rowIndex < SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < SIZE; ++colIndex)
                {
                    if (states[rowIndex, colIndex] == state.Start)
                    {
                        btnList[pos].Text = "S";
                    }
                    else if (states[rowIndex, colIndex] == state.End)
                    {
                        btnList[pos].Text = "E";
                    }
                    else if (states[rowIndex, colIndex] == state.Hurdle)
                    {
                        btnList[pos].Text = "";
                        btnList[pos].BackColor = Color.Black;
                    }
                    else
                    {
                        btnList[pos].BackColor = SystemColors.Control;
                    }
                    pos++;
                }

            btnSolve.Enabled = true;
            lblProgress.Text = "Generated";
        }

        public void ShowState(int position, state newState)
        {
            Button btn = btnList[position];
            switch (newState)
            {
                case state.Backtracked:
                    btn.BackColor = SystemColors.ControlDark;
                    btn.Text = "B";
                    break;
                case state.TraversedToEast:
                    btn.Text = "\u2192";
                    break;
                case state.TraversedToWest:
                    btn.Text = "\u2190";
                    break;
                case state.TraversedToNorth:
                    btn.Text = "\u2191";
                    break;
                case state.TraversedToSouth:
                    btn.Text = "\u2193";
                    break;
            }
            Application.DoEvents();
            Thread.Sleep(200);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            lblProgress.Text = "Solving...";
            //Application.DoEvents();
            int currentPos = START_POS;
            currentPos = MazeProcessor.solve(currentPos);

            if (currentPos == -1)
            {
                lblProgress.Text = "No solution!";
                MessageBox.Show("No Solution!");
            }

            if (currentPos == END_POS)
            {
                lblProgress.Text = "Solved";
                MessageBox.Show("Solved!");
            }
            btnSolve.Enabled = false;
        }

        private void graphicalView_Load(object sender, EventArgs e)
        {

        }
    }
}
