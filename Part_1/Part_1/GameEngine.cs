using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_1
{
    static class GameEngine
    {

        public static int rounds = 0; // varaible to keep track of the amount of rounds that have passed
        static int numOfEnemies = 12; // variable to dectate the amount of units that need to be spawned
        public static Map map; // creates an object of the map class
        static bool initialised = false; // tells the class if variables are initialised before trying to use them

        // method runs every round and perfirms the nessesary actions that are needed to be done every round
        public static void Round()
        {
            if (initialised == false)
            {
                map = new Map(numOfEnemies);
                initialised = true;
            }

            Program.UI.txtUnitInfo.Text = ""; // resets the text in the information box

            foreach (Unit u in map.units)
            {
                if (u.Health > 0) // checks if the unit is alive or not
                {
                    Unit unit;
                    if (u.GetType() == typeof(MeeleeUnit)) //  gets the type of unit
                    {
                        unit = u as MeeleeUnit;
                    }
                    else
                    {
                        unit = u as RangedUnit;
                    }

                    Unit enemy = unit.FindClosestUnit(map.units);
                    if (enemy != null) // checks the type of unit and if there is a unit
                        if (enemy.GetType() == typeof(MeeleeUnit))
                        {
                            enemy = enemy as MeeleeUnit;
                        }
                        else
                        {
                            enemy = enemy as RangedUnit;
                        }

                    if (!unit.DestroyUnit()) // checks if the unit is alive
                    {
                        if (unit.Health / unit.MaxHealth >= 0.25)
                        {
                            if (unit.IsInRange(enemy)) // checks if the unit is in range of the enemy
                            {
                                try
                                {
                                    unit.IsAttacking = true;
                                    enemy.Combat(u.Attack);
                                    enemy.DestroyUnit();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                            else
                            {
                                try
                                {
                                    unit.IsAttacking = false;
                                    unit.Move(unit.DirectionOfEnemy(enemy), 1);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                        }
                        else
                        {
                            unit.IsAttacking = false;
                            Random r = new Random();
                            u.Move((Direction)r.Next(), 1);
                        }
                    }
                    unit.DestroyUnit();
                    Program.UI.txtUnitInfo.AppendText(u.ToString() + "\n\n");
                }
            }
            map.UpDatePosition();
            rounds++;
            Program.UI.RoundUpdate(rounds);
        }
    }
}
