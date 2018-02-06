using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace webREINF
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new SessionBLL().Connect(new Model.SessionModel()
            {
                User = "postgres",
                Password = "beleza",
                Server = "localhost",
                Database ="reinf"
                
            });

            new SessionBLL().TestPosrgresServer();
        }
    }
}