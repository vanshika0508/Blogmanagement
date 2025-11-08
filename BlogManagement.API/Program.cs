using BlogManagement.Core.Interfaces;
using BlogManagement.Infrastructure.Data;          // AppDbContext or ApplicationDbContext
using BlogManagement.Infrastructure.Repositories; // PostRepository, CommentRepository
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(); // Required for JSON Patch support


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
