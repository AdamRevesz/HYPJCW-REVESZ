using Logic.Helper;
using Logic.Interfaces;
using Models;
using Models.Dtos.SalesItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repo;
using Data.ClassRepo;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Logic
{
    public class SalesItemLogic
    {
        private readonly IRepository<SalesItem> salesItemRepo;
        private readonly DtoProvider dtoProvider;

        public SalesItemLogic(IRepository<SalesItem> salesItemRepo, DtoProvider dtoProvider)
        {
            this.salesItemRepo = salesItemRepo;
            this.dtoProvider = dtoProvider;
        }

        public SalesItem GetPictureEntity(string id)
        {
            return salesItemRepo.Read(id);
        }

        public async Task AddSalesItem(SalesItemCreateDto dto)
        {
            SalesItem salesItem = dtoProvider.Mapper.Map<SalesItem>(dto);

            if (salesItemRepo.ReadAll().FirstOrDefault(x => x.Title == salesItem.Title) == null)
            {
                salesItemRepo.Create(salesItem);
            }
            else
            {
                throw new ArgumentException("SalesItem with the same name already exists");
            }
        }

        public async Task AddSalesItemPrivate(SalesItemCreateDto dto)
        {
            SalesItem salesItem = dtoProvider.Mapper.Map<SalesItem>(dto);

            if (salesItemRepo.ReadAll().FirstOrDefault(x => x.Title == salesItem.Title) == null)
            {
                salesItemRepo.Create(salesItem);
            }
            else
            {
                throw new ArgumentException("SalesItem with the same name already exists");
            }
        }

        public IEnumerable<SalesItemShortViewDto> GetAllSalesItems()
        {
            return salesItemRepo.ReadAll().Select(x => dtoProvider.Mapper.Map<SalesItemShortViewDto>(x))
                .Include(s => s.Owner)
                .ToList();
        }

        public void DeleteSalesItem(string id)
        {
            salesItemRepo.Remove(id);
        }

        public void DeleteOwnerSalesItem(string id, string userId)
        {
            var salesItem = salesItemRepo.Read(id);
            if (salesItem.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this sales item.");
            }
            salesItemRepo.Remove(id);
        }

        public void UpdateSalesItem(string id, SalesItemCreateUpdateDto dto)
        {
            var oldSalesItem = salesItemRepo.Read(id);
            dtoProvider.Mapper.Map(dto, oldSalesItem);
            salesItemRepo.Update(oldSalesItem);
        }

        public SalesItemViewDto GetSalesItem(string id)
        {
            var salesItem = salesItemRepo.Read(id);
            return dtoProvider.Mapper.Map<SalesItemViewDto>(salesItem);
        }

        public void AddSalesItemsFromJson(string list)
        {
            var salesItems = JsonSerializer.Deserialize<List<SalesItemCreateDto>>(list);
            foreach (var item in salesItems)
            {
                AddSalesItemPrivate(item).GetAwaiter().GetResult();
            }
        }

        public void Randomprice()
        {
            var salesItems = salesItemRepo.ReadAll().ToList();
            Random random = new Random();
            foreach (var item in salesItems)
            {
                item.Price = random.Next(1, 100);
                salesItemRepo.Update(item);
            }
        }
    }
}


