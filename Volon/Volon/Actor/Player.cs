using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Volon.Def;
using Volon.Device;
using Volon.Scene;
using Volon.Util;
using Volon.Static;

namespace Volon.Actor
{
    class Player : Character
    {
        //フィールド
        private Sound sound;
        public static bool IsDescentFlag;
        private float num = 0f;
        private Timer timer;
        private float playerMoveSeconds = 0;
        private float splashMountainSeconds = 0;
        //float power = 0;
        float firstPower = -2.0f;

        //追加 かいと
        private Vector2 previousPos;
        private Vector2 currentPos;
        private float distance;

        private ParticleEmitter emitter;
        private Vector2 assetSize;

        //当たり判定用enum
        private enum Direction
        {
            Down, UP, RIGHT, LEFT
        };

        public Player(IGameMediator mediator)
              : base("Player", 60, 60,0,0, mediator)
        {
            position = new Vector2(100, 100);
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            IsDescentFlag = false;
            isDeadFlag = false;

            //追加
            assetSize = new Vector2(60, 60);
            emitter = new ParticleEmitter();
            StaticBool.Up = false;
        }

        public override void Initialize()
        {
            position = new Vector2(150, 100);

            timer = new CountDownTimer(2);
            StaticInt.PlayerPower = 0;
        }

       

        public override void Update(GameTime gametime)
        {
            if (position.Y >= 1300)
            {
                isDeadFlag = true;
            }
            float delta = (float)gametime.ElapsedGameTime.TotalSeconds;

            //当たり判定
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);

            //移動用メソッド実装
            PlayerRiseMove();
            if (StaticInt.PlayerPower <= 0 && position.Y <= 100)
            {
                position.Y = 100;
                StaticBool.Up = true;
            }
            else
            {
                StaticBool.Up = false;
            }
            if (Input.GetKeyTrigger(Keys.D))
            {
                IsDescentFlag = true;
            }
            if (position.Y >= Screen.Height - 64)
            {
                //isDeadFlag = true;
            }

            if (IsDescentFlag == true)
            {
                SplashMountain();
            }

            //追加
            if (isDeadFlag == false)
            {
                emitter.Emit("Player", assetSize, position + assetSize / 2, 0.5f, 0.5f, 2f, 1, 600, Color.Black);
            }
            emitter.Update(delta);
        }

        //Playerが昇る
        //Moveのためのメソッド
        /// <summary>
        /// 上昇メソッド
        /// </summary>
        public void PlayerRiseMove()
        {
            ////今と前のポジション受け取り
            //previousPos = currentPos;
            //currentPos = position;

            ////距離
            //distance = Vector2.Distance(currentPos, previousPos);

            ////速さ？
            //float speed = distance / playerMoveSeconds;



            //position.X += 3.0f;
            playerMoveSeconds += 1;

            if (IsDescentFlag == false)
            {
                #region IsTimeお試し
                //position.Y += -10.0f;
                //if (timer.IsTime())
                //{
                //    position.Y += -20.0f;
                //}

                //今のところの完成
                //if (seconds >= 0 && seconds < 100)
                //{
                //    power += -0.1f;
                //    position.Y += firstPower;
                //    position.Y += power;
                //}
                //if (seconds >= 100)
                //{
                //    power += 0.3f;
                //    position.Y += power;
                //}
                #endregion

                #region 恥ずかしい上昇処理
                if (playerMoveSeconds >= -100 && playerMoveSeconds < 20)
                {
                    StaticInt.PlayerPower = firstPower;
                    StaticInt.PlayerPower += -0.2f;
                    if (position.Y <= 100)
                    {
                        return;
                    }
                    position.Y += StaticInt.PlayerPower;
                }
                else if (playerMoveSeconds >= 20)
                {
                    StaticInt.PlayerPower += 0.1f;
                    position.Y += StaticInt.PlayerPower;
                }
                #endregion
            }

            #region　恥ずかしいパーティクル確認用
            //パーティクル確認用
            if (Input.GetKeyTrigger(Keys.F))
            {
                IsDescentFlag = false;
                playerMoveSeconds = 0;
                splashMountainSeconds = 0;
                StaticInt.PlayerPower = 0;
                firstPower = -15.0f;
            }
            #endregion
        }

        public override void Shutdown()
        {
            sound.StopBGM();
        }

        public override void Hit(Character other)
        {
            if (other is NormalBlock && IsDescentFlag == true)
            {
                IsDescentFlag = false;
                playerMoveSeconds = 0;
                splashMountainSeconds = 0;
                StaticInt.PlayerPower = 0;
                firstPower = -15.0f;
            }
            else if (other is GravityBlock && IsDescentFlag == true)
            {
                IsDescentFlag = false;
                playerMoveSeconds = 0;
                splashMountainSeconds = 0;
                StaticInt.PlayerPower = 0;
                firstPower = -5.0f;
            }
            else if (other is SpecialBlock && IsDescentFlag == true)
            {
                IsDescentFlag = false;
                playerMoveSeconds = -50;
                splashMountainSeconds = 0;
                StaticInt.PlayerPower = 0;
                firstPower = -30.0f;
            }
            else if (other is ThornsBlock && IsDescentFlag == true)
            {
                IsDescentFlag = false;
                playerMoveSeconds = 0;
                splashMountainSeconds = 0;
                StaticInt.PlayerPower = 0;
                firstPower = -15.0f;
            }
        }

        public override void SpecialHit(Character other)
        {
            if (other is NormalBlock && IsDescentFlag == true)
            {
                IsDescentFlag = false;
                playerMoveSeconds = -50;
                splashMountainSeconds = 0;
                StaticInt.PlayerPower = 0;
                firstPower = -15.0f;
            }
            else if (other is ThornsBlock && IsDescentFlag == true)
            {
                isDeadFlag = true;
            }
        }
        public override void Draw(Renderer renderer)
        {
            //追加
            emitter.Draw(renderer);

            renderer.DrawTexture(name, position);
        }

        public void SplashMountain()
        {
            #region 急降下
            splashMountainSeconds += 1;
            //初速
            if (splashMountainSeconds >= 0 && splashMountainSeconds < 50)
            {
                StaticInt.PlayerPower += 5.0f;
                position.Y += StaticInt.PlayerPower;
            }
            //加速
            else if (splashMountainSeconds >= 50)
            {
                StaticInt.PlayerPower += 2.5f;
                position.Y += StaticInt.PlayerPower;
            }
            #endregion
        }
    }
}