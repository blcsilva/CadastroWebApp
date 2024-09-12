using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.EntityFrameworkCore.Extensions; // Pacote necessário para o MySQL
using WebApp.Data; // Namespace para o contexto do banco de dados

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do DbContext para usar MySQL
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 21)))); // Versão do MySQL

            // Adiciona suporte para controllers e views
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Configuração para desenvolvimento: exibe páginas de erro detalhadas
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configuração para produção: exibe páginas de erro genéricas
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Redireciona HTTP para HTTPS
            app.UseHttpsRedirection();

            // Serve arquivos estáticos (CSS, JS, imagens, etc.)
            app.UseStaticFiles();

            // Roteamento de requisições
            app.UseRouting();

            // Middleware de autorização: verifica se o usuário está autenticado
            app.UseAuthorization();

            // Configuração dos endpoints para controllers e views
            app.UseEndpoints(endpoints =>
            {
                // Define a rota padrão para controllers e ações
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
