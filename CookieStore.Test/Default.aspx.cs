using System;

namespace CookieStore.Test
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var store = new CookieStore();
            var user = store.Get<User>();
            Response.Write(user.Name + " has id " + user.Id);
        }
    }
}