using Microsoft.EntityFrameworkCore;
using WebApplication1Minimal.DBConnection;
using WebApplication1Minimal.DBConnection.Model;
using M = WebApplication1Minimal.DBConnection.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

builder.Services.AddCors(options =>
{
    options.AddPolicy("customPolicy",policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services.AddDbContext<APPDBContext>(opction =>
{
    opction.UseNpgsql(builder.Configuration.GetConnectionString("DBString"));
});

var app = builder.Build();

app.UseCors("customPolicy");

//api
app.MapGet("/", GetAll);
app.MapPost("/", AddName);
app.MapPut("/", UpdateName);
app.MapDelete("/{id}", DeleteName);
IResult GetAll(APPDBContext _context)
{
    var val = _context.Tasks.ToList();
    return Results.Ok(val);
}
IResult AddName(DTOTask utask, APPDBContext _context)
{
    M.Task t = new M.Task { Name = utask.Name };
    _context.Tasks.Add(t);
    _context.SaveChanges();
    return Results.Ok(new { Msg = "Data Added", Flag = true });
}
IResult UpdateName(DTOTask utask,APPDBContext _context)
{
    _context.Tasks.FirstOrDefault(obj=> obj.Id == utask.Id).Name = utask.Name;
    _context.SaveChanges();
    return Results.Ok(new { Msg = "Data Updated", Flag = true });
}
IResult DeleteName(Guid id, APPDBContext _context)
{
    _context.Tasks.Remove(_context.Tasks.FirstOrDefault(obj => obj.Id == id));
    _context.SaveChanges();
    return Results.Ok(new { Msg = "Data Deleted", Flag = true });
}
    


if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.Run();
