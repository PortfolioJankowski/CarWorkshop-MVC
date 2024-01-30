// buduje aplikacje
var builder = WebApplication.CreateBuilder(args);

// dependency injection - konfiguracja kontenera
builder.Services.AddControllersWithViews();

//buduje z serwisami
var app = builder.Build();

//tutaj info jak aplikacja ma siê zachowywaæ
// sprawdzenie czy aktualnie jesteœmy w œrodowisku develpment
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//przekierowywanie na https -> przekierowanie zapytañ nieu¿ywaj¹cych tego protoko³u
app.UseHttpsRedirection();

//je¿eli klient bedzie siê odnosi³ do plików np. css czy js to bêdziemy je zwracaæ do klienta
app.UseStaticFiles();


// dopasowywanie œcie¿ek do konkretnych handlarów
app.UseRouting();

app.UseAuthorization();

//pattern w jaki sposób bedziemy dopasowywaæ œcie¿kê do handlerów
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// jak to dzia³a? Home to folder w Views, i ma plik Index :D wiêc tak aplikacja szuka plików
app.Run();
