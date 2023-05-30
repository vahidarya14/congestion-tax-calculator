using congestion.calculator;
using JsonSubTypes;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    /*.AddNewtonsoftJson(options =>{
            options.SerializerSettings.Converters.Add(
                JsonSubtypesConverterBuilder
                .Of(typeof(Vehicle), "VehicleType")
                .RegisterSubtype(typeof(Car), VehicleTypes.Car)
                .RegisterSubtype(typeof(Tractor), VehicleTypes.Tractor)
                .SerializeDiscriminatorProperty()
                .Build()
            );
        })*/ ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(/*c =>{
    c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "PolymorphismInWebApi",
                        Version = "v1"
                    }
                );
    c.UseAllOfToExtendReferenceSchemas();
    c.UseAllOfForInheritance();
    c.UseOneOfForPolymorphism();
    c.SelectDiscriminatorNameUsing(type =>
    {
        return type.Name switch
        {
            nameof(Vehicle) => "VehicleType",
            _ => null
        };
    });
}*/);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
