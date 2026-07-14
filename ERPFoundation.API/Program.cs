using ERPFoundation.API.Filters;
using ERPFoundation.Infrastructure.DependencyInjection;
using ERPFoundation.API.Mappings;
using ERPFoundation.API.Middlewares;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => { options.Filters.Add<ValidationFilter>(); });

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<SupplierProfile>();
    cfg.AddProfile<ProductProfile>();
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();