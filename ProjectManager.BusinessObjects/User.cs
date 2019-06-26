using FluentValidation.Attributes;
using ProjectManager.BusinessObjects.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BusinessObjects
{
    [Validator(typeof(UserValidator))]
    public class User //: BaseModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public int ProjectId { get; set; }
    }
}
