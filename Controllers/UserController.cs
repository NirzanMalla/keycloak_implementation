using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IMongoCollection<User> _usercollection;
    public UserController(IMongoDatabase mongoDatabase)
    {
        _usercollection = mongoDatabase.GetCollection<User>("user");

    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(int id, string name, string phone, string email)
    {
        var user = new User()
        {
            Id = id,
            Name = name,
            Phone = phone,
            Email = email
        };
        await _usercollection.InsertOneAsync(user);
        return Ok("User created successfully");
    }
}