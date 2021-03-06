﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BusinessObjects
{
    public class Task
    {
        public int TaskId { get; set; }
        public string  TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }
        public int ProjectId { get; set; }
        public int ParentId { get; set; }
        public string ParentTaskName { get; set; }
        public User User { get; set; }
        public bool Status { get; set; }
    }
}
