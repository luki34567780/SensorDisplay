using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDisplay.SensorModels.SensorViewModel
{
    class ComPortViewModel : ObservableObject
    {
        public string Name { get; set; }
        public ComPortViewModel(string name)
        {
            Name = name;
        }
    }
}
