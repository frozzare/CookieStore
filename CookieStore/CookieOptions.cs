using System;

namespace CookieStore
{
    /// <summary>
    /// Cookie options, will override HttpCookie's properties that's matching if any is set.
    /// </summary>
    public class CookieOptions
    {
        public bool HttpOnly { get; set; }
        public bool Secure { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTime Expires { get; set; }
    }
}
