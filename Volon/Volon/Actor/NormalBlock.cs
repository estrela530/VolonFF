using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Volon.Device;
using Volon.Scene;
using Volon.Static;

namespace Volon.Actor
{
    class NormalBlock : Character
    {

        private float speed;
        private Random rnd = new Random();
        private bool on;


        public NormalBlock(Vector2 position, IGameMediator mediator)
            : base("NormalBlock", 200, 50,30,50, mediator)
        {
            this.position = position;
            isDeadFlag = false;
        }

        public override void Initialize()
        {
            speed = rnd.Next(10, 13);
            isDeadFlag = false;
            on = false;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //x軸0以下で死亡
            if (position.X <= -200 || position.Y >= 1280)
            {
                isDeadFlag = true;
            }
            if (StaticBool.Up == true)
            {
                position.Y -= StaticInt.PlayerPower;
            }
            //Move();
        }
        public override void Hit(Character other)
        {
            if (other is Player)
            {
                on = true;
            } 
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public void Move()
        {
            position.X -= speed;
            if (on == true)
            {
                position.Y += 6;
            }
        }

        public override void SpecialHit(Character other)
        {
        }

    }
}
