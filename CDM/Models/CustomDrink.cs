using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CDM.Models
{
    public class CustomDrink
    {
        public string queue { get; set; }
        public string BaseDrink { get; set; }
        public string BaseDrinkCode { get; set; }
        public string Size { get; set; }
        public List<Layer> Layers { get; set; } = new List<Layer>();
        public List<string> Layer { get; set; } = new List<string>();
        public float TotalCost { get; set; }


        public override string ToString()
        {
            return $"{BaseDrinkCode} - {Size},{string.Join("", Layer)}";
        }
        public float GetTotalCost()
        { 
            return TotalCost;
        }
    }

    public class Layer
    {
        public string Shot { get; set; }
        public string ShotCode { get; set; }
        public float Percentage { get; set; }
        public override string ToString()
        {
            return $"{ShotCode} - {Percentage}%,";
        }
    }

    public class drink
    { 
        public string drink_order { get; set; }
        public string prices { get; set; }
    }
}