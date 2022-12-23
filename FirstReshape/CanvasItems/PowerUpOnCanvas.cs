using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Space_Shooter
{
    class PowerUpOnCanvas : ObjectOnCanvas
    {
        public PowerUpOnCanvas(Canvas canvas, string uri, int height = 20, int width = 20 ):base(canvas, uri, height, width)
        {

        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
