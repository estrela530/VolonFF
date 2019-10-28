//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Volon.Device;
//using Volon.Scene;

//namespace Volon.Actor
//{
//    class SpawnBlock
//    {
//        List<Character> blockList;
//        IGameMediator mediator;
//        Random rnd;


//        public SpawnBlock()
//        {
//            blockList = new List<Character>();
//            var gameDevice = GameDevice.Instance();
//            rnd = gameDevice.GetRandom();
//        }

//        public void Initialize()
//        {
//            blockList.Clear();
//        }

//        public void Update(GameTime gametime)
//        {
//            foreach (var b in blockList)
//                b.Update(gametime);

//            Spawn();
//        }

//        public void Draw()
//        {

//        }

//        private void Spawn()
//        {
//            int count = 0;

//            switch (count)
//            {
//                case 0:
//                    blockList.Add(new NormalBlock(mediator));
//                    break;
//            }
//        }
//    }
//}
