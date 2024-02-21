// buduje aplikacje
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CarWorkshop.Infrastructure.Extensions;
using CarWorkshop.Infrastructure.Seeders;
using CarWorkshop.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// dependency injection - konfiguracja kontenera
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

//buduje z serwisami
var app = builder.Build();

//tworz� zakres us�ug dla aplikacji 
var scope = app.Services.CreateScope();
//pobieram konkretn� us�ug� z tego zakresu us�ug -> Seedera doda�em
//w AddInfrastructure poprzez services.AddScoped<Seeder>()
var seeder = scope.ServiceProvider.GetRequiredService<CarWorkshopSeeder>();
//wywo�uje metod�, kt�ra sprawdzi czy baza danych ma recordy a jak nie ma to doda przypadkowy
await seeder.Seed();

//tutaj info jak aplikacja ma si� zachowywa�
// sprawdzenie czy aktualnie jeste�my w �rodowisku develpment
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//przekierowywanie na https -> przekierowanie zapyta� nieu�ywaj�cych tego protoko�u
app.UseHttpsRedirection();

//je�eli klient bedzie si� odnosi� do plik�w np. css czy js to b�dziemy je zwraca� do klienta
app.UseStaticFiles();


// dopasowywanie �cie�ek do konkretnych handlar�w
app.UseRouting();

app.UseAuthorization();

//pattern w jaki spos�b bedziemy dopasowywa� �cie�k� do handler�w
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Login korzysta z RazorPages, wi�c trzeba sobie to zmapowa�
app.MapRazorPages();
// jak to dzia�a? Home to folder w Views, i ma plik Index :D wi�c tak aplikacja szuka plik�w
app.Run();
