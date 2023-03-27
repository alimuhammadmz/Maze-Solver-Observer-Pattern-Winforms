using Assignment3.Processor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Assignment3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        static void RunGraphicalView(graphicalView view)
        {
            Application.Run(view);
        }
        public static void RunTextualView(textualView view)
        {
            Application.Run(view);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int SIZE = 20;
            int START_POS = 0;
            int END_POS = 399;
            MazeProcess maze = new MazeProcess(SIZE, START_POS, END_POS);
            graphicalView graphicalViewer = new graphicalView(maze);      //1st observer
            textualView textualViewer = new textualView(maze);            //2nd observer
            maze.InitializeMaze();

            ThreadStart graphicalView = new ThreadStart(() => RunGraphicalView(graphicalViewer));
            ThreadStart textualView = new ThreadStart(() => RunTextualView(textualViewer));

            Thread thread1 = new Thread(graphicalView);
            Thread thread2 = new Thread(textualView);

            thread2.Start();
            thread1.Start();
        }
    }
}
