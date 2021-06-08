﻿using System;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            public static string mins;
            public static string hours;
            public static bool run;
            public static string space;

            // selectorState is the selector box
            // minsState is the box where you input minutes
            // timerBox is where the time counts down

        }

        public Form1()
        {
            InitializeComponent();
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectorState.Text == "Tweet")
            {
                globalVariables.executionType = "tweet";
            }
            if (selectorState.Text == "Custom Program")
            {
                globalVariables.executionType = "custom";
            }

        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            globalVariables.mins = minsState.Text;
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            globalVariables.run = false;
            globalVariables.hours = "0";
            globalVariables.mins = "0";
            minsState.Text = "";
            timerBox.Text = "00:00";
            minsState.Show();
            runButton.Show();
        }


        public void runButton_Click(object sender, EventArgs e)
        {
            Thread minsToHours = new Thread(new ThreadStart(conversion));

            globalVariables.run = true;
            try
            {
                minsToHours.Start();
            }
            catch (System.ArgumentNullException)
            {
                errorMessage.Text = "Enter a number.";
                globalVariables.run = false;
            }
            catch (System.FormatException)
            {
                errorMessage.Text = "Enter a number without a decimal";
                globalVariables.run = false;
            }
        }
        
        private void conversion()
        {
            Thread executionTimer = new Thread(new ThreadStart(timer));

            if (globalVariables.run == true)
            {
                double minsToDouble = double.Parse(globalVariables.mins); // converts minutes to double

                
                minsState.Invoke((MethodInvoker)(() => minsState.Hide()));
                runButton.Invoke((MethodInvoker)(() => runButton.Hide()));

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
                executionTimer.Start();
            }
        }

        private void timer()
        {
            int delayTime;
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

            delayTime = 10000;

            while (globalVariables.run == true)
            {   
                while (globalVariables.run == true)
                {
                    if (Int32.Parse(globalVariables.hours) == 0 && Int32.Parse(globalVariables.mins) == 0)
                    {
                        globalVariables.text = "END";
                        timerBox.Invoke((MethodInvoker)(() => timerBox.Text = globalVariables.text)); // avoids cross thread execution error
                    }

                    if (Int32.Parse(globalVariables.hours) <= 0)
                    {
                        globalVariables.hours = "59";
                        globalVariables.mins = "59";
                        delayTime = 1000;
                    }

                    Thread.Sleep(delayTime);
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
                    break;
                }

                globalVariables.text = $"{globalVariables.hours}:{globalVariables.space}{globalVariables.mins}";
                timerBox.Invoke((MethodInvoker)(() => timerBox.Text = globalVariables.text)); // avoids cross thread execution error

            }

            
        }
    }   
}

