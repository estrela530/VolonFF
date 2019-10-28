using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volon.Device;

namespace Volon.Actor
{
    class Particle
    {
        public string _name;        //画像
        public Vector2 _position;   //位置
        public Vector2 _direction;  //向き
        public Vector2 _origin;     //画像の原点
        public float _duration;     //寿命
        public float _scale;        //大きさ
        public float _shrinkRate;   //収縮時間
        public float _speed;        //速さ
        public bool _isActive;      //アクティブかどうか
        public Color _color;        //色

        //追加
        float firstSpeed;
        public float alpha;

        public Particle(string name,Vector2 size,Vector2 pos,float speed,float angle,float scale,float shrinkRate,float duration,Color color)
        {
            _name = name;
            _position = pos;
            _scale = scale;
            _shrinkRate = shrinkRate;
            _isActive = true;
            _duration = duration;
            _color = color;
            _speed = speed;
            _origin = new Vector2(size.X/2,size.Y/2);

            angle = MathHelper.ToRadians(angle);

            Vector2 up = new Vector2(0, -1.0f);
            Matrix rot = Matrix.CreateRotationZ(angle);
            _direction = Vector2.Transform(up, rot);

            //追加
            firstSpeed = 200;
            alpha = 1.0f;

        }
        
        public void Update(float delta)
        {
            _position += _direction * _speed * delta;
            //_position.Y += 50f;

            //_position.X += _direction.X * ((firstSpeed * delta) + ((500 * delta * delta) / 2));
            //_position.Y += _direction.Y *((firstSpeed * delta) + ((_speed * delta * delta) / 2));
            //_speed -=1500f;

            //_scale -= _shrinkRate * delta;

            _duration -= delta;
            alpha -= delta;
            _color *= alpha;

            if (_scale <= 0.0f || _duration <= 0.0f)
            {
                _isActive = false;
                _position = new Vector2(-100, -100);
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture(_name, _position, _color, 0, _origin, _scale);
        }
    }
}
