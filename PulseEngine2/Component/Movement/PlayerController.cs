using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PulseEngine.Component.Interfaces;
using PulseEngine.Objects.Sprite;

namespace PulseEngine.Component.Movement
{
    public delegate void KeyPressedEventHandler(object sender, PressedArgs e);

    public enum MovementState
    { None, Left, Right, Up, Down, Jump, Fire1, Fire2 }

    public class PressedArgs : EventArgs
    {
       public Keys Keypressed { get; set; }
       public MovementState KState { get; set; }
       public float Velocity { get; set; }
       public Entity Attached { get; set; }
         
    }

    public class PlayerController : IEntityComponent, IEntityUpdateComponent
    {
        protected Entity _parent;

        public event KeyPressedEventHandler Pressed;
              
        public struct KeyBinding
        {
            public Keys keys;
            public MovementState movementState;
        };

        List<KeyBinding> KeyBindings =
            new List<KeyBinding>();

        //Public Memebers 
        public float Speed { get; set; }

        //Methods
        public void AddKeyBinding(Keys K, MovementState MS)
        {
            KeyBinding kb;
            kb.keys = K; kb.movementState = MS;

            KeyBindings.Add(kb);
        }

        public void Initialise()
        {
            this.Speed = 1.0f;
        }

        public void AttachTo(Entity entity)
        {
            this._parent = entity; 
        }

        public Entity GetOwner()
        {
            return _parent;
        }

        public void Update(GameTime gameTime)
        {
            PressedArgs _args = new PressedArgs();

            foreach (KeyBinding kb in KeyBindings)
                if (Keyboard.GetState().IsKeyDown(kb.keys))
                {
                    _args.Keypressed = kb.keys;
                    _args.KState = kb.movementState;
                    _args.Velocity = this.Speed;
                    _args.Attached = this._parent;
                            
                    if (Pressed != null)
                        Pressed(this, _args);
                }      
        }
    }
}
