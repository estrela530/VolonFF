using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Volon.Device;
using Volon.Actor;
using Volon.Static;

namespace Volon.Scene
{
    class Tutorial : IScene, IGameMediator
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
        private BlockManager blockManager;
        private int back, back2, back3, back4, back5, back6;
        private SceneName nextScene;
        private int interval;

        public Tutorial()
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
            #region　背景
            renderer.DrawTexture("background1", new Vector2(back, 0));
            renderer.DrawTexture("background1", new Vector2(back4, 0));
            renderer.DrawTexture("background2", new Vector2(back, 720), new Vector2(1.0f, 0.7f), 0.5f);
            renderer.DrawTexture("background2", new Vector2(back4, 720), new Vector2(1.0f, 0.7f), 0.5f);
            renderer.DrawTexture("cloud", new Vector2(back2, 0), 0.5f);
            renderer.DrawTexture("cloud", new Vector2(back5, 0), 0.5f);
            renderer.DrawTexture("cloud2", new Vector2(back3, 0), 0.3f);
            renderer.DrawTexture("cloud2", new Vector2(back6, 0), 0.3f);
            #endregion 
            blockManager.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            Player player = new Player(this);
            isEndFlag = false;
            blockManager = new BlockManager();//ブロック管理者を生成
            blockManager.Add(player);
            nextScene = SceneName.Tutorial;
            interval = 0;
            back = 0;
            back2 = 0;
            back3 = 0;
            back4 = 1280;
            back5 = 1280;
            back6 = 1280;
            blockManager.Add(new NormalBlock(new Vector2(700, 1000), igameMediator));
            blockManager.Add(new NormalBlock(new Vector2(1000, 500), igameMediator));
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public SceneName Next()
        {
            return nextScene;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (blockManager.PlayerIsDead() == true)
            {
                isEndFlag = true;
            }
            if (Input.GetKeyTrigger(Keys.Space))
            {
                nextScene = SceneName.GamePlay;
                isEndFlag = true;
            }
            player.Update(gameTime);
            blockManager.Update(gameTime);
            #region チュートリアル
            interval++;
            if (NowCurrentScene.TutorialState == 0)
            {
                if (interval >= 50)
                {
                    blockManager.Add(new NormalBlock(new Vector2(1280,300),igameMediator));
                    interval = 0;
                }
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
