using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Assignment13750.Data;
using Microsoft.AspNetCore;

using StripeExample.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Assignment13750Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Assignment13750Context") ?? throw new InvalidOperationException("Connection string 'Assignment13750Context' not found.")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICredentialManager, CredRepository>();
builder.Services.AddScoped<IClassManager, ClassRepository>();
builder.Services.AddScoped<ITeacherClassManager, TeacherClassRepository>();
builder.Services.AddScoped<IEnrollmentManager, EnrollmentRepository>();
builder.Services.AddScoped<IAssignmentManager, AssignmentRepository>();
builder.Services.AddScoped<ISubmissionManager, SubmissionRepository>();
builder.Services.AddScoped<IPaymentStatusManager, PaymentStatusRepository>();
builder.Services.AddAuthentication("Cookies")
    .AddCookie(o => o.LoginPath = "/login");
var app = builder.Build();


              

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//Adding Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

