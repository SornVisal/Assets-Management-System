using Assets_Management_System.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assets_Management_System.Services
{
    public class AssetService
    {
        private List<Asset> assets = new List<Asset>();

        // Add new asset
        public void Add(Asset asset)
        {
            if (asset != null && !string.IsNullOrWhiteSpace(asset.Name))
            {
                assets.Add(asset);
            }
        }

        // Get all assets
        public List<Asset> GetAll()
        {
            return new List<Asset>(assets);
        }

        // Get asset by ID
        public Asset GetById(int id)
        {
            return assets.FirstOrDefault(a => a.Id == id);
        }

        // Update existing asset
        public void Update(Asset asset)
        {
            if (asset == null) return;

            var existingAsset = assets.FirstOrDefault(a => a.Id == asset.Id);
            if (existingAsset != null)
            {
                existingAsset.Name = asset.Name;
                existingAsset.Category = asset.Category;
                existingAsset.SerialNumber = asset.SerialNumber;
                existingAsset.PurchaseDate = asset.PurchaseDate;
                existingAsset.Price = asset.Price;
                existingAsset.EmployeeName = asset.EmployeeName;
                existingAsset.Status = asset.Status;
                existingAsset.Notes = asset.Notes;
                existingAsset.ImagePath = asset.ImagePath;
            }
        }

        // Delete asset
        public void Delete(int id)
        {
            var asset = assets.FirstOrDefault(a => a.Id == id);
            if (asset != null)
            {
                assets.Remove(asset);
            }
        }

        // Search assets
        public List<Asset> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAll();

            return assets.Where(a =>
                a.Name.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                a.SerialNumber.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) ||
                a.Category.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}