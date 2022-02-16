using HeBianGu.Systems.Project;
using System;

namespace HeBianGu.App.Creator
{
    public class WorkflowProject : ProjectItem
    {
        public string TemplateID { get; set; }

        public double Size { get; set; }

        public string User { get; set; } = Environment.UserName;
    }
}
