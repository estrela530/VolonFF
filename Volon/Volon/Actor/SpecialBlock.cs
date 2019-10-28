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
    class SpecialBlock : Character
    {

        private float speed;
        private bool on;

        public SpecialBlock(Vector2 position, IGameMediator mediator)
            : base("SpecialBlock", 120, 35,0,0, mediator)
        {
            speed = 18;

            this.position = position;
        }

        public override void Initialize()
        {
            isDeadFlag = false;
            on = false;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //x軸0以下で死亡
            if (position.X <= -120 || position.Y >= 1280)
            {
                isDeadFlag = true;
            }
            if (StaticBool.Up == true)
            {
                position.Y -= StaticInt.PlayerPower;
            }
            Move();
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
                position.Y -= StaticInt.PlayerPower;
            }
        }
        public override void SpecialHit(Character other)
        {
        }

    }
}
