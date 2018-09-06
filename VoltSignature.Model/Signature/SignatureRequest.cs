using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VoltSignature.Model.Signature
{
    public class SignatureRequest
    {
        /// <summary>
        /// File to signature
        /// </summary>
        [Required]
        public IFormFile File { get; set; }
        
        public string Note { get; set; }

        /// <summary>
        /// Id who create signature request
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// List recipients id
        /// </summary>
        [Required]
        public List<string> Recipients { get; set; }


    }
}
