using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Nea
{
    /// <summary>
    /// NeaCookie, will override HttpCookie's properties that's matching if any is set.
    /// </summary>
    public class NeaCookie
    {
        public bool HttpOnly { get; set; }
        public bool Secure { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTime Expires { get; set; }
    }

    /// <summary>
    /// Nea class.
    /// </summary>
    public class Nea
    {
        /// <summary>
        /// Get cookie.
        /// </summary>
        /// <typeparam name="T">The class to use.</typeparam>
        /// <returns>Instance of the class with values if the cookie exists.</returns>
        public T Get<T>() where T : new()
        {
            var obj = new T();
            var objType = obj.GetType();
            var cookie = HttpContext.Current.Request.Cookies[objType.Name];

            if (cookie == null || string.IsNullOrEmpty(cookie.Value) || cookie.Values.Count <= 0)
            {
                return obj;
            }

            foreach (var key in cookie.Values.Cast<string>().Where(key => objType.GetProperty(key) != null))
            {
                var prop = objType.GetProperty(key);
                object value = cookie.Values[key];
                value = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(obj, value, null);
            }

            return obj;
        }

        /// <summary>
        /// Set properties of the class to the cookie name value collection and save it. 
        /// </summary>
        /// <param name="obj">Instance of class</param>
        /// <param name="options">Cookie options</param>
        public void Save (object obj, object options = null)
        {
            var objType = obj.GetType();
            var cookie = new HttpCookie(objType.Name);
            var nameValueCollection = new NameValueCollection();

            foreach (var prop in objType.GetProperties())
            {
                var value = prop.GetValue(obj, null);

                if (value != null)
                {
                    nameValueCollection.Add(prop.Name, value.ToString());
                }
            }

            cookie.Values.Add(nameValueCollection);

            if (options != null)
            {
                foreach (var prop in options.GetType().GetProperties())
                {
                    var propValue = prop.GetValue(options, null);
                    if (propValue == null) continue;

                    var cookieProp = cookie.GetType().GetProperty(prop.Name);
                    var cookieValue = cookieProp.GetValue(cookie, null);

                    if (cookieValue != null && cookieValue != propValue)
                    {
                        cookieProp.SetValue(cookie, propValue, null);
                    }
                }
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Delete a cookie.
        /// </summary>
        /// <typeparam name="T">The class to use.</typeparam>
        public void Delete<T>() where T : new()
        {
            var obj = new T();
            var cookie = new HttpCookie(obj.GetType().Name)
            {
                Expires = DateTime.Now.AddDays(-1d)
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
