using System;

namespace Nea.Test
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var nea = new Nea();
            var user = nea.Get<User>();
            Response.Write(user.Name + " has id " + user.Id + " with tags: " + string.Join(",", user.Tags));
        }
    }
}