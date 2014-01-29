using System;

namespace CookieStore.Test
{
    /// <summary>
    /// User object.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public partial class SetCookie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var store = new CookieStore();

            // Create a new user.
            var user = new User
            {
                Id = 1,
                Name = "Eleonor"
            };

            // Save it and add some settings.
            store.Save(user, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true
            });
        }
    }
}