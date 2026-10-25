using ProductApi.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructreService(builder.Configuration);

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
//}

app.UseInfrastructurePolicy();
app.UseHttpsRedirection();

app.UseAuthorization(); 

app.MapControllers();

app.Run();
