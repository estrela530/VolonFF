using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Volon.Device;

namespace Volon.Actor
{
    class BlockManager
    {
        //フィールド
        private List<Character> blocks;
        private List<Character> players;
        private List<Character> addNewList;

        private Renderer renderer;

        public BlockManager()
        {
            var gameDevice = GameDevice.Instance();
            renderer = gameDevice.GetRenderer();
            Initialize();
        }
        public void Initialize()
        {

            if (blocks != null)
            {
                blocks.Clear();
            }
            else
            {
                blocks = new List<Character>();
            }
            if (players != null)
            {
                players.Clear();
            }
            else
            {
                players = new List<Character>();
            }
            if (addNewList != null)
            {
                addNewList.Clear();
            }
            else
            {
                addNewList = new List<Character>();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var p in players)
            {
                p.Update(gameTime);
            }
            foreach (Character c in blocks)
            {
                c.Update(gameTime);
            }
            foreach (var tuika in addNewList)
            {
                if (tuika is Player)
                {
                    tuika.Initialize();
                    players.Add(tuika);
                }
                else if (tuika is NormalBlock)
                {
                    tuika.Initialize();
                    blocks.Add(tuika);
                }
                else if (tuika is SpecialBlock)
                {
                    tuika.Initialize();
                    blocks.Add(tuika);
                }
                else if (tuika is GravityBlock)
                {
                    tuika.Initialize();
                    blocks.Add(tuika);
                }
                else if (tuika is ThornsBlock)
                {
                    tuika.Initialize();
                    blocks.Add(tuika);
                }
            }
            addNewList.Clear();

            HitToBlocks();

            blocks.RemoveAll(c => c.IsDead() == true);//
        }
        //public void Add(Character character)
        //{
        //    if (character.ToString().Contains("Block"))
        //    {
        //        blocks.Add(character);
        //    }

        //}
        public void Add(Character character)
        {
            if (character == null)
            {
                return;
            }
            addNewList.Add(character);
        }

        private void HitToBlocks()
        {
            foreach (var player in players)
            {
                foreach (var block in blocks)
                {
                    if (player.IsDead())
                    {
                        continue;
                    }
                    if (player.IsCollision(block))
                    {
                        player.Hit(block);
                        block.Hit(player);
                    }
                    else if (player.IsSpecialCollision(block))
                    {
                        player.SpecialHit(block);
                    }

                }
            }
        }
       
        public void Draw(Renderer renderer)
        {
            foreach (Character c in blocks)
            {
                c.Draw(renderer);
            }
            foreach (var p in players)
            {
                p.Draw(renderer);
            }
        }
        public bool PlayerIsDead()
        {
            bool d = false;
            foreach(var p in players)
            {
                 d=p.IsDead();
            }
            return d;
        }
        public float PlayerPos()
        {
            float f = 0;
            foreach (var p in players)
            {
                f = p.GetY();
            }
            return f;
        }
    }
}
