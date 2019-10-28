using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Volon.Device;
using Volon.Def;
using Volon.Scene;

namespace Volon.Actor
{


    abstract class Character
    {
        protected Vector2 position;
        protected string name;
        protected bool isDeadFlag;
        protected IGameMediator mediator;
        protected int width;//幅
        protected int height;//高さ
        protected int specialWidth;
        protected int specialHeight;

        public Character(string name, int width, int height, int specialWidth, int specialHeight, IGameMediator mediator)
        {
            this.name = name;
            position = Vector2.Zero;
            isDeadFlag = false;
            this.mediator = mediator;
            this.width = width;
            this.height = height;
            this.specialWidth = specialWidth;
            this.specialHeight = specialHeight;
        }
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Shutdown();
        public abstract void Hit(Character other);
        public abstract void SpecialHit(Character other);

        public bool IsDead()
        {
            return isDeadFlag;
        }
        public float GetY()
        {
            return position.Y;
        }
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }
        //public bool IsCollision(Character other)
        //{
        //    float length = (position - other.position).Length();

        //    float radiusSum = 64f;
        //    if (length <= radiusSum)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }

        public Rectangle GetRectangle()
        {
            //矩形の生成
            Rectangle area = new Rectangle();

            //位置と幅、高さを設定
            area.X = (int)position.X;
            area.Y = (int)position.Y;
            area.Height = height;
            area.Width = width;

            return area;
        }
        public Rectangle GetSpecialRectangle()
        {
            //矩形の生成
            Rectangle specialarea = new Rectangle();

            //クリティカル判定範囲の位置と幅、高さを設定
            specialarea.X = (int)position.X + 85;
            specialarea.Y = (int)position.Y;
            specialarea.Height = specialHeight;
            specialarea.Width = specialWidth;

            return specialarea;
        }
        public bool IsCollision(Character other)
        {
            //RectangleクラスのIntersectsメソッドで重なり判定
            return this.GetRectangle().Intersects(other.GetRectangle());
        }

        public bool IsSpecialCollision(Character other)
        {
            //RectangleクラスのIntersectsメソッドで重なり判定
            return this.GetRectangle().Intersects(other.GetSpecialRectangle());
        }
    }

}

