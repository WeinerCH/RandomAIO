﻿using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace RandomAIO.Plugins.Riven.Addon.Orb
{
    public static class Laneclear
    {
        private static AIHeroClient Riven => Player.Instance;

        public static Obj_AI_Base Target;

        public static void Get()
        {
            var m =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Riven.Position)
                    .FirstOrDefault(x => x.IsInRange(Riven, 450));

            if (m == null) return;

            if (MenuHandler.lane["q"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.Q.IsReady() && m.IsInRange(Riven, 450))
                {
                    Target = m;
                }
            }
            if (MenuHandler.lane["w"].Cast<CheckBox>().CurrentValue)
            {
                if (Riven.CountEnemyMinionsInRange(SpellHandler.W.Range) >=
                    MenuHandler.lane["wmin"].Cast<Slider>().CurrentValue && SpellHandler.W.IsReady())
                {
                    SpellHandler.W.Cast();
                    Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                }
            }
        }
    }
}