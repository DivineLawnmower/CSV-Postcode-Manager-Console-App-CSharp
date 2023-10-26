using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGEChallengeApp.Core.Models.API
{
    /// <summary>
    /// Response given by the API.
    /// </summary>
    public class APIResponse
    {
        public string? JSONData { get; set; }

        [Required]
        public bool IsSuccess { get; set; }
    }
}
