using Assets_Management_System.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assets_Management_System.Services
{
    public class AssetService
    {
        private List<Asset> assets = new List<Asset>();

        public void Add(Asset asset)
        {
            if (asset != null)
                assets.Add(asset);
        }

        public List<Asset> GetAll()
        {
            return new List<Asset>(assets);
        }

        public Asset GetById(int id)
        {
            return assets.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Asset asset)
        {
            var existing = assets.FirstOrDefault(a => a.Id == asset.Id);
            if (existing != null)
            {
                existing.Name = asset.Name;
                existing.Category = asset.Category;
                existing.SerialNumber = asset.SerialNumber;
                existing.PurchaseDate = asset.PurchaseDate;
                existing.Price = asset.Price;
                existing.EmployeeName = asset.EmployeeName;
                existing.Status = asset.Status;
                existing.Notes = asset.Notes;
                existing.ImagePath = asset.ImagePath;
            }
        }

        public void Delete(int id)
        {
            var asset = assets.FirstOrDefault(a => a.Id == id);
            if (asset != null)
                assets.Remove(asset);
        }
    }
}