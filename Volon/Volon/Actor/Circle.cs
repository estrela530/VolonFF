//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Volon.Device;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework;

//namespace Volon.Actor
//{
//    class Circle
//    {
//        private float scale, scale2;
//        private Vector2 vector,position;
//        private Renderer renderer;

//        public Circle(Vector2 position)
//        {
//            var gameDevice = GameDevice.Instance();

//            renderer = gameDevice.GetRenderer();
//            scale = 0.01f;
//            vector = new Vector2(scale);
//            this.position = position;
//        }
//        public void Initialize()
//        {
//            scale = 0.01f;
//            scale = 500;
//            vector = new Vector2(scale);
//        }
//        public void Draw()
//        {
//            renderer.Begin();
//            renderer.DrawTexture("Circle", new Vector2(450), 0.4f);
//            renderer.DrawTexture("Circle", new Vector2(scale2), vector);
//            renderer.End();
//        }
//        public void Update(GameTime gameTime)
//        {
//            Draw();
//            if (Input.GetKeyState(Keys.Space))
//            {
//                scale += 0.01f;
//                scale2 -= 0.5f;
//            }
//            if (Input.GetKeyRelease(Keys.Space))
//            {
//                scale = 0.01f;
//                scale2 = 500;
//            }
//            vector = new Vector2(scale);
//        }
//    }
//}
