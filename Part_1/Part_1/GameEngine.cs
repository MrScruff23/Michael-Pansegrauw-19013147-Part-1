using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_1
{
    static class GameEngine
    {

        public static int rounds = 0;
        static int numOfEnemies = 12;
        public static Map map;
        static bool initialised = false;


        public static void Round()
        {
            if (initialised == false)
            {
                map = new Map(numOfEnemies);
                initialised = true;
            }

            Program.UI.txtUnitInfo.Text = "";

            foreach (Unit u in map.units)
            {
                if (u.Health > 0)
                {
                    Unit unit;
                    if (u.GetType() == typeof(MeeleeUnit))
                    {
                        unit = u as MeeleeUnit;
                    }
                    else
                    {
                        unit = u as RangedUnit;
                    }

                    Unit enemy = unit.FindClosestUnit(map.units);
                    if (enemy != null)
                        if (enemy.GetType() == typeof(MeeleeUnit))
                        {
                            enemy = enemy as MeeleeUnit;
                        }
                        else
                        {
                            enemy = enemy as RangedUnit;
                        }

                    if (!unit.DestroyUnit())
                    {
                        if (unit.Health / unit.MaxHealth >= 0.25)
                        {
                            if (unit.IsInRange(enemy))
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
