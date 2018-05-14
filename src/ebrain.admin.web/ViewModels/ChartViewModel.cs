// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================
//https://www.c-sharpcorner.com/article/top-10-most-popular-charts-in-angular-with-net-core-api/
namespace Ebrain.ViewModels
{
    public class ChartViewModel
    {
        public System.Collections.Generic.List<ChartModel> ChartModels { get; set; } = new System.Collections.Generic.List<ChartModel>();
        public string[] ChartLabels { get; set; } = new string[0];
    }

    public class ChartModel
    {
        public decimal?[] Data { get; set; } = new decimal?[0];
        public string Label { get; set; } = string.Empty;
    }
}
