using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LoaiSPBUS
    {
        SanPhamModel context = new SanPhamModel();
        public List<LoaiSP> GetAll()
        {
            return context.LoaiSPs.ToList();
        }
    }
}
