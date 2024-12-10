using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FREDDIE
{
   public class GUI_UTL
    {

        public static void ClearCheckBoxControls(GroupBox grp)
        {
            foreach (Control c in grp.Controls)
            {
                Application.DoEvents();
                if (c is CheckBox)
                {
                    CheckBox check_box = (CheckBox)c;
                    check_box.Checked = false;
                }
            }
        }

        public static void DisableCheckBoxControls(GroupBox grp)
        {
            foreach (Control c in grp.Controls)
            {
                Application.DoEvents();
                if (c is CheckBox)
                {
                    CheckBox check_box = (CheckBox)c;
                    check_box.Enabled = false;
                }
            }
        }

        public static void EnableCheckBoxControls(GroupBox grp)
        {
            foreach (Control c in grp.Controls)
            {
                Application.DoEvents();
                if (c is CheckBox)
                {
                    CheckBox check_box = (CheckBox)c;
                    check_box.Enabled = false;
                }
            }
        }

    }
}
