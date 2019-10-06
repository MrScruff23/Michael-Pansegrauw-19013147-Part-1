using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Part_1
{
    // constructor to initialise the map class values
    class Map
    {
        public Unit[,] map = new Unit[20, 20];
        public List<Unit> units = new List<Unit>();
        public List<Label> unitLables = new List<Label>();

        Random rand = new Random();

        public Map(int numberOfUnits)
        {
            for (int i = 0; i < numberOfUnits; i++)
            {
                units.Add(CreateUnit(i % 2));
            }
            DisplayAllUnits();
        }


        // Both the type of unit, as well as their X and Y position, should be randomised; 
        // Meelee: int xPos, int yPos, double maxHealth, double attack, int team
        // Ranged: int xPos, int yPos, double maxHealth, double attack, int team

        // method returns a unit after creating it and assigning valuse to it
        public Unit CreateUnit(int team)
        {
            int xPos = rand.Next(0, 20);
            int yPos = rand.Next(0, 20);
            while (true)
            {
                if (map[xPos, yPos] == null)
                {
                    break;
                }
                xPos = rand.Next(0, 20);
                yPos = rand.Next(0, 20);
            }

            if (rand.NextDouble() < 0.5) // deturmines whether a unit is a ranged unit or a meelee unit
            {
                return new MeeleeUnit(xPos, yPos, 60.0, 10.0, 1, team);
            }
            else
            {
                return new RangedUnit(xPos, yPos, 35.0, 7.0, 3, 2, team);
            }
        }

        // displays the units onto the GUI
        public void DisplayAllUnits()
        {
            int Ysize = Program.UI.grbMap.Height / 21;
            int Xsize = Program.UI.grbMap.Width / 21;

            foreach (Unit u in units)
            {
                Label l = new Label();
                l.Text = u.Symbol;
                l.Enabled = true;
                l.ForeColor = (u.Team == 0) ? Color.Red: Color.Blue;
                l.SetBounds(u.XPos * Xsize + 3, u.YPos * Ysize + 3, Xsize, Ysize);
                unitLables.Add(l);
            }

            foreach (Label lab in unitLables)
            {
                Program.UI.grbMap.Controls.Add(lab);
            }
        }

        // updates the position of the unit so it doesnt need to update the entire GUI
        public void UpDatePosition()
        {
            int Ysize = Program.UI.grbMap.Height / 20;
            int Xsize = Program.UI.grbMap.Width / 20;

            for (int i = 0; i < unitLables.Count; i++)
            {
                unitLables[i].SetBounds(units[i].XPos * Xsize + 3, units[i].YPos * Ysize + 3, Xsize, Ysize);
                unitLables[i].Text = units[i].Symbol;
                unitLables[i].Refresh();
            }
        }
    }
}
