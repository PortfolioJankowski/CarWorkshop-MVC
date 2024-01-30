// buduje aplikacje
var builder = WebApplication.CreateBuilder(args);

// dependency injection - konfiguracja kontenera
builder.Services.AddControllersWithViews();

//buduje z serwisami
var app = builder.Build();

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
// jak to dzia�a? Home to folder w Views, i ma plik Index :D wi�c tak aplikacja szuka plik�w
app.Run();
