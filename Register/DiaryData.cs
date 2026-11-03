using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register
{
        public class DiaryData
    {
        public List<User> students { get; set; } = new();
        public List<User> teachers { get; set; } = new();
        public List<User> admins { get; set; } = new();
    }
}
