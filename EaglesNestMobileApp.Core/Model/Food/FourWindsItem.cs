﻿using GalaSoft.MvvmLight;
using System;

namespace EaglesNestMobileApp.Core.Model
{
    public class FourWindsItem : ObservableObject
    {
        public string Id { get; set; }
        public string MealTheme { get; set; }
        public string MealTime { get; set; }
        public string LineNumber { get; set; }
        public string ItemName { get; set; }
        public string BuildingName { get; set; }
        public string Version { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}