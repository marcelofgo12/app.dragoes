var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

if (builder.Environment.IsDevelopment())
{
  builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
}

// Configura��o de sess�o
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(1440);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
  options.Cookie.SameSite = SameSiteMode.Strict;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Adicione esta linha para servir arquivos est�ticos
app.UseRouting();
app.UseSession(); // A sess�o deve vir ap�s UseRouting e antes de UseAuthorization
app.UseAuthorization();

app.MapRazorPages();

app.Run();