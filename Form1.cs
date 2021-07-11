using System;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemCheck
{
    public partial class Form1 : Form
    {
        static class globalVariables
        {
            public static string text;
            public static string executionType;
            public static string executionPath;
            public static string mins;
            public static string originalMins;
            public static string hours;
            public static bool secondMode = false;
            public static bool run;
            public static bool time;
            public static string space;
            public static bool checkIn;

            // selectorState is the selector box
            // minsState is the box where you input minutes
            // timerBox is where the time counts down

        }

        public Form1()
        {
            InitializeComponent();
        }


        
        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            globalVariables.mins = minsState.Text;
            globalVariables.originalMins = minsState.Text;
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            globalVariables.run = false;
            globalVariables.hours = "0";
            globalVariables.mins = "0";
            minsState.Text = "";
            timerBox.Text = string.Empty;

            ovveride.Hide();
            checkIn.Hide();
            abortButton.Hide();
            minsState.Show();
            runButton.Show();
        }

        public void runButton_Click(object sender, EventArgs e)
        {
            void run()
            {
                Thread minsToHours = new Thread(new ThreadStart(conversion));
                globalVariables.run = true;
                globalVariables.checkIn = false;
                timer1.Interval = 1000;
                minsToHours.Start();
            }

            if (selectorState.Text == "")
            {
                globalVariables.run = false;
                globalVariables.hours = "0";
                globalVariables.mins = "0";
                minsState.Text = "";
                timerBox.Text = string.Empty;

                ovveride.Hide();
                checkIn.Hide();
                abortButton.Hide();
                minsState.Show();
                runButton.Show();

                MessageBox.Show("Select an Execution Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (selectorState.Text == "Custom Program")
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "Executable Files (*.exe)|*.exe";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    globalVariables.executionPath = openFile.FileName;
                }
                run();
            }

            ovveride.Invoke((MethodInvoker)(() => ovveride.Show()));
            checkIn.Invoke((MethodInvoker)(() => checkIn.Show()));
            abortButton.Invoke((MethodInvoker)(() => abortButton.Show()));

            globalVariables.executionType = selectorState.Text;

            
        }

        private void checkIn_Click(object sender, EventArgs e)
        {
            globalVariables.checkIn = true;
            globalVariables.mins = globalVariables.originalMins;
            conversion();
        }

        private void ovveride_Click(object sender, EventArgs e)
        {
            Thread executionEvent = new Thread(new ThreadStart(execution));
            executionEvent.Start();
        }

        private void conversion()
        {
            Thread executionTimer = new Thread(new ThreadStart(timer));

            minsState.Invoke((MethodInvoker)(() => minsState.Hide()));
            runButton.Invoke((MethodInvoker)(() => runButton.Hide()));

            try
            {

                if (globalVariables.run == true)
                {
                    if (Int32.Parse(globalVariables.originalMins) < 60)
                    {
                        globalVariables.hours = globalVariables.originalMins;
                        globalVariables.mins = "00";
                    }
                    else
                    {
                        double minsToDouble = double.Parse(globalVariables.mins); // converts minutes to double

                        if (minsToDouble % 60 == 0) // if the mins go into 60 with no remainder
                        {
                            string hoursCalculation = $"{minsToDouble} / 60";
                            string hours = new DataTable().Compute(hoursCalculation, null).ToString(); // calculates line above
                            globalVariables.hours = hours;
                        }
                        else
                        {
                            double doubleHours = minsToDouble / 60;
                            doubleHours = Math.Floor(doubleHours); //removes decimals
                            globalVariables.hours = doubleHours.ToString();
                        }

                        int remainderFromHours = Int32.Parse(globalVariables.mins) % 60;
                        globalVariables.mins = remainderFromHours.ToString();
                        globalVariables.time = true;
                        executionTimer.Start();
                    }
                    
                }
            }
            catch
            {
                ovveride.Invoke((MethodInvoker)(() => ovveride.Hide()));
                checkIn.Invoke((MethodInvoker)(() => checkIn.Hide()));
                abortButton.Invoke((MethodInvoker)(() => abortButton.Hide()));

                minsState.Invoke((MethodInvoker)(() => minsState.Show()));
                runButton.Invoke((MethodInvoker)(() => runButton.Show()));

                globalVariables.run = false;
                globalVariables.hours = "0";
                globalVariables.mins = "0";
                minsState.Text = "";
                timerBox.Text = string.Empty;

                MessageBox.Show("Check you have entered your time in minutes using only numbers without decimals", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void timer()
        {

            if (Int32.Parse(globalVariables.mins) < 10)
            {
                globalVariables.space = "0";
            }
            else
            {
                globalVariables.space = "";
            }

            globalVariables.text = $"{globalVariables.hours}:{globalVariables.space}{globalVariables.mins}";
            timerBox.Invoke((MethodInvoker)(() => timerBox.Text = globalVariables.text)); // avoids cross thread execution error
        }

        

        private void execution()
        {
            timer1.Interval = 10000000;
            timerBox.Invoke((MethodInvoker)(() => timerBox.Text = "00:00"));

            if (globalVariables.executionType == "Custom Program")
            {
                Process.Start(globalVariables.executionPath);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (globalVariables.run == true)
                {
                    if (Int32.Parse(globalVariables.originalMins) < 60)
                    {
                        try
                        {
                            if (Int32.Parse(globalVariables.hours) == 0 && Int32.Parse(globalVariables.mins) == 0)
                            {
                                if (globalVariables.checkIn == false)
                                {
                                    globalVariables.run = false;

                                    Thread executionEvent = new Thread(new ThreadStart(execution));
                                    executionEvent.Start();
                                }
                            }

                            if (globalVariables.checkIn == true)
                            {
                                globalVariables.checkIn = false;
                                globalVariables.time = false;
                            }

                            int mins;
                            mins = Int32.Parse(globalVariables.mins) - 1; // update text, could be optimised
                            globalVariables.mins = mins.ToString();


                            if (Int32.Parse(globalVariables.mins) < 0)
                            {
                                globalVariables.mins = "59";
                                int hours;
                                hours = Int32.Parse(globalVariables.hours) - 1;
                                globalVariables.hours = hours.ToString();
                            }

                            if (Int32.Parse(globalVariables.mins) < 10)
                            {
                                globalVariables.space = "0";
                            }
                            else
                            {
                                globalVariables.space = "";
                            }

                            timerBox.Invoke((MethodInvoker)(() => timerBox.Text = $"{globalVariables.hours}:{globalVariables.space}{globalVariables.mins}"));
                        }
                        catch
                        {

                        }

                    }
                    else
                    {
                        try
                        {


                            if (globalVariables.secondMode == true)
                            {
                                timer1.Interval = 1000;
                            }
                            else
                            {
                                timer1.Interval = 60000;
                            }

                            if (Int32.Parse(globalVariables.hours) == 0 && Int32.Parse(globalVariables.mins) == 0)
                            {
                                if (globalVariables.checkIn == false)
                                {
                                    globalVariables.run = false;

                                    Thread executionEvent = new Thread(new ThreadStart(execution));
                                    executionEvent.Start();
                                }
                            }

                            if (globalVariables.checkIn == true)
                            {
                                globalVariables.checkIn = false;
                                globalVariables.time = false;
                            }

                            if (Int32.Parse(globalVariables.hours) == 0)
                            {


                                globalVariables.hours = "59";
                                globalVariables.mins = "59";
                                globalVariables.secondMode = true;
                            }

                            if (Int32.Parse(globalVariables.mins) < 0)
                            {
                                globalVariables.mins = "59";
                                int hours;
                                hours = Int32.Parse(globalVariables.hours) - 1;
                                globalVariables.hours = hours.ToString();
                            }

                            if (Int32.Parse(globalVariables.mins) < 10)
                            {
                                globalVariables.space = "0";
                            }
                            else
                            {
                                globalVariables.space = "";
                            }

                            timerBox.Invoke((MethodInvoker)(() => timerBox.Text = $"{globalVariables.hours}:{globalVariables.space}{globalVariables.mins}"));
                            int mins;
                            mins = Int32.Parse(globalVariables.mins) - 1; // update text, could be optimised
                            globalVariables.mins = mins.ToString();
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch
            {

            }
        }

    }   
}

