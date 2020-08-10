using System.Threading.Tasks;
using AutoMapper;
using TechParts.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechParts.API.Dtos;
using System;
using TechParts.API.Helpers;
using System.Linq;

namespace TechParts.API.Data
{
    public class PartRepository : IPartRepository
    {
        private DataContext _context { get; set; }

        public PartRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Part>> GetAllParts()
        {
            return await _context.parts.ToListAsync();
        }

        public async Task<Part> GetPart(int id)
        {
            System.Console.WriteLine("looking from id " + id);
            return await _context.parts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> GetCountAvailable(int id)
        {
            var part = await _context.parts.FirstOrDefaultAsync(p => p.Id == id);

            if(part != null) 
                return part.CountAvailable;
            else 
                return -1;
        }

        public async Task<bool> SaveDatabase()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Part>> GetAllPartsPaged(PagedParams pagedParams)
        {
            return await _context.parts.AsQueryable()
            .Skip((pagedParams.CurrentPage - 1) * pagedParams.ItemPerPage)
            .Take(pagedParams.ItemPerPage).ToListAsync();
        }

        public async Task<int> GetNumberOfParts()
        {
            return await _context.parts.CountAsync();
        }
    }
}