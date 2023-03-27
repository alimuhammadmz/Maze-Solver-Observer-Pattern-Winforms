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
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;

namespace Assignment3
{
    public partial class textualView : Form, IObserver
    {
        private List<Button> btnList = new List<Button>();
        private static int SIZE = 20;
        private static int START_POS = 0;
        private static int END_POS = 399;
        private MazeProcess MazeProcessor;                      //object of observable

        public textualView(MazeProcess maze)
        {
            InitializeComponent();
            this.SuspendLayout();
      
            this.MazeProcessor = maze;
            MazeProcessor.add(this);
            this.ResumeLayout();
        }

        public void SetState(int position, state newState)
        {
            MazeProcessor.SetState(position, newState);
            ShowState(position, newState);
        }

        public void ShowState(int position, state newState)
        {
            switch (newState)
            {
                case state.Backtracked:
                    if (textBox.InvokeRequired)
                    {
                        textBox.Invoke((Action)(() => {
                            textBox.Text = "Backtracked to previously moved cell";
                        }));
                    }
                    break;
                case state.TraversedToEast:
                    if (textBox.InvokeRequired)
                    {
                        textBox.Invoke((Action)(() => {
                            textBox.Text = "Moved from left to right cell";
                        }));
                    }                    break;
                case state.TraversedToWest:
                    if (textBox.InvokeRequired)
                    {
                        textBox.Invoke((Action)(() => {
                            textBox.Text = "Moved from right to left cell";
                        }));
                    }
                    break;
                case state.TraversedToNorth:
                    if (textBox.InvokeRequired)
                    {
                        textBox.Invoke((Action)(() => {
                            textBox.Text = textBox.Text = "Moved from downward to upward cell";
                        }));
                    }
                    break;
                case state.TraversedToSouth:
                    if (textBox.InvokeRequired)
                    {
                        textBox.Invoke((Action)(() => {
                            textBox.Text = textBox.Text = "Moved from upward to downward cell";
                        }));
                    }
                    break;
            }
            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(200);
        }

        private void textualView_Load(object sender, EventArgs e)
        {

        }
    }
}
