using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVCareManager.ViewModels
{
    class InsuredViewModel
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public InsuredViewModel(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
