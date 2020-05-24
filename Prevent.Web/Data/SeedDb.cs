using Prevent.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Prevent.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            PreventTypeEntity Consejo = CheckPreventType("Consejo");
            PreventTypeEntity Guia = CheckPreventType("Guía de información");
            await CheckPreventAsync(Consejo, Guia);
        }

        private async Task CheckPreventAsync(PreventTypeEntity Consejo, PreventTypeEntity Guia)
        {
            if (!_dataContext.Prevents.Any())
            {
                _dataContext.Prevents.Add(new PreventEntity
                {
                    Title = "CONSUMO DE ALCOHOL",
                    Description = "Cuanta mayor cantidad de alcohol consumas, mayor riesgo de padecer cáncer de mama tienes.",
                    Date = DateTime.UtcNow,
                    PreventTypeId = Consejo.Id,
                    PreventType = Consejo
                });

                _dataContext.Prevents.Add(new PreventEntity
                {
                    Title = "CONTROLAR EL PESO",
                    Description = "La gordura y la obesidad aumentan el riesgo de padecer cáncer de mama y más todavía cuando ocurren a mayor edad, particularmente después de la menopausia.",
                    Date = DateTime.UtcNow,
                    PreventTypeId = Guia.Id,
                    PreventType = Guia
                });
                await _dataContext.SaveChangesAsync();
            }
        }

        private PreventTypeEntity CheckPreventType(string Name)
        {
            return new PreventTypeEntity
            {
                Name = Name
            };
        }
    }
}