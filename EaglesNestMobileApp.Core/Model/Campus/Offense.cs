﻿using System;

namespace EaglesNestMobileApp.Core.Model.Campus
{
    public class Offense
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string OffenseDescription { get; set; }
        public string OffenseDate { get; set; }
        public string OffenseName { get; set; }
        public float Demerits { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string version { get; set; }
    }
}
