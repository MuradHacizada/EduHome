using EduHome.Models;
using System.Collections.Generic;

namespace EduHome.ViewModels
{
    public class AboutVM
    {
        public About About { get; set; }  
        public List<AboutTeacher> AboutTeachers { get; set; }    
        public List<Info> Infos { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }
    }
}
