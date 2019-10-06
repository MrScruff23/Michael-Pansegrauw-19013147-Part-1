using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part_1
{
    public partial class UI : Form
    {
        public int rounds = 0; // variable to keep track of the number of rounds that have passed
        public UI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnStop_Click(object sender, EventArgs e) // When the stop button is clicked the timer will bet stopped to stop the animation
        {
            timer1.Enabled = false;
        }

        private void Timer1_Tick(object sender, EventArgs e) // each tick of the timer will triger a 'round' in the game engine 
        {
            GameEngine.Round();
        }

        private void BtnStart_Click(object sender, EventArgs e) // When the stop button is clicked the timer will bet resumed to resume the animation
        {
            timer1.Enabled = true;
        }

        public void RoundUpdate(int rounds) // updates the number of rounds that have passed
        {
            this.rounds = rounds;
            lblRound.Text = "Round: " + rounds;
        }
    }
}
