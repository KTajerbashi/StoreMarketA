using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_Market_1
{
    public class Functions
    {
        public void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }
        }
        
        public bool selectStatus(String name, String family)
        {
            DBCode1 dbc = new DBCode1();
            foreach (var item in dbc.admins)
            {
                if (item.Name == name && item.Family == family)
                {
                    return true;
                }
            }
            return false;
        }
        public bool AccessCode(String accessCode)
        {
            DBCode1 dbc = new DBCode1();
            foreach (var item in dbc.roleAccesses)
            {
                if (item.access == accessCode)
                {
                    return true;
                }
            }
            return false;
        }
        public int Role(String Role)
        {
            if (Role == "ادمین")
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public bool RegisterAdmin(Admin admin)
        {
            DBCode1 dbc = new DBCode1();
            if (!selectStatus(admin.Name, admin.Family) && AccessCode(admin.accessCode))
            {
                dbc.admins.Add(new Admin
                {
                    Name = admin.Name,
                    Family = admin.Family,
                    RoleId = admin.RoleId,
                    Phone = admin.Phone,
                    Email = admin.Email,
                    Address = admin.Address,
                    IsActive = admin.IsActive,
                    accessCode = admin.accessCode,
                    Username = admin.Username,
                    Password = admin.Password,
                });
                dbc.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Enter(Admin admin)
        {
            DBCode1 dbc = new DBCode1();
            foreach (var item in dbc.admins)
            {
                if (item.Username == admin.Username && item.Password == admin.Password && item.accessCode == admin.accessCode)
                {
                    return true;
                }
            }

            return false;
        }
        public bool CheckAdmin(Admin admin)
        {
            DBCode1 dbc = new DBCode1();
            foreach (var item in dbc.admins)
            {
                if (item.Username == admin.Username && item.Phone == admin.Phone && item.accessCode == admin.accessCode)
                {
                    return true;
                }
            }
            return false;
        }
        public String CheckColeader(Admin coleader)
        {
            DBCode1 dbc = new DBCode1();

            foreach (var item in dbc.admins)
            {
                if (item.Username == coleader.Username && item.accessCode == coleader.accessCode && item.Phone == coleader.Phone && item.Email == coleader.Email)
                {
                    return item.Password;
                }
            }
            return "0";
        }
        public bool Newpass(String username, String password)
        {
            DBCode1 dbc = new DBCode1();
            Admin admin = dbc.admins.Where(c => c.Username == username).FirstOrDefault();
            admin.Password = password;
            dbc.SaveChanges();

            return true;
        }
        
    }
}
