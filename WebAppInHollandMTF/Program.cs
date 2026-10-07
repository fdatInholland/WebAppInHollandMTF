
using Microsoft.EntityFrameworkCore;
using WebAppInHollandMTF.DataLayer;
using WebAppInHollandMTF.Services;

namespace WebAppInHollandMTF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<NoteDBContext>(options =>
                                                        options.UseSqlServer(builder.Configuration.
                                                        GetConnectionString("NotesConnection")));

            //Register Blob Storage Service
         //   builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
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

            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<NoteDBContext>();
            //    db.Database.EnsureCreated();
            //}

            app.Run();
        }
    }
}
