using PassIn.Api.Filters;
using PassIn.Application.UseCases.Attendees.DoCheckIn;
using PassIn.Application.UseCases.Attendees.GetBadge;
using PassIn.Application.UseCases.Events.GetAttendees;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Application.UseCases.Events.RegisterAttendee;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IAttendeeRepository, AttendeeRepository>();
builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();

builder.Services.AddScoped<IRegisterEventUseCase, RegisterEventUseCase>();
builder.Services.AddScoped<IGetEventByIdUseCase, GetEventByIdUseCase>();
builder.Services.AddScoped<IRegisterAttendeeOnEventUseCase, RegisterAttendeeOnEventUseCase>();
builder.Services.AddScoped<IGetAttendeesByEventIdUseCase, GetAttendeesByEventIdUseCase>();

builder.Services.AddScoped<IGetAttendeeBadgeUseCase, GetAttendeeBadgeUseCase>();
builder.Services.AddScoped<IDoAttendeeCheckInUseCase, DoAttendeeCheckInUseCase>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

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
