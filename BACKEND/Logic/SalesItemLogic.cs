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

        public void AddSalesItem(SalesItemCreateUpdateDto dto)
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
            return salesItemRepo.ReadAll().Select(x => dtoProvider.Mapper.Map<SalesItemShortViewDto>(x));
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

        public void UpdateSalesItem(string id, SalesItemCreateUpdateDto dto, string userId)
        {
            var oldSalesItem = salesItemRepo.Read(id);
            if (oldSalesItem.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You are not the owner of this sales item.");
            }
            dtoProvider.Mapper.Map(dto, oldSalesItem);
            salesItemRepo.Update(oldSalesItem);
        }

        public SalesItemViewDto GetSalesItem(string id)
        {
            var salesItem = salesItemRepo.Read(id);
            return dtoProvider.Mapper.Map<SalesItemViewDto>(salesItem);
        }
    }
}


