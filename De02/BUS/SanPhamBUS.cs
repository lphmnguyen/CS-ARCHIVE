using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class SanPhamBUS
    {
        SanPhamModel context = new SanPhamModel();
        public List<SanPham> GetAll()
        {
            return context.SanPhams.ToList();
        }
        public void InsertUpdate(SanPham sanPham)
        {
            context.SanPhams.AddOrUpdate(sanPham);
            context.SaveChanges();
        }

        public void DeleteSanPham(SanPham sanPham)
        {
            context.SanPhams.Remove(sanPham);
            context.SaveChanges();
        }
    }
}
