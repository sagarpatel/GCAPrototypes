using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Prototype1
{
    class TargetObject:GameObject
    {

     

        public TargetObject(Texture2D tex):base( tex)
        {

           
            
        }


        public bool CheckInside(Rectangle other)
        {
            if (rect.Contains(other))
                return true;
            else
                return false;

        }


    }
}
