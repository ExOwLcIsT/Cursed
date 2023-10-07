using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursed.Models
{
    public class SkinViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public string BorderColor { get; set; }
        public string FieldColor { get; set; }
        public string BackgroundColor { get; set; }
    }
}
