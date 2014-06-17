# Cookie Store

Had an idea I wanted to try with cookies in C#.

## Example

```c#
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

var store = new CookieStore();

// Create a new user.
var user = new User
{
    Id = 1,
    Name = "Eleonor"
};

// Save it and add some options.
store.Save(user, new CookieOptions
{
    Expires = DateTime.Now.AddDays(1),
    HttpOnly = true
});

// Get the user from the cookie.
user = store.Get<User>();

// Output the result.
Response.Write(user.Name + " has id " + user.Id);

// Output: Eleonor has id 1
```
