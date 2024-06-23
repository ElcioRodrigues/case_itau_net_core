using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain.Common
{
    public class Result
    {
        public Result()
        {
            Messages = [];
            SuccessMessage = string.Empty;
        }

        public List<string> Messages { get; }
        public string SuccessMessage { get; set; }

        public bool Success { get { return Messages.Count == 0; } }
    }
}
