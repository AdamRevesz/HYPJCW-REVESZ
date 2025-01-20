using Logic.Helper;
using Logic.Interfaces;
using Models;
using Repository;
using Models.Dtos.SalesItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SalesItemLogic
    {
        private readonly Repository<SalesItem> salesItemRepo;
        private readonly DtoProvider dtoProvider;

        public SalesItemLogic(Repository<SalesItem> salesItemRepo, DtoProvider dtoProvider)
        {
            this.salesItemRepo = salesItemRepo;
            this.dtoProvider = dtoProvider;
        }

        public void AddSalesItem(SalesItemCreateUpdateDto dto)
        {
            SalesItem salesItem = dtoProvider.Mapper.Map<SalesItem>(dto);

            if (salesItemRepo.GetAll().FirstOrDefault(x => x.Title == salesItem.Title) == null)
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
            return salesItemRepo.GetAll().Select(x => dtoProvider.Mapper.Map<SalesItemShortViewDto>(x));
        }

        public void DeleteSalesItem(string id)
        {
            salesItemRepo.DeleteById(id);
        }

        public void UpdateSalesItem(string id, SalesItemCreateUpdateDto dto)
        {
            var oldSalesItem = salesItemRepo.FindById(id);
            dtoProvider.Mapper.Map(dto, oldSalesItem);
            salesItemRepo.Update(oldSalesItem);
        }

        public SalesItemViewDto GetSalesItem(string id)
        {
            var salesItem = salesItemRepo.FindById(id);
            return dtoProvider.Mapper.Map<SalesItemViewDto>(salesItem);
        }
    }
}


