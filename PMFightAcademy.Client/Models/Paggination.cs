﻿using System.ComponentModel.DataAnnotations;

namespace PMFightAcademy.Client.Models
{
    /// <summary>
    /// Paggination model.
    /// </summary>
    public class Paggination
    {
        /// <summary>
        /// Current page number.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        /// <summary>
        /// Total pages count.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int TotalPages { get; set; }

        /// <summary>
        /// Indicates if the current page has a previous one.
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (Page > 1);
            }
        }

        /// <summary>
        /// Indicates if the current page has a next one.
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (Page < TotalPages);
            }
        }
    }
}