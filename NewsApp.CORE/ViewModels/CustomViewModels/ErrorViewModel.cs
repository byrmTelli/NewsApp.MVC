using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.ViewModels.CustomViewModels
{
    public class ErrorViewModel
    {
        public List<string> Errors { get; set; }
        public bool IsShow { get; set; }


        #region Constructor Definitions
        public ErrorViewModel()
        {
            Errors = new List<string>();
        }

        public ErrorViewModel(string error,bool isShow)
        {
            Errors = new List<string>();
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorViewModel(List<string> errors,bool isShow)
        {
            Errors = new List<string>();
            Errors.AddRange(errors);
            IsShow = isShow;
        }
        #endregion
    }
}
