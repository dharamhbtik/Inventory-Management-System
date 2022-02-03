﻿using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetInventoriesByName(string name);
        Task<Inventory> GetInventoryById(int id);
        Task AddInventoryAsync(Inventory inventory);
        Task UpdateInventory(Inventory inventory);
    }
}
