﻿using System;

namespace PMFightAcademy.Admin.Contract
{
    /// <summary>
    /// Dto
    /// </summary>
    public class SlotsCreateContract
    {
        public int Id { get; }
        /// <summary>
        /// Coach Id
        /// </summary>
        public int CoachId { get; set; }
        /// <summary>
        /// Time start
        /// </summary>
        public string TimeStart { get; set; }
        /// <summary>
        /// Time End
        /// </summary>
        public string TimeEnd { get; set; }
    }
}