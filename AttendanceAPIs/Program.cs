using NHibernate;
using PMPoshanWithAngular.Server.Data;
using PMPoshanWithAngular.Server.Data.DAO;
using PMPoshanWithAngular.Server.Filters.Security;
using PMPoshanWithAngular.Server.Helper;
using PMPoshanWithAngular.Server.Middlewares;
using ISessionFactory = NHibernate.ISessionFactory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResponseHeaderFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Student & Attendance Management API",
        Version = "1.0.0",
        Description = "API specifications for managing student profiles, photo uploads, embeddings, and attendance tracking."
    });
});

NHibernateHelper.Initialize(builder.Configuration);

builder.Services.AddSingleton<ISessionFactory>(_ => NHibernateHelper.AttendanceSessionFactory);
builder.Services.AddScoped(_ => NHibernateHelper.AttendanceSession());
builder.Services.AddScoped<IDocStudentService, DocStudentService>();
builder.Services.AddScoped<IDocPhotoService, DocPhotoService>();
builder.Services.AddScoped<IDocAttendanceService, DocAttendanceService>();
builder.Services.AddScoped<IDocSchoolService, DocSchoolService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpContextAccessor();


builder.Services.Configure<Microsoft.AspNetCore.HostFiltering.HostFilteringOptions>(options =>
{
    options.AllowedHosts = new List<string> { "*" };
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student & Attendance Management API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<HostValidation>();
app.UseMiddleware<AllowOnlyGetAndPost>();
app.UseMiddleware<RefererValidator>();
app.UseMiddleware<ApplyCSP>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
