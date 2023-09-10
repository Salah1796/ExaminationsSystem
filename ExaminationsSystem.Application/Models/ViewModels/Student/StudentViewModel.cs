#region Using ...

using System;
#endregion

namespace ExaminationsSystem.Application.Models.ViewModels
{
    public class StudentViewModel
    {
        #region Properties      
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        #endregion
    }
}
