using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Volon.Device;
using Volon.Actor;
using Volon.Static;

namespace Volon.Scene
{
    class GamePlay : IScene, IGameMediator
    {
        //フィールド
        // 終了しているかどうか
        private bool isEndFlag;
        // サウンド
        private Sound sound;
        // レンダラー
        private Renderer renderer;
        //Playerクラス
        private Player player;

        //IgameMediator
        private IGameMediator igameMediator;
        private List<Character> characters;
        private BlockManager blockManager;
        private int back, back2, back3, back4, back5, back6,R,G,B;
        private int interval, num, num2;
        private Random rnd = new Random();
        private List<int> numbers;
        private float backY, score,alpha;

        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            renderer = gameDevice.GetRenderer();
            player = new Player(igameMediator);
            blockManager = new BlockManager();
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("backcolor", Vector2.Zero,alpha);
            renderer.DrawTexture("background1", new Vector2(back, backY));
            renderer.DrawTexture("background1", new Vector2(back4, backY));
            renderer.DrawTexture("background2", new Vector2(back,backY+ 720)/*, new Vector2(1.0f, 0.7f)*/, 0.5f);
            renderer.DrawTexture("background2", new Vector2(back4,backY + 720)/*, new Vector2(1.0f, 0.7f)*/, 0.5f);
            renderer.DrawTexture("cloud", new Vector2(back2, 0), 0.5f);
            renderer.DrawTexture("cloud", new Vector2(back5, 0), 0.5f);
            renderer.DrawTexture("cloud2", new Vector2(back3, 0), 0.3f);
            renderer.DrawTexture("cloud2", new Vector2(back6, 0), 0.3f);
            blockManager.Draw(renderer);
            #region　数字//角煮用
            if ((int)score / 1000 <= 9 && (int)score / 1000 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, score.ToString("f2"), 7);
            }
            if ((int)score / 100 <= 9 && (int)score / 100 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, score.ToString("f2"), 6);
            }
            if ((int)score / 10 <= 9 && (int)score / 10 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, score.ToString("f2"), 5);
            }
            renderer.DrawNumber("number", Vector2.Zero, score.ToString("f2"), 4);
            #endregion
            renderer.End();
        }

        public void Initialize()
        {
            Player player = new Player(this);
            isEndFlag = false;
            blockManager = new BlockManager();//ブロック管理者を生成
            blockManager.Add(player);
            numbers = new List<int>();
            for (int i = -5; i < 12; i++)
            {
                numbers.Add(i);
            }

            //blockManager.Add(new NormalBlock(new Vector2(500, 550), igameMediator));//テンプレート
            //blockManager.Add(new NormalBlock(new Vector2(1050, 250), igameMediator));//佐々木

            //75クリティカル　150ノーマル
            blockManager.Add(new NormalBlock(new Vector2(75, 550), igameMediator));//テンプレート
            blockManager.Add(new NormalBlock(new Vector2(1050, 250), igameMediator));//佐々木

            back = 0;
            back2 = 0;
            back3 = 0;
            back4 = 1280;
            back5 = 1280;
            back6 = 1280;
            interval = 0;
            num2 = 0;
            score = 0;
            backY = 0;
            alpha = 1.0f;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public SceneName Next()
        {
            return SceneName.GameEnding;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space)/* || blockManager.PlayerIsDead()==true*/)
            {
                isEndFlag = true;
            }
            player.Update(gameTime);
            if (StaticInt.PlayerPower<0&&blockManager.PlayerPos()==100)
            {
                score-= StaticInt.PlayerPower/10;
                backY -= StaticInt.PlayerPower / 10;
                if (score > 1200)
                {
                    alpha -= 0.0015f;
                }
            }
            blockManager.Update(gameTime);
            #region ランダム生成
            if (numbers.Count == 0)
            {
                for (int i = -5; i < 12; i++)
                {
                    numbers.Add(i);
                }
            }
            interval++;
            if (interval >= rnd.Next(50, 100) && num2 == 0 ||
                interval >= rnd.Next(50, 100) && num2 == 2)
            {
                num = numbers[rnd.Next(numbers.Count)];
                blockManager.Add(new ThornsBlock(new Vector2(1280, (num * 100)), igameMediator));
                if (score < 1000)
                {
                    blockManager.Add(new ThornsBlock(new Vector2(1100, 550 - rnd.Next(150)), igameMediator));
                }
                else
                {
                    blockManager.Add(new NormalBlock(new Vector2(1100, 550 - rnd.Next(150)), igameMediator));
                }
                numbers.RemoveAll(c => c.ToString().Contains(num.ToString()));
                interval = 0;
                num2+=rnd.Next(0,2);
            }
            if (interval >= rnd.Next(50, 100) && num2 == 1)
            {
                num = numbers[rnd.Next(numbers.Count)];
                blockManager.Add(new NormalBlock(new Vector2(1280, (num * 100)), igameMediator));
                blockManager.Add(new ThornsBlock(new Vector2(1100, 550 - rnd.Next(150)), igameMediator));
                numbers.RemoveAll(c => c.ToString().Contains(num.ToString()));
                interval = 0;
                num2++;
            }
            if (interval >= rnd.Next(50, 100) && num2 == 3)
            {
                num = numbers[rnd.Next(numbers.Count)];
                blockManager.Add(new GravityBlock(new Vector2(1280, (num * 100)), igameMediator));
                numbers.RemoveAll(c => c.ToString().Contains(num.ToString()));
                interval = 0;
                num2++;
            }
            if (interval >= rnd.Next(20, 60) && num2 ==4)
            {
                num = numbers[rnd.Next(numbers.Count)];
                blockManager.Add(new SpecialBlock(new Vector2(1280, (num * 100)), igameMediator));
                blockManager.Add(new ThornsBlock(new Vector2(1100, 550-rnd.Next(150)), igameMediator));
                numbers.RemoveAll(c => c.ToString().Contains(num.ToString())); 
                num2 = 0;
            }
            
            #endregion
            #region 背景
            back -= 1;
            if (back <= -1280)
            {
                back = 0;
            }
            back2 -= 3;
            if (back2 <= -1280)
            {
                back2 = 0;
            }
            back3 -= 2;
            if (back3 <= -1280)
            {
                back3 = 0;
            }
            back4 -= 1;
            if (back4 <= 0)
            {
                back4 = 1280;
            }
            back5 -= 3;
            if (back5 <= 0)
            {
                back5 = 1280;
            }
            back6 -= 2;
            if (back6 <= 0)
            {
                back6 = 1280;
            }
            #endregion
        }
    }
}
