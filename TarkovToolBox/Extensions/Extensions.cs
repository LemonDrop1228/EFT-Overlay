using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TarkovToolBox.Extensions
{
    public static class Extensions
    {
        public static System.Windows.Media.Color WithAlpha(this System.Drawing.Color color, int newA) {
            var aplhColor = System.Drawing.Color.FromArgb(newA,color);
            return System.Windows.Media.Color.FromArgb(aplhColor.A, aplhColor.R, aplhColor.G, aplhColor.B);
        }

        public static int AddCollection(this UIElementCollection collection, IEnumerable<Control> controls)
        {
            if (controls == null)
                return 0;

            if (collection == null)
                throw new InvalidOperationException("An exception occured while trying to add child controls to a null ui object.");

            foreach (var control in controls)
            {
                collection.Add(control);
            }

            return collection.Count;
        }
    }
}
