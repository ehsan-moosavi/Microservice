using Microservice.Interfaces;
using Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Repository
{
    public class PlatformRepo : IPlatformRepo
    {
        public AppDbContext _Context { get; }
        public PlatformRepo(AppDbContext context)
        {
            _Context = context;    
        }

       

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));

            }
            _Context.Platforms.Add(plat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _Context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _Context.Platforms.FirstOrDefault(p => p.id == id);
        }

        public bool SaveChanges()
        {
            return (_Context.SaveChanges() >= 0);
        }
    }
}
